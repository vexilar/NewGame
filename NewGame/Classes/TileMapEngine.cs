using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.Serialization;

namespace NewGame.Classes
{
    class TileMapEngine
    {
        private Rectangle _currentMapCoords;
        
        public const int TILE_SIZE = 32;

        public int MapRows
        {
            get { return (this.Height / TILE_SIZE); }
        }
        public int MapColumns
        {
            get { return (this.Width / TILE_SIZE); }
        }

        public int Width
        {
            get { return CurrentMapCoords.Width; }
        }
        public int Height
        {
            get { return CurrentMapCoords.Height; }
        }

        public Rectangle CurrentMapCoords
        {
            set { _currentMapCoords = value; }
            get { return _currentMapCoords; }
        }

        public List<MapCell> MapCells; 

        private static Viewport _viewport;
        private TileMap tileMap;

        public TileMapEngine(ContentManager content, Viewport viewport)
        {
            _viewport = viewport;

            MapCells = new List<MapCell>();

            populateCells();

            tileMap = getTileMap(content);
        }

        public void Update()
        {
            populateCells();

            MapCells.RemoveAll(cell => !CurrentMapCoords.Contains(cell.Coords));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var cell in MapCells)
            {
                Tile tile = tileMap.GetTile(cell);

                drawTileInCell(spriteBatch, cell, tile);
            }
        }

        private TileMap getTileMap(ContentManager content)
        {
            var fs = new FileStream("Content/TileMap.xml", FileMode.Open);
            var reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
            var ser = new DataContractSerializer(typeof(TileMap));
            var tileMap = new TileMap();

            try
            {
                tileMap = (TileMap)ser.ReadObject(reader, true);
            }
            catch
            {
                throw;
            }
            finally
            {
                reader.Close();
                fs.Close();
            }

            foreach (var region in tileMap.Regions)
            {
                region.Tile.Texture = content.Load<Texture2D>(region.Tile.TextureFileName);
            }

            return tileMap;
        }

        private Rectangle getViewableMapCoords()
        {
            var coords = Camera.GetViewableCoords(_viewport);

            var viewableCoordsXMultipleOf32 = getNearestMultipleOfTileSize(coords.X);
            var viewableCoordsYMultipleOf32 = getNearestMultipleOfTileSize(coords.Y);

            coords = new Rectangle(viewableCoordsXMultipleOf32, viewableCoordsYMultipleOf32,
                coords.Width, coords.Height);

            coords.Offset(-TileMapEngine.TILE_SIZE * 2, -TileMapEngine.TILE_SIZE * 2);
            coords.Inflate(TileMapEngine.TILE_SIZE * 4, TileMapEngine.TILE_SIZE * 4);

            return coords;
        }

        private int getNearestMultipleOfTileSize(int value)
        {
            int factor = TileMapEngine.TILE_SIZE;
            return
                    (int)Math.Round(
                         (value / (double)factor),
                         MidpointRounding.AwayFromZero
                     ) * factor;
        }

        private void populateCells()
        {
            CurrentMapCoords = getViewableMapCoords();

            for (int y = 0; y < MapRows; y++)
            {
                for (int x = 0; x < MapColumns; x++)
                {
                    var cellLocation = new Vector2(CurrentMapCoords.X + x * TileMapEngine.TILE_SIZE,
                                                    CurrentMapCoords.Y + y * TileMapEngine.TILE_SIZE);
                    var mapCell = new MapCell(cellLocation);

                    if (!MapCells.Any(c => c.Coords.Intersects(mapCell.Coords)))
                        MapCells.Add(new MapCell(
                                        new Vector2(CurrentMapCoords.X + x * TileMapEngine.TILE_SIZE,
                                                    CurrentMapCoords.Y + y * TileMapEngine.TILE_SIZE)));
                }
            }
        }

        private void drawTileInCell(SpriteBatch spriteBatch, MapCell cell, Tile tile)
        {
            spriteBatch.Draw(tile.Texture,
                                cell.GetRelativeCoords(_viewport),
                                tile.TextureLocation,
                                Color.White);
        }
    }

    [DataContract]
    class TileMap
    {
        [DataMember]
        public List<MapRegion> Regions;

        public Tile GetTile(MapCell cell)
        {
            var mappedRegions = this.Regions.Where(r => r.Coords.Contains(cell.Coords)).ToList();

            if (mappedRegions.Count == 0)
                return this.Regions.First().Tile;
            else
            {
                return mappedRegions.First().Tile;
            }
        }
    }

    [DataContract]
    class MapRegion
    {
        [DataMember]
        public Rectangle Coords;

        [DataMember]
        public Tile Tile;
    }

    [DataContract]
    class Tile
    {
        [DataMember]
        public string TextureName;

        [DataMember]
        public string TextureFileName;

        [DataMember]
        public Rectangle TextureLocation;

        public Texture2D Texture;
    }
}
