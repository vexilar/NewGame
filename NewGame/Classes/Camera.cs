using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NewGame.Classes
{
    static class Camera
    {
        static public Vector2 Location = Vector2.Zero;

        public static int ViewableHeight(Viewport viewport)
        {
            return viewport.Height;
        }

        public static int ViewableWidth(Viewport viewport)
        {
            return viewport.Width;
        }

        public static void Update()
        {
            KeyboardState ks = Keyboard.GetState();

            var speed = 10;

            if (ks.IsKeyDown(Keys.Left))
            {
                Camera.Location.X -= speed;
            }

            if (ks.IsKeyDown(Keys.Right))
            {
                Camera.Location.X += speed;
            }

            if (ks.IsKeyDown(Keys.Up))
            {
                Camera.Location.Y -= speed;
            }

            if (ks.IsKeyDown(Keys.Down))
            {
                Camera.Location.Y += speed;
            }
        }

        public static Rectangle GetViewableCoords(Viewport viewport)
        {
            var halfHeight = ViewableHeight(viewport) / 2;
            var halfWidth = ViewableWidth(viewport) / 2;
            var locationX = (int)Location.X;
            var locationY = (int)Location.Y;

            return new Rectangle(locationX - halfWidth, locationY - halfHeight,
                                    viewport.Width,
                                    viewport.Height);
        }

        public static Vector2 GetRelativePosition(Viewport viewport, Vector2 absolutePostion)
        {
            var viewableCoords = GetViewableCoords(viewport);
            
            return new Vector2(absolutePostion.X - viewableCoords.X, absolutePostion.Y - viewableCoords.Y);
        }
    }
}
