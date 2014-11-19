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
            gameObject.X += gameObject.XVelocity;
            gameObject.Y += gameObject.YVelocity;
        }
    }
}
