using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NewGame.Classes
{
    class MapCell
    {
        public Rectangle Coords;

        public MapCell(Vector2 location)
        {
            Coords = new Rectangle((int) location.X, (int) location.Y,
                TileMapEngine.TILE_SIZE, TileMapEngine.TILE_SIZE);
        }

        public Rectangle GetRelativeCoords(Viewport viewport)
        {
            var location = new Vector2(Coords.X, Coords.Y);
            Vector2 relativeLocation = Camera.GetRelativePosition(viewport, location);
            return new Rectangle((int)relativeLocation.X, (int)relativeLocation.Y, Coords.Width, Coords.Height);
        }
    }
}
