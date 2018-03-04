using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Dragonoscopy.Graphics
{
    public class Tile
    {
        public int ID { get; set; }
        public int SheetID { get; set; }
        public bool Solid { get; set; }

        public Tile(int tileID, int sheetID, bool solid = false)
        {
            ID = tileID;
            SheetID = sheetID;
            Solid = solid;
        }
    }
}
