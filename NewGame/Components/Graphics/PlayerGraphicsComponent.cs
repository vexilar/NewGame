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
        private Texture2D defaultSprite;
        
        public PlayerGraphicsComponent(ContentManager content)
        {
            defaultSprite = content.Load<Texture2D>("kirby.gif");
        }
        
        public void Update(GameObject gameObject, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(defaultSprite, new Rectangle(gameObject.X, gameObject.Y, gameObject.X + 20, gameObject.Y + 20), Color.White);
        }
    }
}
