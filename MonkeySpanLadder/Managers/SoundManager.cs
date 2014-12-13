using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace MonkeySpanLadder
{
    class SoundManager : Microsoft.Xna.Framework.GameComponent
    {
        public enum Sounds
        {
            Start,
            TimerBeep1,
            TimerBeep2,
            GuessingStart,
            BoxesLoaded,
            BoxClick,
            Error,
            Correct,
            CookieMonster,
            Lost
        }

        SoundEffect SE;
        static SoundEffectInstance Start;
        static SoundEffectInstance TimerBeep1;
        static SoundEffectInstance TimerBeep2;
        static SoundEffectInstance GuessingStart;
        static SoundEffectInstance BoxesLoaded;
        static SoundEffectInstance BoxClick;
        static SoundEffectInstance Error;
        static SoundEffectInstance Correct;
        static SoundEffectInstance CookieMonster;
        static SoundEffectInstance Lost;

        public SoundManager(Game game) : base(game) { }

        public override void Initialize()
        {
            SE = Game.Content.Load<SoundEffect>("Audio/Start");
            Start = SE.CreateInstance();
            SE = Game.Content.Load<SoundEffect>("Audio/TimerBeep1");
            TimerBeep1 = SE.CreateInstance();
            SE = Game.Content.Load<SoundEffect>("Audio/TimerBeep2");
            TimerBeep2 = SE.CreateInstance();
            SE = Game.Content.Load<SoundEffect>("Audio/BoxesLoaded");
            BoxesLoaded = SE.CreateInstance();
            SE = Game.Content.Load<SoundEffect>("Audio/GuessingStart");
            GuessingStart = SE.CreateInstance();
            SE = Game.Content.Load<SoundEffect>("Audio/BoxClick");
            BoxClick = SE.CreateInstance();
            SE = Game.Content.Load<SoundEffect>("Audio/Error");
            Error = SE.CreateInstance();
            SE = Game.Content.Load<SoundEffect>("Audio/Correct");
            Correct = SE.CreateInstance();
            SE = Game.Content.Load<SoundEffect>("Audio/CookieMonster");
            CookieMonster = SE.CreateInstance();
            SE = Game.Content.Load<SoundEffect>("Audio/Lost");
            Lost = SE.CreateInstance();
            base.Initialize();
        }

        static public void PlaySound(Sounds sound)
        {
            switch(sound)
            {
                case Sounds.Start:
                    if(Start.State != SoundState.Playing)
                    {
                        Start.Play();
                    }
                    break;
                case Sounds.TimerBeep1:
                    if (TimerBeep1.State != SoundState.Playing)
                    {
                        TimerBeep1.Play();
                    }
                    break;
                case Sounds.TimerBeep2:
                    if (TimerBeep2.State != SoundState.Playing)
                    {
                        TimerBeep2.Play();
                    }
                    break;
                case Sounds.GuessingStart:
                    if (GuessingStart.State != SoundState.Playing)
                    {
                        GuessingStart.Play();
                    }
                    break;
                case Sounds.BoxesLoaded:
                    BoxesLoaded.Play();
                    break;
                case Sounds.BoxClick:
                    if (BoxClick.State != SoundState.Playing)
                    {
                        BoxClick.Play();
                    }
                    break;
                case Sounds.Error:
                    if (BoxClick.State != SoundState.Playing)
                    {
                        Error.Play();
                    }
                    break;
                case Sounds.Correct:
                    if (BoxClick.State != SoundState.Playing)
                    {
                        Correct.Play();
                    }
                    break;
                case Sounds.CookieMonster:
                    if (CookieMonster.State != SoundState.Playing)
                    {
                        CookieMonster.Play();
                    }
                    break;
                case Sounds.Lost:
                    if(Lost.State != SoundState.Playing)
                    {
                        Lost.Play();
                    }
                    break;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
