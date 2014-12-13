using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonkeySpanLadder
{
    class Timer : Component
    {
        FrameClass frames;
        public bool Enabled { get; set; }
        public int i;
        int timesRepeat;
        public TimerState CurrentTimerState { get; set; }
        public enum TimerState
        {
            Running,
            Off
        }

        public override ComponentType ComponentType
        {
            get { return ComponentType.Timer; }
        }
        
        public Timer()
        {
            this.frames = null;
            this.i = 1;
            this.timesRepeat = 0;
            this.CurrentTimerState = TimerState.Off;
            Enabled = true;
        }

        public void StartTimer(FrameClass frames, int timesRepeat)
        {
            i = 0;
            CurrentTimerState = TimerState.Off;
            this.frames = frames;
            this.Enabled = true;
            this.CurrentTimerState = TimerState.Running;
            this.timesRepeat = timesRepeat;
        }

        public override void Update(GameTime gameTime)
        {
            if (this.CurrentTimerState == TimerState.Running && frames != null)
            {
                frames.UpdateFrames(gameTime);
                if (frames.HitMaxFrames())
                {
                    i++;
                    if (i >= timesRepeat)
                    {
                        this.CurrentTimerState = TimerState.Off;
                        Enabled = false;
                        i = 0;
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
