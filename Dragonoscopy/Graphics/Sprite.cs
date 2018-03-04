using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dragonoscopy.Game;
using Dragonoscopy.Math;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dragonoscopy.Graphics
{
    public class Sprite
    {
        public enum OriginType
        {
            BottomLeft = 0,
            BottomRight,
            TopLeft,
            TopRight,
            Center,
            CenterPixel
        }

        public Texture2D Texture { get; set; }
        public Color Color { get; set; }
        public Vector2 Origin { get; set; }
        public Rectangle? Region { get; set; }
        public float Depth { get; set; }

        public Sprite(Texture2D tex, OriginType type = OriginType.BottomLeft, Color? tint = null, float depth = 0f)
        {
            Texture = tex;
            Color = tint ?? Color.White;
            SetOrigin(type);
            Depth = depth;
        }

        public Sprite(Texture2D tex, Rectangle region, OriginType type = OriginType.TopLeft, Color? tint = null, float depth = 0f)
            : this(tex, type, tint)
        {
            Region = region;
            Depth = depth;
        }

        public void Draw(SpriteBatch batch, Transform transform)
        {
            batch.Draw(Texture, transform.Position, Region, Color, transform.Angle, Origin, transform.Scale,
                SpriteEffects.None, Depth);
        }

        public void Draw(SpriteBatch batch, int x, int y)
        {
            batch.Draw(Texture, new Vector2(x, y), Region, Color, 0, Origin, Vector2.One,
                SpriteEffects.None, Depth);
        }

        public void SetOrigin(OriginType type)
        {
            switch (type)
            {
                case OriginType.BottomLeft:
                {
                    Origin = new Vector2(0, Texture.Height);
                    break;
                }
                case OriginType.BottomRight:
                {
                    Origin = new Vector2(Texture.Width, Texture.Height);
                    break;
                }
                case OriginType.TopRight:
                {
                    Origin = new Vector2(Texture.Width, 0);
                    break;
                }
                case OriginType.Center:
                {
                    Origin = new Vector2((float)Texture.Width / 2, (float)Texture.Height / 2);
                    break;
                }
                case OriginType.CenterPixel:
                {
                    Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
                    break;
                }
                default:
                {
                    Origin = Vector2.Zero;
                    break;
                }
            }
        }
    }
}
