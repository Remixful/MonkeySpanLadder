using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonkeySpanLadder
{
    public enum ComponentType
    {
        Sprite,
        Button,
        Timer,
        Text
    }

    abstract class Component
    {
        private GameObject gameObject;

        public void Initialize(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        public abstract ComponentType ComponentType { get; }

        public void Remove()
        {
            gameObject.RemoveComponent(this);
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
