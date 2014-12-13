using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonkeySpanLadder
{
    class RenderManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;

        #region Variables
        SpriteFont Arial;
        static public SpriteFont BoxArial;
        #endregion

        #region GameObjects
        static public GameObject Check;
        static public GameObject Incorrect;
        static public GameObject GameOver;
        static public GameObject Win;
        #endregion

        #region GUI
        GameObject Boxes;
        GameObject Meter;
        GameObject pointer;
        GameObject Score;
        GameObject panel;
        GameObject debugInfo;
        static public GameObject StartButton;
        static public List<Texture2D> TimerSprites;
        static public GameObject Timer;
        static public GameObject LivesCounter;
        static public List<Texture2D> LiveSprites;
        #endregion

        public RenderManager(Game game) : base(game) { }

        public override void Initialize()
        {
            Check = new GameObject();
            Incorrect = new GameObject();
            debugInfo = new GameObject();
            spriteBatch = new SpriteBatch(this.GraphicsDevice);
            panel = new GameObject();
            StartButton = new GameObject();
            Timer = new GameObject();
            TimerSprites = new List<Texture2D>() // I am really tired - Not sure what I'm doing
            {
                null, null, null
            };
            LiveSprites = new List<Texture2D>() // still really really tired.. getting more tired by the minute
            {
                null, null, null, null
            };
            LivesCounter = new GameObject();
            Score = new GameObject();
            Meter = new GameObject();
            pointer = new GameObject();
            Boxes = new GameObject();
            GameOver = new GameObject();
            Win = new GameObject();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Texture2D temp; //temporary sprite for loading content
            temp = Game.Content.Load<Texture2D>("Sprites/squareunlit");
            BoxManager.UnlitTexture = temp;
            temp = Game.Content.Load<Texture2D>("Sprites/squarelit");
            BoxManager.LitTexture = temp;
            temp = Game.Content.Load<Texture2D>("GUI/panelgui");
            panel.AddComponent(new Sprite(temp, temp.Width, temp.Height, new Vector2(875, 0)));
            temp = Game.Content.Load<Texture2D>("GUI/startbutton");
            StartButton.AddComponent(new Sprite(temp, temp.Width, temp.Height, new Vector2((825 / 2) - (temp.Width / 2), (Game.Window.ClientBounds.Height / 2) - (temp.Height / 2))));
            StartButton.AddComponent(new Button(temp.Width, temp.Height, StartButton.GetComponent<Sprite>(ComponentType.Sprite).Position));
            //Timer
            for (int i = 0; i < TimerSprites.Count; i++)
            {
                string location = "GUI/Timer/timer" + (i + 1);
                TimerSprites[i] = Game.Content.Load<Texture2D>(location);
            } 
            Timer.AddComponent(new Sprite(TimerSprites[2], TimerSprites[2].Width, TimerSprites[2].Height, new Vector2((825 / 2) - TimerSprites[2].Width / 2, Game.Window.ClientBounds.Height / 2 - TimerSprites[2].Height / 2), false));
            Timer.AddComponent(new Timer());
            ///Live Counter
            for (int i = 0; i < LiveSprites.Count; i++)
            {
                string location = "GUI/Lives/lives" + i;
                LiveSprites[i] = Game.Content.Load<Texture2D>(location);
            }
            LivesCounter.AddComponent(new Sprite(LiveSprites[3], LiveSprites[3].Width, LiveSprites[3].Height, new Vector2(903f, 148f)));
            Arial = Game.Content.Load<SpriteFont>("Arial");
            BoxArial = Game.Content.Load<SpriteFont>("BoxArial");
            //debugInfo.AddComponent(new Text(Arial, Vector2.Zero, Color.White, "Test"));
            temp = Game.Content.Load<Texture2D>("Sprites/Check");
            Check.AddComponent(new Sprite(temp, temp.Width, temp.Height, new Vector2((825 / 2) - temp.Width / 2, Game.Window.ClientBounds.Height / 2 - temp.Height / 2), false));
            temp = Game.Content.Load<Texture2D>("Sprites/Incorrect");
            Incorrect.AddComponent(new Sprite(temp, temp.Width, temp.Height, new Vector2((825 / 2) - temp.Width / 2, Game.Window.ClientBounds.Height / 2 - temp.Height / 2), false));
            Score.AddComponent(new Text(BoxArial, new Vector2(887, 251), Color.Yellow, "0"));
            temp = Game.Content.Load<Texture2D>("GUI/Meter");
            Meter.AddComponent(new Sprite(temp, temp.Width, temp.Height, new Vector2(985, 253)));
            Boxes.AddComponent(new Text(Arial, new Vector2(1017, 435), Color.White, "0"));
            temp = Game.Content.Load<Texture2D>("GUI/pointer");
            pointer.AddComponent(new Sprite(temp, temp.Width, temp.Height, new Vector2(1013, 406)));
            temp = Game.Content.Load<Texture2D>("Sprites/lostMessage");
            GameOver.AddComponent(new Sprite(temp, temp.Width, temp.Height, new Vector2(Game.Window.ClientBounds.Width / 2 - (temp.Width / 2), Game.Window.ClientBounds.Height / 2 - (temp.Height / 2)), false));
            temp = Game.Content.Load<Texture2D>("sprites/wonMessage");
            Win.AddComponent(new Sprite(temp, temp.Width, temp.Height, new Vector2(Game.Window.ClientBounds.Width / 2 - (temp.Width / 2), Game.Window.ClientBounds.Height / 2 - (temp.Height / 2)), false));
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            string text = String.Format("Boxes: {0}\nAmount: {1}", BoxManager.boxes.Count, GameManager.Boxes);
            Score.GetComponent<Text>(ComponentType.Text).SetText(GameManager.Score.ToString());
            //1025
            if(GameManager.Boxes >= 10)
            {
                Boxes.GetComponent<Text>(ComponentType.Text).Move(new Vector2(1010, 435));
            }
            Boxes.GetComponent<Text>(ComponentType.Text).SetText(GameManager.Boxes.ToString());
            Vector2 pointerPOS = new Vector2(1013, 406);
            if (GameManager.Boxes <= 25)
            {
                pointerPOS = new Vector2(1013, 406 - ((GameManager.Boxes - 1) * 6));
            }
            pointer.GetComponent<Sprite>(ComponentType.Sprite).SetPosition(pointerPOS);
            //debugInfo.GetComponent<Text>(ComponentType.Text).SetText(text);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (GameObject i in GameObjectManager.GameObjects)
            {
                if (i != null)
                {
                    i.Draw(spriteBatch);
                }
            }
            
            base.Draw(gameTime);
        }
    }
}
