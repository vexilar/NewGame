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
            // Physics code... (such as, don't let character walk off the edge of the world)
        }
    }
}
