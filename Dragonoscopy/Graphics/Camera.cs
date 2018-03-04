using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dragonoscopy.Global;
using Dragonoscopy.Math;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dragonoscopy.Graphics
{
    public class Camera
    {
        public Transform Transform { get; }

        public Camera()
        {
            Transform = new Transform();
        }

        public void CenterOn(Transform t)
        {
            Transform.Position = t.Position -
                                 new Vector2(Settings.VIRTUAL_VIEWPORT_WIDTH / 2, Settings.VIRTUAL_VIEWPORT_HEIGHT / 2);
        }
    }
}
