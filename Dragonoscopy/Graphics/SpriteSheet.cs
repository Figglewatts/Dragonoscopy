using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SimpleJSON;

namespace Dragonoscopy.Graphics
{
    public class SpriteSheet
    {
        public Texture2D Texture { get; }
        public Vector2 SpriteDimensions { get; }
        public JSONNode Properties { get; }

        public int NumberOfSprites
        {
            get
            {
                int across = Texture.Width / (int) SpriteDimensions.X;
                int down = Texture.Height / (int) SpriteDimensions.Y;
                return across * down;
            }
        }

        public SpriteSheet(Texture2D texture, int spriteWidth, int spriteHeight, JSONNode properties = null)
        {
            Texture = texture;
            SpriteDimensions = new Vector2(spriteWidth, spriteHeight);
            Properties = properties;
        }

        public Sprite GetSprite(int index, float depth = 0f)
        {
            int tileY = index / (Texture.Width / (int)SpriteDimensions.X);
            int tileX = index % (Texture.Width / (int)SpriteDimensions.X);
            return new Sprite(Texture, 
                new Rectangle(tileX * (int)SpriteDimensions.X, 
                    tileY * (int)SpriteDimensions.Y,
                    (int)SpriteDimensions.X,
                    (int)SpriteDimensions.Y), depth:depth);
        }
    }
}
