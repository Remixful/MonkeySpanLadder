using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonkeySpanLadder
{
    class GameObjectManager
    {
        static public readonly List<GameObject> GameObjects = new List<GameObject>();

        static public void AddGameObject(GameObject gameObject)
        {
            GameObjects.Add(gameObject);
        }
        
        static public void RemoveGameObject(GameObject gameObject)
        {
            GameObjects.Remove(gameObject);
            gameObject = null;
        }
    }
}
