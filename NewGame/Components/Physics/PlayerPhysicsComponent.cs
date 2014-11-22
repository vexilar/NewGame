using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewGame.Classes;
using Microsoft.Xna.Framework;

namespace NewGame.Components.Physics
{
    public class PlayerPhysicsComponent : IPhysicsComponent
    {
        public void Update(GameObject gameObject, World world, GameTime gameTime)
        {
            if (gameObject.Velocity.X > 0)
            {
                int i = 0;
            }

            gameObject.Location += new Vector2((float)(gameObject.Velocity.X * gameTime.ElapsedGameTime.TotalSeconds)
                                            , (float)(gameObject.Velocity.Y * gameTime.ElapsedGameTime.TotalSeconds));

            //gameObject.RelativeLocation = Camera.GetRelativePosition(world.Viewport, gameObject.Location);
        }
    }
}
