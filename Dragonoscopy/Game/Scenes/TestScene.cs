using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dragonoscopy.Game.Entities;
using Dragonoscopy.Graphics;
using Dragonoscopy.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Dragonoscopy.Game.Scenes
{
    public class TestScene : Scene
    {
        private Player player;
        private Texture2D playerTex;
        private Texture2D tileSheetTex;
        private SpriteSheet tileSheet;
        private TileMap test;

        private KeyboardState _lastState;
        private KeyboardState _currentState;

        private const float CAM_MOVE_SPEED = 30f;

        public override void Start()
        {
            player = new Player(playerTex);
            player.GoToTilePosition(3, 3);
            tileSheet = new SpriteSheet(tileSheetTex, 32, 32);
            RegisterObject(player);
            player.UpdateTileMap(test);
        }

        public override void Update(GameTime time)
        {
            _lastState = _currentState;
            _currentState = Keyboard.GetState();

            if (_currentState.IsKeyDown(Keys.Up) && _lastState.IsKeyUp(Keys.Up))
            {
                player.TranslateTiles(0, -1);
            }
            else if (_currentState.IsKeyDown(Keys.Down) && _lastState.IsKeyUp(Keys.Down))
            {
                player.TranslateTiles(0, 1);
            }

            if (_currentState.IsKeyDown(Keys.Left) && _lastState.IsKeyUp(Keys.Left))
            {
                player.TranslateTiles(-1, 0);
            }
            else if (_currentState.IsKeyDown(Keys.Right) && _lastState.IsKeyUp(Keys.Right))
            {
                player.TranslateTiles(1, 0);
            }

            Camera.CenterOn(player.Transform);
        }

        public override void Draw()
        {
            SpriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, transformMatrix: Camera.Transform.InverseMatrix, samplerState: SamplerState.PointClamp);

            test.Draw(SpriteBatch, Camera.Transform.Position);

            foreach (GameObject o in Objects)
            {
                o.Draw(SpriteBatch);
            }

            SpriteBatch.End();
        }

        public override void LoadContent()
        {
            playerTex = Resources.Load<Texture2D>("sprites/char");
            test = Resources.Load<TileMap>("tilemaps/test_level1");
        }

        public override void Dispose()
        {
            
        }

        public TestScene(ContentManager content, GraphicsDevice device) : base(content, device)
        {
        }
    }
}
