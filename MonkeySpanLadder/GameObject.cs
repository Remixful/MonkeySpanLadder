using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonkeySpanLadder
{
    class GameObject
    {
        static private int lastId { get; set; }
        public int Id { get; set; }
        private readonly List<Component> components;

        public GameObject()
        {
            this.components = new List<Component>();
            this.Id = lastId++;
            GameObjectManager.AddGameObject(this);
        }

        public TComponentType GetComponent<TComponentType>(ComponentType componentType) where TComponentType : Component
        {
            return components.Find(c => c.ComponentType == componentType) as TComponentType;
        }

        public void AddComponent(Component component)
        {
            this.components.Add(component);
            component.Initialize(this);
        }

        public void RemoveComponent(Component component)
        {
            this.components.Remove(component);
        }

        public void Update(GameTime gameTime)
        {
            foreach (Component component in components)
            {
                component.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Component component in components)
            {
                component.Draw(spriteBatch);
            }
        }
    }
}
