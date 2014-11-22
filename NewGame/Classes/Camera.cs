using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NewGame.Classes
{
    public class Camera
    {
        private Vector2 _position;
        private Matrix _transform;
        private GameObject _clampedObject;
        private Viewport _viewPort;

        public Vector2 Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
            }
        }

        public Matrix Transform
        {
            get
            {
                return _transform;
            }
            set
            {
                _transform = value;
            }
        }

        public GameObject ClampedObject
        {
            get
            {
                return _clampedObject;
            }
            set
            {
                _clampedObject = value;
            }
        }

        public Viewport viewPort
        {
            get
            {
                return _viewPort;
            }
            set
            {
                _viewPort = value;
            }
        }

        public Camera(Viewport viewport)
        {
            _position = Vector2.Zero;
            _viewPort = viewport;
        }

        public void Update(GameObject gameObject)
        {
            //_transform = Matrix.CreateTranslation(gameObject.Location.X, gameObject.Location.Y, 0);
            _transform = Matrix.CreateTranslation(0, 0, 0);
        }

        //Should take in IClampable, but for now just a game object
        public void Clamp(GameObject gameObject)
        {
            ClampedObject = gameObject;
        }
    }
}
