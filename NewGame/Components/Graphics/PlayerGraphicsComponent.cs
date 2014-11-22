using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NewGame.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewGame.Helpers;
using System.Diagnostics;

namespace NewGame.Components.Graphics
{
    public class PlayerGraphicsComponent : IGraphicsComponent
    {
        public Rectangle toDraw = new Rectangle();

        private Texture2D _spriteSheet;
        private LinkedList<Rectangle> _walkingUpAnimationCoords;
        private LinkedList<Rectangle> _walkingDownAnimationCoords;
        private LinkedList<Rectangle> _walkingLeftAnimationCoords;
        private LinkedList<Rectangle> _walkingRightAnimationCoords;

        private LinkedListNode<Rectangle> currentSpriteCoords;

        private Stopwatch AnimationTimer = new Stopwatch();
        private float AnimationThreshhold = 150;
        private bool Animate = false;

        public PlayerGraphicsComponent(ContentManager content)
        {
            _spriteSheet = content.Load<Texture2D>("HeroSS2H.png");

            var spriteSheetParser = new SpriteSheetParser(_spriteSheet, 4, 6);

            _walkingDownAnimationCoords = spriteSheetParser.GetCoordsForAnimation(0, 0, 0, 3);
            _walkingRightAnimationCoords = spriteSheetParser.GetCoordsForAnimation(1, 0, 1, 3);
            _walkingLeftAnimationCoords = spriteSheetParser.GetCoordsForAnimation(2, 0, 2, 3);
            _walkingUpAnimationCoords = spriteSheetParser.GetCoordsForAnimation(3, 0, 3, 3);
        }
        
        public void Update(GameObject gameObject)
        {

            if (!AnimationTimer.IsRunning)
            {
                AnimationTimer.Start();
            }
            else
            {
                if (AnimationTimer.ElapsedMilliseconds > AnimationThreshhold)
                {
                    AnimationTimer.Stop();
                    Animate = true;
                }
            }


                if (currentSpriteCoords == null)
                    currentSpriteCoords = _walkingDownAnimationCoords.First;

                if (Animate)
                {
                if (gameObject.Velocity.X > 0)
                {
                    currentSpriteCoords = getNextOrFirst(currentSpriteCoords, _walkingRightAnimationCoords);
                }
                else if (gameObject.Velocity.X < 0)
                {
                    currentSpriteCoords = getNextOrFirst(currentSpriteCoords, _walkingLeftAnimationCoords);
                }
                else if (gameObject.Velocity.Y < 0)
                {
                    currentSpriteCoords = getNextOrFirst(currentSpriteCoords, _walkingUpAnimationCoords);
                }
                else if (gameObject.Velocity.Y > 0)
                {
                    currentSpriteCoords = getNextOrFirst(currentSpriteCoords, _walkingDownAnimationCoords);
                }
                else
                {
                    currentSpriteCoords = currentSpriteCoords.List.First;
                }
                Animate = false;
                AnimationTimer.Reset();
                AnimationTimer.Start();
            }

            toDraw = currentSpriteCoords.Value;

        }

        public void Draw(GameObject gameObject, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_spriteSheet, new Rectangle((int)gameObject.Location.X, (int)gameObject.Location.Y, toDraw.Width, toDraw.Height), toDraw, Color.White);
        }

        LinkedListNode<Rectangle> getNextOrFirst(LinkedListNode<Rectangle> current, LinkedList<Rectangle> list)
        {
            return list.Contains(current.Value) ? (current.Next ?? current.List.First) : list.First;
        } 
    }
}
