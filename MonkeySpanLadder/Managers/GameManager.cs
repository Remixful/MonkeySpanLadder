using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonkeySpanLadder
{
    public enum Scene {WaitingForStart, Timer, InGame, GameOver, PressedStart}

    class GameManager : Microsoft.Xna.Framework.GameComponent
    {
        #region Variables
        static public Scene CurrentScene;
        FrameClass timerFC;
        public int Lives { get; set; }
        static bool Won { get; set; }
        static public int Boxes { get; set; }
        static public int Score { get; set; } //Most amount of boxes right in a Successful round.
        bool showingBoxes;
        bool timerBeep;
        bool waitingSecond;
        FrameClass waitSecond;
        static public List<Box> ClickedBoxes;
        #endregion
        public enum State { ShowingBoxes, GuessingOrder, Waiting, None }
        public State CurrentState;
        enum Condition { Failure, Success, None };
        Condition condition;

        public GameManager(Game game) : base(game) { }

        public override void Initialize()
        {
            waitSecond = new FrameClass(1000);
            waitingSecond = false;
            timerBeep = false;
            Won = false;
            Score = 0;
            condition = Condition.None;
            ClickedBoxes = new List<Box>();
            Lives = 3;
            Boxes = 2;
            showingBoxes = false;
            timerFC = new FrameClass(1000);
            CurrentScene = Scene.WaitingForStart;
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            //GAMEOBJECT UPDATE
            foreach (GameObject i in GameObjectManager.GameObjects)
            {
                if (i != null)
                {
                    i.Update(gameTime);
                }
            }
            /////////////



            switch(CurrentScene)
            {
                case Scene.WaitingForStart:
                    RenderManager.StartButton.GetComponent<Sprite>(ComponentType.Sprite).Active = true;
                    if (RenderManager.StartButton.GetComponent<Button>(ComponentType.Button).Click(Mouse.GetState()))
                    {
                        RenderManager.StartButton.GetComponent<Sprite>(ComponentType.Sprite).Active = false;
                        waitingSecond = true;
                        CurrentScene = Scene.PressedStart;
                    }
                    break;
                case Scene.PressedStart:

                    if (waitingSecond)
                    {
                        SoundManager.PlaySound(SoundManager.Sounds.Start);
                        waitSecond.UpdateFrames(gameTime);
                        if (waitSecond.HitMaxFrames())
                        {
                            CurrentScene = Scene.Timer;
                        }
                    }
                    break;
                case Scene.Timer:
                    RenderManager.Timer.GetComponent<Sprite>(ComponentType.Sprite).ChangeTexture(RenderManager.TimerSprites[2]);
                    if (RenderManager.Timer.GetComponent<Timer>(ComponentType.Timer).CurrentTimerState == Timer.TimerState.Off)
                    {
                        RenderManager.Timer.GetComponent<Timer>(ComponentType.Timer).StartTimer(timerFC, 4);
                    }
                    if (RenderManager.Timer.GetComponent<Timer>(ComponentType.Timer).Enabled)
                    {
                        if (RenderManager.Timer.GetComponent<Timer>(ComponentType.Timer).i == 0)
                        {
                            if(!timerBeep)
                            {
                                SoundManager.PlaySound(SoundManager.Sounds.TimerBeep1);
                                timerBeep = true;
                            }
                            RenderManager.Timer.GetComponent<Sprite>(ComponentType.Sprite).Active = true;
                        }
                        if (RenderManager.Timer.GetComponent<Timer>(ComponentType.Timer).i == 1)
                        {
                            if(timerBeep)
                            {
                                SoundManager.PlaySound(SoundManager.Sounds.TimerBeep1);
                                timerBeep = false;
                            }
                            RenderManager.Timer.GetComponent<Sprite>(ComponentType.Sprite).ChangeTexture(RenderManager.TimerSprites[1]);
                        }
                        if (RenderManager.Timer.GetComponent<Timer>(ComponentType.Timer).i == 2)
                        {
                            if (!timerBeep)
                            {
                                SoundManager.PlaySound(SoundManager.Sounds.TimerBeep2);
                                timerBeep = true;
                            }
                            RenderManager.Timer.GetComponent<Sprite>(ComponentType.Sprite).ChangeTexture(RenderManager.TimerSprites[0]);

                        }
                        if (RenderManager.Timer.GetComponent<Timer>(ComponentType.Timer).i == 3)
                        {
                            RenderManager.Timer.GetComponent<Sprite>(ComponentType.Sprite).Active = false;
                            CurrentScene = Scene.InGame;
                            CurrentState = State.Waiting;
                        }
                    }
                    break;

                case Scene.InGame:
                    Round(gameTime);
                    break;
                case Scene.GameOver:
                    RenderManager.Check.GetComponent<Sprite>(ComponentType.Sprite).Active = false;
                    RenderManager.Incorrect.GetComponent<Sprite>(ComponentType.Sprite).Active = false;
                    if (Won)
                    {
                        RenderManager.Win.GetComponent<Sprite>(ComponentType.Sprite).Active = true;
                    }
                    else
                    {
                        RenderManager.GameOver.GetComponent<Sprite>(ComponentType.Sprite).Active = true;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        if (Won)
                        {
                            RenderManager.Win.GetComponent<Sprite>(ComponentType.Sprite).Active = false;
                        }
                        else
                        {
                            RenderManager.GameOver.GetComponent<Sprite>(ComponentType.Sprite).Active = false;
                        }
                        Initialize();
                        CurrentScene = Scene.WaitingForStart;
                    }
                    break;
            }
            switch (Lives)
            {
                case 3:
                    RenderManager.LivesCounter.GetComponent<Sprite>(ComponentType.Sprite).ChangeTexture(RenderManager.LiveSprites[3]);
                    break;
                case 2:
                    RenderManager.LivesCounter.GetComponent<Sprite>(ComponentType.Sprite).ChangeTexture(RenderManager.LiveSprites[2]);
                    break;
                case 1:
                    RenderManager.LivesCounter.GetComponent<Sprite>(ComponentType.Sprite).ChangeTexture(RenderManager.LiveSprites[1]);
                    break;
                case 0:
                    RenderManager.LivesCounter.GetComponent<Sprite>(ComponentType.Sprite).ChangeTexture(RenderManager.LiveSprites[0]);
                    break;
            }

            base.Update(gameTime);
        }


        #region Variables
        FrameClass showBoxesWait;
        FrameClass waitBefore = new FrameClass(1200);
        bool playedSound;
        bool playedSound2;
        #endregion

        void Round(GameTime gameTime)
        {
            if (!showingBoxes && CurrentState == State.ShowingBoxes)
            {
                if(!playedSound)
                {
                    SoundManager.PlaySound(SoundManager.Sounds.BoxesLoaded);
                    playedSound = true;
                }
                Game.IsMouseVisible = false;
                showBoxesWait = new FrameClass(800 * Boxes);
                showingBoxes = true;
                BoxManager.GenerateBoxes(Boxes);
                BoxManager.ChangeState(BoxState.Lit);
            }
            if (showingBoxes && CurrentState == State.ShowingBoxes)
            {
                showBoxesWait.UpdateFrames(gameTime);
                if (showBoxesWait.HitMaxFrames())
                {
                    BoxManager.ChangeState(BoxState.Unlit);
                    BoxManager.RemoveNumbers();
                    CurrentState = State.GuessingOrder;
                    showingBoxes = false;
                    if (playedSound)
                    {
                        SoundManager.PlaySound(SoundManager.Sounds.GuessingStart);
                        playedSound = false;
                    }
                }
            }
            if(CurrentState == State.GuessingOrder)
            {
                Game.IsMouseVisible = true;
                BoxManager.CheckClick(gameTime);
                if(BoxManager.boxes.Count == 0)
                {
                    for(int i = 0; i < Boxes; i++)
                    {
                        if(ClickedBoxes[i].Number != i + 1)
                        {
                            Lives--;
                            if(Lives == 0)
                            {
                                SoundManager.PlaySound(SoundManager.Sounds.Lost);
                                CurrentScene = Scene.GameOver;
                                Won = false;
                                return;
                            }
                            condition = Condition.Failure;
                            break;
                        }
                    }
                    if(condition != Condition.Failure)
                    {
                        condition = Condition.Success;
                    }
                    ClickedBoxes.Clear();
                    CurrentState = State.Waiting;
                }
            }
            if(CurrentState == State.Waiting)
            {
                if (Boxes == 25)
                {
                    CurrentScene = Scene.GameOver;
                    SoundManager.PlaySound(SoundManager.Sounds.CookieMonster);
                    Won = true;
                    return;
                }
                switch (condition)
                {
                    case Condition.Success:
                        if (!playedSound2)
                        {
                            SoundManager.PlaySound(SoundManager.Sounds.Correct);
                            playedSound2 = true;
                        }
                        if (!RenderManager.Check.GetComponent<Sprite>(ComponentType.Sprite).Active)
                        {
                            RenderManager.Check.GetComponent<Sprite>(ComponentType.Sprite).Active = true;
                            if (Score != Boxes)
                            {
                                Score = Boxes;
                            }
                            Boxes++;
                        }
                        break;
                    case Condition.Failure:
                        if (!playedSound2)
                        {
                            SoundManager.PlaySound(SoundManager.Sounds.Error);
                            playedSound2 = true;
                        }
                        if (!RenderManager.Incorrect.GetComponent<Sprite>(ComponentType.Sprite).Active)
                        {
                            RenderManager.Incorrect.GetComponent<Sprite>(ComponentType.Sprite).Active = true;
                            if (Boxes > 2)
                            {
                                Boxes--;
                            }
                        }
                        break;
                    default:
                        break;
                }
                waitBefore.UpdateFrames(gameTime);
                if(waitBefore.HitMaxFrames())
                {
                    RenderManager.Check.GetComponent<Sprite>(ComponentType.Sprite).Active = false;
                    RenderManager.Incorrect.GetComponent<Sprite>(ComponentType.Sprite).Active = false;
                    condition = Condition.None;
                    CurrentState = State.ShowingBoxes;
                    playedSound2 = false;
                }

            }
        }

    }
}
