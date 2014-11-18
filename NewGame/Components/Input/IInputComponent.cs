using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using NewGame.Classes;

namespace NewGame.Components.Input
{
    public interface IInputComponent
    {
        void Update(GameObject gameObject);
    }
}
