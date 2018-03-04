using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dragonoscopy.Math;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dragonoscopy.Graphics
{
    public class TileMap
    {
        private List<SpriteSheet> _tileSheets;
        private List<TileLayer> _layers;

        public int Width { get; }
        public int Height { get; }
        public int SquaresDown { get; set; }
        public int SquaresAcross { get; set; }

        public TileMap(SpriteSheet tileSheet, int width, int height)
        {
            _tileSheets = new List<SpriteSheet> {tileSheet};
            _layers = new List<TileLayer>();

            Width = width;
            Height = height;
            _layers.Add(new TileLayer("default", width, height));
            SquaresDown = 10;
            SquaresAcross = 11;
        }

        public TileMap(int width, int height)
        {
            _tileSheets = new List<SpriteSheet>();
            _layers = new List<TileLayer>();
            Width = width;
            Height = height;
            SquaresDown = 16;
            SquaresAcross = 16;
        }

        public void AddLayer(TileLayer layer)
        {
            _layers.Add(layer);
        }

        public void AddSheet(SpriteSheet sheet)
        {
            _tileSheets.Add(sheet);
        }

        public TileLayer this[int layer] => _layers[layer];

        public TileLayer this[string layerName] => GetLayerWithName(layerName);

        public TileLayer GetLayerWithName(string name)
        {
            return _layers.FirstOrDefault(layer => layer.Name.Equals(name));
        }

        public void Draw(SpriteBatch batch, Vector2 camPos)
        {
            int tileWidth = (int)_tileSheets[0].SpriteDimensions.X;
            int tileHeight = (int)_tileSheets[0].SpriteDimensions.Y;
            int firstSquareX = (int)camPos.X / tileWidth;
            int firstSquareY = (int)camPos.Y / tileHeight;

            foreach (var layer in _layers)
            {
                for (int y = firstSquareY; y < SquaresDown + firstSquareY; y++)
                {
                    for (int x = firstSquareX; x < SquaresAcross + firstSquareX; x++)
                    {
                        if (x < 0 || x >= Width || y < 0 || y >= Height)
                        {
                            continue;
                        }

                        Tile tile = layer[x, y];

                        if (tile.ID == 0) continue;

                        _tileSheets[tile.SheetID].GetSprite(tile.ID - 1, layer.Depth).Draw(batch, x * tileWidth, y * tileHeight);
                    }
                }
            }
        }

        public bool CheckIsSolid(int x, int y)
        {
            foreach (TileLayer layer in _layers)
            {
                if (layer[x, y].Solid)
                    return true;
            }
            return false;
        }
    }
}
