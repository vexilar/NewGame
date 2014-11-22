using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NewGame.Components.Graphics;
using NewGame.Components.Input;
using NewGame.Components.Physics;

namespace NewGame.Classes
{
    public class GameObject
    {

        public Vector2 Location = new Vector2(150,150);
        public Vector2 RelativeLocation = new Vector2(0, 0);
        public Vector2 Velocity = new Vector2(0, 0);

        private IInputComponent _inputComponent;
        private IPhysicsComponent _physicsComponent;
        private IGraphicsComponent _graphicsComponent;

        public GameObject(IInputComponent inputComponent, IPhysicsComponent physicsComponent, IGraphicsComponent graphicsComponent)
        {
            _inputComponent = inputComponent;
            _physicsComponent = physicsComponent;
            _graphicsComponent = graphicsComponent;
        }

        public void Update(World world, GameTime gameTime)
        {
            _inputComponent.Update(this);
            _physicsComponent.Update(this, world, gameTime);
            _graphicsComponent.Update(this);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _graphicsComponent.Draw(this, spriteBatch);
        }
    }
}
