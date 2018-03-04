using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dragonoscopy.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dragonoscopy.Game.Entities
{
    public class Player : GameObject
    {
        public Sprite Sprite;

        public Vector2 TilePosition => ((Transform.Position + new Vector2(16)) / 32) - Vector2.One;

        private TileMap _currentTileMap;

        public Player(Texture2D tex) : base("player")
        {
            Sprite = new Sprite(tex)
            {
                Origin = new Vector2(tex.Width / 2, tex.Height),
                Depth = 0.5f
            };
            Transform.Position = new Vector2(0, 100);
        }

        public override void Start()
        {
            
        }

        public override void Draw(SpriteBatch batch)
        {
            Sprite.Draw(batch, Transform);
        }

        public override void Update(double delta)
        {
            throw new NotImplementedException();
        }

        public void GoToTilePosition(int x, int y)
        {
            Transform.Position = new Vector2((x * 32) - 16, (y * 32) - 16);
        }

        public void TranslateTiles(int x, int y)
        {
            Vector2 tilePos = TilePosition;
            int newX = (int)tilePos.X + x;
            int newY = (int)tilePos.Y + y;
            if (_currentTileMap.CheckIsSolid(newX, newY)) return;
            GoToTilePosition((int)tilePos.X + x + 1, (int)tilePos.Y + y + 1);
        }

        public void UpdateTileMap(TileMap map)
        {
            _currentTileMap = map;
        }
    }
}
