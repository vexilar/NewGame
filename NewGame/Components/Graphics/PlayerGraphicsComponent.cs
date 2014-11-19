using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NewGame.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewGame.Components.Graphics
{
    public class PlayerGraphicsComponent : IGraphicsComponent
    {
        private Texture2D spriteSheet;
        private Dictionary<PlayerSpritePosition, Rectangle> spriteCoordinates;
        private WalkSequences walkSequences;
        private LinkedListNode<PlayerSpritePosition> _currentPosition;
        
        public PlayerGraphicsComponent(ContentManager content)
        {
            spriteSheet = content.Load<Texture2D>("HeroSS2H.png");

            var spriteSheetParser = new SpriteSheetParser(spriteSheet, 4, 6);

            spriteCoordinates = new Dictionary<PlayerSpritePosition, Rectangle>();

            spriteCoordinates.Add(PlayerSpritePosition.StandingFacingDown, spriteSheetParser.getRectangleForSprite(0, 0));
            spriteCoordinates.Add(PlayerSpritePosition.WalkingDownWithLeftFootForward, spriteSheetParser.getRectangleForSprite(0, 1));
            spriteCoordinates.Add(PlayerSpritePosition.WalkingDownWithRightFootForward, spriteSheetParser.getRectangleForSprite(0, 3));
            spriteCoordinates.Add(PlayerSpritePosition.StandingFacingRight, spriteSheetParser.getRectangleForSprite(1, 0));
            spriteCoordinates.Add(PlayerSpritePosition.WalkingRightWithRightFootForward, spriteSheetParser.getRectangleForSprite(1, 1));
            spriteCoordinates.Add(PlayerSpritePosition.WalkingRightWithLeftFootForward, spriteSheetParser.getRectangleForSprite(1, 3));
            spriteCoordinates.Add(PlayerSpritePosition.StandingFacingLeft, spriteSheetParser.getRectangleForSprite(2, 0));
            spriteCoordinates.Add(PlayerSpritePosition.WalkingLeftWithLeftFootForward, spriteSheetParser.getRectangleForSprite(2, 1));
            spriteCoordinates.Add(PlayerSpritePosition.WalkingLeftWithRightFootForward, spriteSheetParser.getRectangleForSprite(2, 3));
            spriteCoordinates.Add(PlayerSpritePosition.StandingFacingUp, spriteSheetParser.getRectangleForSprite(3, 0));
            spriteCoordinates.Add(PlayerSpritePosition.WalkingUpWithLeftFootForward, spriteSheetParser.getRectangleForSprite(3, 1));
            spriteCoordinates.Add(PlayerSpritePosition.WalkingUpWithRightFootForward, spriteSheetParser.getRectangleForSprite(3, 3));

            walkSequences = new WalkSequences();
        }
        
        public void Update(GameObject gameObject, SpriteBatch spriteBatch)
        {
            Rectangle rectangleToDraw = new Rectangle(0, 0, 1, 1);
            
            if (_currentPosition == null)
                _currentPosition = walkSequences.DownSequence.Sequence.First;
            
            if (gameObject.XVelocity > 0)
            {
                rectangleToDraw = walkSequences.RightSequence.GetNextRectangle(_currentPosition, spriteCoordinates);
            }
            else if (gameObject.XVelocity < 0)
            {
                rectangleToDraw = walkSequences.LeftSequence.GetNextRectangle(_currentPosition, spriteCoordinates);
            }
            else if (gameObject.YVelocity < 0)
            {
                rectangleToDraw = walkSequences.UpSequence.GetNextRectangle(_currentPosition, spriteCoordinates);
            }
            else if (gameObject.YVelocity > 0)
            {
                rectangleToDraw = walkSequences.DownSequence.GetNextRectangle(_currentPosition, spriteCoordinates);
            }
            else
            {
                rectangleToDraw = spriteCoordinates[_currentPosition.Value];
            }

            spriteBatch.Draw(spriteSheet, new Rectangle(gameObject.X, gameObject.Y, rectangleToDraw.Width, rectangleToDraw.Height), rectangleToDraw, Color.White);
        }
    }

    //TODO: this might end up going in a base class for other graphics components to use
    public enum PlayerSpritePosition
    {
        StandingFacingDown,
        WalkingDownWithLeftFootForward,
        WalkingDownWithRightFootForward,
        StandingFacingRight,
        WalkingRightWithRightFootForward,
        WalkingRightWithLeftFootForward,
        StandingFacingLeft,
        WalkingLeftWithLeftFootForward,
        WalkingLeftWithRightFootForward,
        StandingFacingUp,
        WalkingUpWithLeftFootForward,
        WalkingUpWithRightFootForward
    }

    //TODO: this might end up going in a base class for other graphics components to us
    public class WalkSequences
    {
        public WalkSequence UpSequence;
        public WalkSequence DownSequence;
        public WalkSequence LeftSequence;
        public WalkSequence RightSequence;

        public WalkSequences()
        {
            UpSequence = new WalkSequence();
            DownSequence = new WalkSequence();
            LeftSequence = new WalkSequence();
            RightSequence = new WalkSequence();
            
            var toAddAfter = UpSequence.Sequence.AddFirst(PlayerSpritePosition.StandingFacingUp);
            toAddAfter = UpSequence.Sequence.AddAfter(toAddAfter, PlayerSpritePosition.WalkingUpWithLeftFootForward);
            toAddAfter = UpSequence.Sequence.AddAfter(toAddAfter, PlayerSpritePosition.WalkingUpWithRightFootForward);


            toAddAfter = DownSequence.Sequence.AddFirst(PlayerSpritePosition.StandingFacingDown);
            toAddAfter = DownSequence.Sequence.AddAfter(toAddAfter, PlayerSpritePosition.WalkingDownWithLeftFootForward);
            toAddAfter = DownSequence.Sequence.AddAfter(toAddAfter, PlayerSpritePosition.WalkingDownWithRightFootForward);

            toAddAfter = LeftSequence.Sequence.AddFirst(PlayerSpritePosition.StandingFacingLeft);
            toAddAfter = LeftSequence.Sequence.AddAfter(toAddAfter, PlayerSpritePosition.WalkingLeftWithLeftFootForward);
            toAddAfter = LeftSequence.Sequence.AddAfter(toAddAfter, PlayerSpritePosition.WalkingLeftWithRightFootForward);

            toAddAfter = RightSequence.Sequence.AddFirst(PlayerSpritePosition.StandingFacingRight);
            toAddAfter = RightSequence.Sequence.AddAfter(toAddAfter, PlayerSpritePosition.WalkingRightWithRightFootForward);
            toAddAfter = RightSequence.Sequence.AddAfter(toAddAfter, PlayerSpritePosition.WalkingRightWithLeftFootForward);
        }
    }

    //TODO: this might end up going in a base class for other graphics components to us
    public class WalkSequence
    {
        public LinkedList<PlayerSpritePosition> Sequence;

        public WalkSequence()
        {
            Sequence = new LinkedList<PlayerSpritePosition>();
        }

        public Rectangle GetNextRectangle(LinkedListNode<PlayerSpritePosition> current,
                                            Dictionary<PlayerSpritePosition, Rectangle> spriteCoordinates)
        {
            PlayerSpritePosition nextPosition = Sequence.Contains(current.Value) ?
                (current.Next != null ? current.Next.Value : current.List.First.Value) : Sequence.First.Value;

            return spriteCoordinates[nextPosition];
        }
    }

    //TODO: this should be moved to a helper class
    public class SpriteSheetParser
    {
        private int spritesheetRowHeight;
        private int spritesheetColumnWidth;
        
        public SpriteSheetParser(Texture2D spritesheet, int rows, int columns)
        {
            spritesheetRowHeight = spritesheet.Height / rows;
            spritesheetColumnWidth = spritesheet.Width / columns;
        }

        public Rectangle getRectangleForSprite(int row, int column)
        {
            return new Rectangle(column * spritesheetColumnWidth, 
                                 row * spritesheetRowHeight,
                                 spritesheetColumnWidth,
                                 spritesheetRowHeight);
        }
    }
}
