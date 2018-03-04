using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragonoscopy.Graphics
{
    public class TileLayer
    {
        public Tile[,] Tiles;
        public string Name { get; }
        public int Width { get; }
        public int Height { get; }
        public float Depth { get; }

        public TileLayer(string name, int width, int height, float depth = 0f)
        {
            Name = name;
            Width = width;
            Height = height;
            Tiles = new Tile[width, height];
            fillWith(new Tile(0, 0));
            Depth = depth;
        }

        public Tile this[int x, int y]
        {
            get => Tiles[x, y];
            set => Tiles[x, y] = value;
        }

        private void fillWith(Tile tile)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Tiles[x, y] = tile;
                }
            }
        }
    }
}
