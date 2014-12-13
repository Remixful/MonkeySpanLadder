using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonkeySpanLadder
{
    class Button : Component
    {
        Vector2 Position;
        int Width;
        int Height;

        public override ComponentType ComponentType
        {
            get { return ComponentType.Button; }
        }

        public Button(int width, int height, Vector2 position)
        {
            this.Width = width;
            this.Height = height;
            this.Position = position;
        }

        public bool Click(MouseState mouse)
        {
            if (mouse.Position.X <= Width + Position.X && mouse.Position.X >= Position.X && mouse.Position.Y <= Height + Position.Y && mouse.Position.Y >= Position.Y)
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    return true;
                }
            }
            return false;
        }

        public bool MouseHover(MouseState mouse)
        {
            if (mouse.Position.X <= Width + Position.X && mouse.Position.X >= Position.X && mouse.Position.Y <= Height + Position.Y && mouse.Position.Y >= Position.Y)
            {
                    return true;
            }
            return false;
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
