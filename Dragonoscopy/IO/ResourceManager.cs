using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dragonoscopy.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SimpleJSON;

namespace Dragonoscopy.IO
{
    public enum ResourceLifespan
    {
        Scene,
        Global
    }

    public class GenericResource
    {
        public ResourceLifespan Lifespan;

        public GenericResource(ResourceLifespan lifespan)
        {
            Lifespan = lifespan;
        }
    }

    public class Resource<T> : GenericResource
    {
        public T Data;
        public Resource(T data, ResourceLifespan lifespan = ResourceLifespan.Global) : base(lifespan)
        {
            Data = data;
        }

        public Resource(ResourceLifespan lifespan = ResourceLifespan.Global) : base(lifespan)
        {
            // intentionally empty
        }
    }

    public class ResourceManager
    {
        private ContentManager _content;
        private Dictionary<string, GenericResource> _resources;

        public ResourceManager(ContentManager content)
        {
            _content = content;
            _resources = new Dictionary<string, GenericResource>();
        }

        public void ClearLifespan(ResourceLifespan span)
        {
            List<string> toRemove = new List<string>();
            foreach (var res in _resources)
            {
                if (res.Value.Lifespan == span)
                {
                    toRemove.Add(res.Key);
                }
            }

            foreach (string key in toRemove)
            {
                _resources.Remove(key);
            }
        }

        public bool CheckCache<T>(string path, out T data) where T : class
        {
            if (_resources.ContainsKey(path))
            {
                data = _resources[path] as T;
                return true;
            }
            data = null;
            return false;
        }
        
        public T Load<T>(string path, ResourceLifespan span = ResourceLifespan.Global)
        {
            if (CheckCache(path, out Resource<T> res)) return res.Data;

            if (typeof(T) == typeof(SpriteSheet))
            {
                string jsonText = _content.Load<String>(path);
                JSONNode node = JSON.Parse(jsonText);
                SpriteSheet sheet = SpriteSheetLoader.LoadFromJSON(node, _content);
                Resource<SpriteSheet> resource = new Resource<SpriteSheet>(sheet, span);
                _resources[path] = resource;
            }
            else if (typeof(T) == typeof(TileMap))
            {
                string jsonText = _content.Load<String>(path);
                JSONNode node = JSON.Parse(jsonText);
                TileMap map = TileMapLoader.LoadFromJSON(node, _content, this, span);
                Resource<TileMap> resource = new Resource<TileMap>(map, span);
                _resources[path] = resource;
            }
            else
            {
                Resource<T> resource = new Resource<T>(_content.Load<T>(path), span);
                _resources[path] = resource;
            }

            return ((Resource<T>)_resources[path]).Data;
        }
    }
}
