using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NewGame.Helpers
{
    public class SpriteSheetParser
    {
        private readonly int _spritesheetRowHeight;
        private readonly int _spritesheetColumnWidth;

        public SpriteSheetParser(Texture2D spritesheet, int rows, int columns)
        {
            _spritesheetRowHeight = spritesheet.Height / rows;
            _spritesheetColumnWidth = spritesheet.Width / columns;
        }

        public Rectangle GetCoordsForSprite(int row, int column)
        {
            return new Rectangle(column * _spritesheetColumnWidth,
                                 row * _spritesheetRowHeight,
                                 _spritesheetColumnWidth,
                                 _spritesheetRowHeight);
        }

        public LinkedList<Rectangle> GetCoordsForAnimation(int startRow, int startColumn, int endRow, int endColumn)
        {
            if (endRow < startRow || (startRow == endRow && endColumn < startColumn))
                throw new Exception("Can't end before you start (sheet is parsed left to right, top to bottom.");

            var coords = new LinkedList<Rectangle>();

            for (int y = startRow; y <= endRow; y++)
            {
                for (int x = startColumn; x <= endColumn; x++)
                {
                    coords.AddLast(new Rectangle(x * _spritesheetColumnWidth,
                                 y * _spritesheetRowHeight,
                                 _spritesheetColumnWidth,
                                 _spritesheetRowHeight));
                }
            }

            return coords;
        }
    }
}
