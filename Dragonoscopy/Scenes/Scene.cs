using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dragonoscopy.Game;
using Dragonoscopy.Graphics;
using Dragonoscopy.Interfaces;
using Dragonoscopy.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Dragonoscopy.Scenes
{
    public abstract class Scene
    {
        protected List<GameObject> Objects;
        protected ContentManager Content;
        protected GraphicsDevice GraphicsDevice;
        protected ResourceManager Resources;
        
        public SpriteBatch SpriteBatch { get; }

        public Camera Camera { get; }

        protected Scene(ContentManager content, GraphicsDevice device)
        {
            Objects = new List<GameObject>();
            Content = content;
            Camera = new Camera();
            SpriteBatch = new SpriteBatch(device);
            GraphicsDevice = device;
            Resources = new ResourceManager(Content);
        }

        public abstract void Start();
        public abstract void Update(GameTime time);
        public abstract void Draw();
        public abstract void LoadContent();
        public abstract void Dispose();

        public void RegisterObject(GameObject obj)
        {
            Objects.Add(obj);
            obj.Start();
            obj.SetParentScene(this);
        }

        public void DestroyObject(GameObject obj)
        {
            Objects.Remove(obj);
        }

        public void DestroyAllObjects()
        {
            Objects.Clear();
        }

        public GameObject GetObject(int id)
        {
            foreach (var obj in Objects)
            {
                if (obj.ID == id)
                    return obj;
            }
            return null;
        }

        public List<GameObject> GetObjectsWithTag(string tag)
        {
            List<GameObject> returnVal = new List<GameObject>();
            foreach (var obj in Objects)
            {
                if (obj.Tag.Equals(tag))
                    returnVal.Add(obj);
            }
            return returnVal;
        }
    }
}
