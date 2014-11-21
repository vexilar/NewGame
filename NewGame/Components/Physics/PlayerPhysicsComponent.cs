using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewGame.Classes;

namespace NewGame.Components.Physics
{
    public class PlayerPhysicsComponent : IPhysicsComponent
    {
        public void Update(GameObject gameObject, World world)
        {
            gameObject.Location.X += gameObject.Velocity.X;
            gameObject.Location.Y += gameObject.Velocity.Y;

            //TODO: make this configurable to lock/unlock camera
            Camera.Location.X = gameObject.Location.X;
            Camera.Location.Y = gameObject.Location.Y;

            gameObject.RelativeLocation = Camera.GetRelativePosition(world.Viewport, gameObject.Location);
        }
    }
}
