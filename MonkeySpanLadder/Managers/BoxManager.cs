using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonkeySpanLadder
{
    public enum BoxState
    {
        Lit,
        Unlit
    }
    class BoxManager
    {
        static public Texture2D LitTexture;
        static public Texture2D UnlitTexture;
        static public Grid BoxGrid = new Grid(8, 11, 75, 65);
        static public readonly List<Box> boxes = new List<Box>();
        static Random rdmNum = new Random();
        static FrameClass fade = new FrameClass(60);

        static private void AddBox(Point gridPosition, int number)
        {
            boxes.Add(new Box(gridPosition, LitTexture, UnlitTexture, number));
        }

        static public void RemoveBox(Box box)
        {
            GameObjectManager.RemoveGameObject(box);
            boxes.Remove(box);
        }

        static public void Clear()
        {
            for (int i = 0; i < boxes.Count; i++ )
            {
                GameObjectManager.RemoveGameObject(boxes[i]);
            }
                boxes.Clear();
        }

        static public void RemoveNumbers()
        {
            for(int i = 0; i < boxes.Count; i++)
            {
                boxes[i].HideNumber();
            }
        }


        static public void ChangeState(BoxState boxState)
        {
            if (boxState == BoxState.Lit)
            {
                foreach(Box i in boxes)
                {
                    i.BoxState = BoxState.Lit;
                    i.UpdateTexture();
                }
            }
            else if (boxState == BoxState.Unlit)
            {
                foreach(Box i in boxes)
                {
                    i.BoxState = BoxState.Unlit;
                    i.UpdateTexture();
                }
            }
        }

        static public void ChangeState(Box box, BoxState boxState)
        {
            box.BoxState = boxState;
            box.UpdateTexture();
        }

        static public void CheckClick(GameTime gameTime)
        {
            for (int i = 0; i < boxes.Count; i++ )
            {
                if (boxes[i].GetComponent<Button>(ComponentType.Button).MouseHover(Mouse.GetState()) && !boxes[i].hidingBox)
                {
                    ChangeState(boxes[i], BoxState.Lit);
                }
                else
                {
                    ChangeState(boxes[i], BoxState.Unlit);
                }
                if (boxes[i].GetComponent<Button>(ComponentType.Button).Click(Mouse.GetState()) && !boxes[i].hidingBox)
                {
                    SoundManager.PlaySound(SoundManager.Sounds.BoxClick);
                    boxes[i].hidingBox = true;
                }

                if(boxes[i].hidingBox)
                {
                    fade.UpdateFrames(gameTime);

                    if (fade.HitMaxFrames())
                    {
                        if (boxes[i].GetComponent<Sprite>(ComponentType.Sprite).GetTransparency() > 0)
                        {
                            boxes[i].GetComponent<Sprite>(ComponentType.Sprite).SetTransparency(boxes[i].GetComponent<Sprite>(ComponentType.Sprite).GetTransparency() - .4f);
                        }
                        if (boxes[i].GetComponent<Sprite>(ComponentType.Sprite).GetTransparency() <= 0)
                        {
                            GameManager.ClickedBoxes.Add(boxes[i]);
                            boxes[i].hidingBox = false;
                            RemoveBox(boxes[i]);
                        }
                    }
                }
            }
        }

        static public void GenerateBoxes(int amount)
        {
            Clear();
            for (int i = 1; i <= amount; i++)
            {
                bool foundSpot = false;
                Point randomGridPos = new Point(0,0);
                while (!foundSpot)
                {
                    randomGridPos = new Point(rdmNum.Next(0, BoxGrid.Columns), rdmNum.Next(0, BoxGrid.Rows));
                    List<Box> checkedBoxes = new List<Box>();
                    foreach(Box box in boxes)
                    {
                        if(box.GridPosition != randomGridPos)
                        {
                            checkedBoxes.Add(box);
                        }
                    }
                    if(checkedBoxes.Count == boxes.Count && !foundSpot)
                    {
                        foundSpot = true;
                    }
                }
                AddBox(randomGridPos, i);
            }
        }
    }

    class Box : GameObject
    {
        public Point GridPosition { get; set; }
        public Vector2 Position;
        public int Number { get; set; }
        public BoxState BoxState { get; set; }
        public bool hidingBox = false;

        public Box(Point gridPosition, Texture2D LitTexture, Texture2D UnlitTexture, int number)
        {
            this.Number = number;
            this.GridPosition = gridPosition;
            this.BoxState = MonkeySpanLadder.BoxState.Unlit;
            this.Position = BoxManager.BoxGrid.GetGridPosition(gridPosition);
            this.AddComponent(new Button(LitTexture.Width, LitTexture.Height, Position));
            this.AddComponent(new Sprite(UnlitTexture, UnlitTexture.Width, UnlitTexture.Height, Position));
            this.AddComponent(new Text(RenderManager.BoxArial, Vector2.Zero, Color.White, number.ToString()));
            this.GetComponent<Text>(ComponentType.Text).Move(new Vector2((Position.X + LitTexture.Width / 2) - (GetComponent<Text>(ComponentType.Text).Size.X / 2), (Position.Y + LitTexture.Height / 2) - ((GetComponent<Text>(ComponentType.Text).Size.Y / 2) - 5)));
        }

        public void HideNumber()
        {
            this.RemoveComponent(this.GetComponent<Text>(ComponentType.Text));
        }


        public void UpdateTexture()
        {
            switch(BoxState)
            {
                case MonkeySpanLadder.BoxState.Lit:
                    this.GetComponent<Sprite>(ComponentType.Sprite).ChangeTexture(BoxManager.LitTexture);
                break;
                case MonkeySpanLadder.BoxState.Unlit:
                this.GetComponent<Sprite>(ComponentType.Sprite).ChangeTexture(BoxManager.UnlitTexture);
                break;
            }

        }
    }
}
