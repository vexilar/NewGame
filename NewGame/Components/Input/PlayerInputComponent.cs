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
        //TODO: this should be passed in to this component.  Not sure where from yet -- I'm thinking stats are going to be another component
        // so may have to implement message between components
        private const int WALK_ACCELERATION = 10;
        
        public void Update(GameObject gameObject)
        {
            var keyState = Keyboard.GetState();

            //TODO: implement command pattern here http://gameprogrammingpatterns.com/command.html
            if (keyState.IsKeyDown(Keys.A))
            {
                if (gameObject.Velocity.X >= 0)
                {
                    gameObject.Velocity.X -= WALK_ACCELERATION;
                    gameObject.Velocity.Y = 0;
                }
            }
            else if (keyState.IsKeyDown(Keys.D))
            {
                if (gameObject.Velocity.X <= 0)
                {
                    gameObject.Velocity.X += WALK_ACCELERATION;
                    gameObject.Velocity.Y = 0;
                }
            }
            else if (keyState.IsKeyDown(Keys.S))
            {
                if (gameObject.Velocity.Y <= 0)
                {
                    gameObject.Velocity.Y += WALK_ACCELERATION;
                    gameObject.Velocity.X = 0;
                }
            }
            else if (keyState.IsKeyDown(Keys.W))
            {
                if (gameObject.Velocity.Y >= 0)
                {
                    gameObject.Velocity.Y -= WALK_ACCELERATION;
                    gameObject.Velocity.X = 0;
                }
            }
            else
            {
                gameObject.Velocity.X = 0;
                gameObject.Velocity.Y = 0;
            }
        }
    }
}
