using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using NewGame.Classes;

namespace NewGame.Components.Input
{
    public class PlayerInputComponent : IInputComponent
    {
        private const int WALK_ACCELERATION = 1;
        
        public void Update(GameObject gameObject)
        {
            var keyState = Keyboard.GetState();

            //TODO: implement command pattern here http://gameprogrammingpatterns.com/command.html
            if (keyState.IsKeyDown(Keys.A))
            {
                gameObject.X -= WALK_ACCELERATION;
            }
            else if (keyState.IsKeyDown(Keys.D))
            {
                gameObject.X += WALK_ACCELERATION;
            }
            else if (keyState.IsKeyDown(Keys.S))
            {
                gameObject.Y += WALK_ACCELERATION;
            }
            else if (keyState.IsKeyDown(Keys.W))
            {
                gameObject.Y -= WALK_ACCELERATION;
            }
        }
    }
}
