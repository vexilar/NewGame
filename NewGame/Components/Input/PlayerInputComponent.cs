using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using NewGame.Classes;
using Microsoft.Xna.Framework;

namespace NewGame.Components.Input
{
    public class PlayerInputComponent : IInputComponent
    {
        private KeyboardState CurrentKeyState;
        private KeyboardState PreviousKeyState;

        public void Update(GameObject gameObject)
        {
            PreviousKeyState = CurrentKeyState;
            CurrentKeyState = Keyboard.GetState();

            if (CurrentKeyState.IsKeyDown(Keys.A))
            {
                if (CurrentKeyState.IsKeyDown(Keys.W))
                {
                    gameObject.Velocity = new Vector2(-100, -100);
                }
                else if (CurrentKeyState.IsKeyDown(Keys.S))
                {
                    gameObject.Velocity = new Vector2(-100, 100);
                }
                else
                {
                    gameObject.Velocity = new Vector2(-150, 0);
                }
            }
            else if (CurrentKeyState.IsKeyDown(Keys.S))
            {
                if (CurrentKeyState.IsKeyDown(Keys.A))
                {
                    gameObject.Velocity = new Vector2(-100, 100);
                }
                else if (CurrentKeyState.IsKeyDown(Keys.D))
                {
                    gameObject.Velocity = new Vector2(100, 100);
                }
                else
                {
                    gameObject.Velocity = new Vector2(0, 150);
                }
            }
            else if (CurrentKeyState.IsKeyDown(Keys.D))
            {
                if (CurrentKeyState.IsKeyDown(Keys.S))
                {
                    gameObject.Velocity = new Vector2(100, -100);
                }
                else if (CurrentKeyState.IsKeyDown(Keys.W))
                {
                    gameObject.Velocity = new Vector2(100, -100);
                }
                else
                {
                    gameObject.Velocity = new Vector2(150, 0);
                }
            }
            else if (CurrentKeyState.IsKeyDown(Keys.W))
            {
                if (CurrentKeyState.IsKeyDown(Keys.A))
                {
                    gameObject.Velocity = new Vector2(-100, -100);
                }
                else if (CurrentKeyState.IsKeyDown(Keys.D))
                {
                    gameObject.Velocity = new Vector2(100, -100);
                }
                else
                {
                    gameObject.Velocity = new Vector2(0, -150);
                }
            }
            else
            {
                gameObject.Velocity = new Vector2(0, 0);
            }
        }
    }
}
