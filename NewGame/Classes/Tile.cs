using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NewGame.Classes
{
    class Tile
    {
        public const int TILE_SIZE = 32;
        
        static public Texture2D TileSetTexture;

        static public Rectangle GetSourceRectangle(int tileIndex)
        {
            return new Rectangle(tileIndex * TILE_SIZE, 0, TILE_SIZE, TILE_SIZE);
        }
    }
}
