#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using NewGame.Classes;
using NewGame.Components.Graphics;
using NewGame.Components.Input;
using NewGame.Components.Physics;

#endregion

namespace NewGame
{
    public class NewGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<GameObject> gameObjects;
        World world;
        Camera camera;

        public NewGame()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {       
            var player = new GameObject(new PlayerInputComponent()
                       , new PlayerPhysicsComponent()
                       , new PlayerGraphicsComponent(Content));

            camera = new Camera(this.GraphicsDevice.Viewport);
            camera.Clamp(player);

            gameObjects = new List<GameObject>();
            gameObjects.Add(player);

            //world = new World(Content, this.GraphicsDevice.Viewport);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (var gameObject in gameObjects)
                gameObject.Update(world, gameTime);

            //world.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //Check out this link http://www.dreamincode.net/forums/topic/237979-2d-camera-in-xna/

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, camera.Transform);
            
            //world.Draw(spriteBatch);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.End();

            spriteBatch.Begin();

            foreach (var gameObject in gameObjects)
                gameObject.Draw(spriteBatch);

            spriteBatch.End();

        }
    }
}
