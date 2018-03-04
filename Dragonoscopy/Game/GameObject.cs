using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dragonoscopy.Interfaces;
using Dragonoscopy.Math;
using Dragonoscopy.Scenes;
using Microsoft.Xna.Framework.Graphics;

namespace Dragonoscopy.Game
{
    public abstract class GameObject : IDrawable, IUpdateable
    {
        public int ID { get; }
        public string Tag { get; }
        public Transform Transform { get; }
        protected Scene ParentScene { get; set; }

        protected GameObject(string tag = "")
        {
            ID = _numGameObjects;
            Tag = tag;
            Transform = new Transform();
            _numGameObjects++;
        }

        public abstract void Start();

        public abstract void Draw(SpriteBatch batch);

        public abstract void Update(double delta);

        public void SetParentScene(Scene scene)
        {
            ParentScene = scene;
        }

        private static int _numGameObjects = 0;
    }
}
