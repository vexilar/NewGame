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
                if (gameObject.XVelocity >= 0)
                {
                    gameObject.XVelocity -= WALK_ACCELERATION;
                    gameObject.YVelocity = 0;
                }
            }
            else if (keyState.IsKeyDown(Keys.D))
            {
                if (gameObject.XVelocity <= 0)
                {
                    gameObject.XVelocity += WALK_ACCELERATION;
                    gameObject.YVelocity = 0;
                }
            }
            else if (keyState.IsKeyDown(Keys.S))
            {
                if (gameObject.YVelocity <= 0)
                {
                    gameObject.YVelocity += WALK_ACCELERATION;
                    gameObject.XVelocity = 0;
                }
            }
            else if (keyState.IsKeyDown(Keys.W))
            {
                if (gameObject.YVelocity >= 0)
                {
                    gameObject.YVelocity -= WALK_ACCELERATION;
                    gameObject.XVelocity = 0;
                }
            }
            else
            {
                gameObject.XVelocity = 0;
                gameObject.YVelocity = 0;
            }
        }
    }
}
