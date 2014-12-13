using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonkeySpanLadder
{
    class Sprite : Component
    {
        public Texture2D Texture;
        public int Width {get; set;}
        public int Height {get; set;}
        public Vector2 Position {get; private set;}
        public bool Active { get; set; }
        float transparency = 1;

        public Sprite(Texture2D texture, int width, int height, Vector2 position, bool active = true)
        {
            this.Texture = texture;
            this.Width = width;
            this.Height = height;
            this.Position = position;
            this.Active = active;
        }

        public override ComponentType ComponentType
        {
            get { return ComponentType.Sprite; }
        }
        
        public float GetTransparency()
        {
            return transparency;
        }

        public void SetTransparency(float t)
        {
            this.transparency = t;
        }

        public void SetPosition(Vector2 position)
        {
            this.Position = position;
        }

        public void ChangeTexture(Texture2D texture)
        {
            this.Texture = texture;
        }

        public override void Update(GameTime gameTime)
        {
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                spriteBatch.Draw(Texture, Position, null, Color.White * transparency, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                spriteBatch.End();
            }
        }
    }
}
