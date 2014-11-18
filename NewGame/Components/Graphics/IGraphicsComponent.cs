using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using NewGame.Classes;

namespace NewGame.Components.Graphics
{
    public interface IGraphicsComponent
    {
        void Update(GameObject gameObject, SpriteBatch spriteBatch);
    }
}
