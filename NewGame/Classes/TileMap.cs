using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NewGame.Classes
{
    class MapRow
    {
        private List<MapCell> _columns; 
        
        public List<MapCell> Columns
        {
            get { return _columns; }
            set { _columns = value; }
        }

        public void AddRightColumn()
        {
            Rectangle coords = _columns.Last().Coords;

            coords.Offset(Tile.TILE_SIZE, 0);

            _columns.Add(getMapCell(new Vector2(coords.X, coords.Y)));
        }

        public void AddLeftColumn()
        {
            Rectangle coords = _columns.First().Coords;

            coords.Offset(-Tile.TILE_SIZE, 0);

            _columns.Insert(0, getMapCell(new Vector2(coords.X, coords.Y)));
        }

        public void RemoveRightColumn()
        {
            _columns.Remove(_columns.Last());
        }

        public void RemoveLeftColumn()
        {
            _columns.Remove(_columns.First());
        }

        public void RemoveColumn(MapCell cell)
        {
            _columns.Remove(cell);
        }

        public Rectangle Coords;

        public MapRow(Vector2 location)
        {
            _columns = new List<MapCell>();

            Coords = new Rectangle((int)location.X, (int)location.Y, TileMap.Width, TileMap.Height);

            for (int x = 0; x < TileMap.MapColumns; x++)
            {
                var columnLocation = new Vector2(Coords.X + (x * Tile.TILE_SIZE), Coords.Y);
                _columns.Add(getMapCell(columnLocation));
            }
        }

        //TODO: move this somewhere where we can get tile info better
        public static MapCell getMapCell(Vector2 location)
        {
            return new MapCell(getTileId(location),
                        new Rectangle((int)location.X, (int)location.Y,
                                        Tile.TILE_SIZE, Tile.TILE_SIZE));
        }

        //TODO: move this somewhere where we can get tile info better
        public static int getTileId(Vector2 location)
        {
            if (location.X < 200)
            {
                return 0;
            }
            else
            {
                return 1;
            }

        }
    }

    class MapRows
    {
        private List<MapRow> _rows;

        public List<MapRow> Rows 
        {
            get { return _rows; }
            set { _rows = value; }
        }

        public void AddBottomRow()
        {
            var newCoords = _rows.Last().Coords;
            newCoords.Offset(0, Tile.TILE_SIZE);
            _rows.Add(new MapRow(new Vector2(newCoords.X, newCoords.Y)));
        }

        public void AddTopRow()
        {
            var newCoords = _rows.First().Coords;
            newCoords.Offset(0, -Tile.TILE_SIZE);
            _rows.Insert(0, new MapRow(new Vector2(newCoords.X, newCoords.Y)));
        }

        public void RemoveBottomRow()
        {
            _rows.Remove(_rows.Last());
        }

        public void RemoveTopRow()
        {
            _rows.Add(_rows.First());
        }

        public MapRows(Rectangle viewableCoords)
        {
            _rows = new List<MapRow>();

            for (int i = 0; i < TileMap.MapRows; i++)
            {
                _rows.Add(new MapRow(new Vector2(viewableCoords.X, viewableCoords.Y + (i * Tile.TILE_SIZE))));
            }
        }
    }

    class TileMap
    {
        public MapRows RowsContainer;

        public static int MapRows
        {
            get { return (Camera.ViewableHeight(_viewport) / Tile.TILE_SIZE); }
        }
        public static int MapColumns
        {
            get { return (Camera.ViewableWidth(_viewport) / Tile.TILE_SIZE); }
        }

        public static int Width
        {
            get { return TileMap.MapColumns * Tile.TILE_SIZE; }
        }
        public static int Height
        {
            get { return TileMap.MapRows * Tile.TILE_SIZE; }
        }

        public Rectangle ViewableCoords;

        private static Viewport _viewport;

        public TileMap(Viewport viewport)
        {
            _viewport = viewport;
            ViewableCoords = Camera.GetViewableCoords(_viewport);
            RowsContainer = new MapRows(ViewableCoords);
        }

        public void Update()
        {
            ViewableCoords = Camera.GetViewableCoords(_viewport);

            var upperLeftCell = RowsContainer.Rows.First().Columns.First();
            var bottomRightCell = RowsContainer.Rows.Last().Columns.Last();

            if (ViewableCoords.Left < upperLeftCell.Coords.Left)
            {
                foreach (var row in RowsContainer.Rows)
                {
                    row.AddLeftColumn();
                }
            }

            if (ViewableCoords.Top < upperLeftCell.Coords.Top)
            {
                RowsContainer.AddTopRow();
            }

            if (ViewableCoords.Right > bottomRightCell.Coords.Right)
            {
                foreach (var row in RowsContainer.Rows)
                {
                    row.AddRightColumn();
                }
            }

            if (ViewableCoords.Bottom > bottomRightCell.Coords.Bottom)
            {
                RowsContainer.AddBottomRow();
            }

            foreach (var row in RowsContainer.Rows)
            {
                row.Columns.RemoveAll(c => !ViewableCoords.Contains(c.Coords));
            }

            RowsContainer.Rows = RowsContainer.Rows.Where(r => r.Columns.Count > 0).ToList();
        }
    }
}
