using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dragonoscopy.Graphics;
using Microsoft.Xna.Framework.Content;
using SimpleJSON;

namespace Dragonoscopy.IO
{
    public static class TileMapLoader
    {
        public static TileMap LoadFromJSON(JSONNode json, ContentManager content, ResourceManager manager, ResourceLifespan span)
        {
            JSONNode tilesets = json["tilesets"];
            List<SpriteSheet> sheets = new List<SpriteSheet>();
            int[] sheetLastIndices = new int[tilesets.Count];
            int i = 0;
            foreach (JSONNode tileset in tilesets)
            {
                string source = tileset["source"];
                if (source.StartsWith("../")) source = source.Substring(3);
                string path = Path.GetDirectoryName(source);
                string filename = Path.GetFileNameWithoutExtension(source);
                SpriteSheet sheet = manager.Load<SpriteSheet>(Path.Combine(path, filename), span);
                sheets.Add(sheet);
                sheetLastIndices[i] = sheet.NumberOfSprites + sheetLastIndices.Sum();

                i++;
            }

            int width = json["width"];
            int height = json["height"];

            TileMap tilemap = new TileMap(width, height);
            foreach (SpriteSheet sheet in sheets)
            {
                tilemap.AddSheet(sheet);
            }

            JSONNode layers = json["layers"];
            List<TileLayer> tileLayers = new List<TileLayer>();
            foreach (JSONNode layer in layers)
            {
                JSONNode layerProperties = layer["properties"];
                float depth = layerProperties["depth"];

                TileLayer tileLayer = new TileLayer(layer["name"], width, height, depth);

                JSONNode layerData = layer["data"];
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int tileID = layer["data"][y * width + x];
                        int sheetID = calculateSheetIndex(tileID, sheetLastIndices);
                        int tileSheetTileID = sheetID > 0 ? tileID - sheetLastIndices[sheetID - 1] : tileID;
                        bool isSolid = sheets[sheetID].Properties[(tileSheetTileID - 1).ToString()]["solid"];

                        tileLayer[x, y] = new Tile(tileSheetTileID, sheetID, isSolid);
                    }
                }

                tilemap.AddLayer(tileLayer);
            }

            return tilemap;
        }

        private static int calculateSheetIndex(int tileID, int[] sheetLastIndices)
        {
            int i = 0;
            foreach (int idx in sheetLastIndices)
            {
                if (tileID <= idx)
                {
                    return i;
                }
                i++;
            }

            return -1;
        }
    }
}
