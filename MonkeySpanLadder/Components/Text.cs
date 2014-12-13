using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonkeySpanLadder
{
    class Text : Component
    {
        string text;
        SpriteFont spriteFont;
        Vector2 position;
        Color fontColor;
        public Vector2 Size;
        

        public override ComponentType ComponentType
        {
            get { return ComponentType.Text; }
        }

        public Text(SpriteFont spriteFont, Vector2 position, Color color, string text)
        {
            this.spriteFont = spriteFont;
            this.position = position;
            this.fontColor = color;
            this.text = text;
            this.Size = spriteFont.MeasureString(text);
        }

        public void SetText(string text)
        {
            this.text = text;
        }

        public void Move(Vector2 position)
        {
            this.position = position;
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, text, position, fontColor);
            spriteBatch.End();
        }
    }
}
