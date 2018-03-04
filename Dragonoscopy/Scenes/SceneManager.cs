using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dragonoscopy.Interfaces;
using Dragonoscopy.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dragonoscopy.Scenes
{
    public class SceneManager
    {
        private List<Scene> Scenes;

        public SceneManager()
        {
            Scenes = new List<Scene>();
        }

        public void AddScene(Scene scene)
        {
            Scenes.Add(scene);
            scene.LoadContent();
            scene.Start();
        }

        public void RemoveScene(Scene scene)
        {
            Scenes.Remove(scene);
            scene.Dispose();
        }

        public void Draw()
        {
            foreach (Scene scene in Scenes)
            {
                scene.Draw();
            }
        }

        public void Update(GameTime time)
        {
            foreach (Scene scene in Scenes)
            {
                scene.Update(time);
            }
        }

        public void ClearScenes()
        {
            foreach (Scene scene in Scenes)
            {
                scene.Dispose();
            }
            Scenes.Clear();
        }

        public void SwitchScene(Scene scene)
        {
            ClearScenes();
            AddScene(scene);
        }
    }
}
