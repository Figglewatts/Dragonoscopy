using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dragonoscopy.Graphics
{
    public class PixelatedViewport
    {
        public Viewport Viewport { get; private set; }

        public Vector2 VirtualSize { get; }

        public Vector2 Size { get; }

        public RenderTarget2D PixelRenderTarget { get; }

        private readonly GraphicsDeviceManager _device;

        public PixelatedViewport(GraphicsDeviceManager deviceMgr, Vector2 virtualSize)
        {
            VirtualSize = virtualSize;
            _device = deviceMgr;

            PixelRenderTarget = new RenderTarget2D(_device.GraphicsDevice,
                (int)VirtualSize.X,
                (int)VirtualSize.Y,
                false,
                _device.GraphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24);

            Resize(new Vector2(_device.PreferredBackBufferWidth, _device.PreferredBackBufferHeight));
        }

        public void Resize(Vector2 size)
        {
            float aspect = VirtualSize.X / VirtualSize.Y;
            int width = (int) size.X;
            int height = (int) (width / aspect + 0.5f);

            if (height > size.Y)
            {
                height = (int)size.Y;
                width = (int) (height * aspect + 0.5f);
            }

            var x = (int)(size.X / 2) - (width / 2);
            var y = (int)(size.Y / 2) - (height / 2);

            Viewport = new Viewport(x, y, width, height);
            _device.GraphicsDevice.Viewport = Viewport;
        }
    }
}
