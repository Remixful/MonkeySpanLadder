using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonkeySpanLadder
{
    class FrameClass
    {
        public int CurrentFrame { get; set; }
        public int MaxFrames { get; set; }

        public FrameClass(int maxFrames)
        {
            this.MaxFrames = maxFrames;
        }

        public void UpdateFrames(GameTime gameTime)
        {
            CurrentFrame += gameTime.ElapsedGameTime.Milliseconds;
        }

        public bool HitMaxFrames()
        {
            if (CurrentFrame >= MaxFrames)
            {
                CurrentFrame = 0;
                return true;
            }
            return false;
        }
    }
}
