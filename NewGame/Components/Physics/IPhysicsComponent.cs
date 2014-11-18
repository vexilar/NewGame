using NewGame.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewGame.Components.Physics
{
    public interface IPhysicsComponent
    {
        void Update(GameObject gameObject, World world);
    }
}
