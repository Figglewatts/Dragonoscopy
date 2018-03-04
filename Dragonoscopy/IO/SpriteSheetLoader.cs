using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dragonoscopy.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SimpleJSON;

namespace Dragonoscopy.IO
{
    public static class SpriteSheetLoader
    {
        public static SpriteSheet LoadFromJSON(JSONNode json, ContentManager content)
        {
            string image = json["image"];
            if (image.StartsWith("../")) image = image.Substring(3);
            string path = Path.GetDirectoryName(image);
            string filename = Path.GetFileNameWithoutExtension(image);
            Texture2D tex = content.Load<Texture2D>($"{path}/{filename}");
            int spriteWidth = json["tilewidth"];
            int spriteHeight = json["tileheight"];
            JSONNode properties = json["tileproperties"];
            return new SpriteSheet(tex, spriteWidth, spriteHeight, properties);
        }
    }
}
