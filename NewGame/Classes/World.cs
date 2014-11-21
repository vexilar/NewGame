using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NewGame.Classes
{
    public class World
    {
        private readonly TileMap _worldMap;
        private Vector2 _previousCameraLocation;
        
        public Viewport Viewport;

        public World(Viewport viewport)
        {
            Viewport = viewport;
            _previousCameraLocation = new Vector2(0, 0);
            _worldMap = new TileMap(viewport);
        }

        public void Update()
        {
            if (_previousCameraLocation != Camera.Location)
                _worldMap.Update();
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            if (_previousCameraLocation != Camera.Location)
            {
                for (int y = 0; y < _worldMap.RowsContainer.Rows.Count; y++)
                {
                    for (int x = 0; x < _worldMap.RowsContainer.Rows[y].Columns.Count; x++)
                    {
                        spriteBatch.Draw(Tile.TileSetTexture,
                            _worldMap.RowsContainer.Rows[y].Columns[x].GetRelativeCoords(Viewport),
                            Tile.GetSourceRectangle(_worldMap.RowsContainer.Rows[y].Columns[x].TileID),
                            Color.White);
                    }
                }

                _previousCameraLocation = Camera.Location;
            }
        }
    }
}
