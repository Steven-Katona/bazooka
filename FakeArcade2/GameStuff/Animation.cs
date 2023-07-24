using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeArcade2.GameStuff
{
    internal class Animation
    {
        public Texture2D[] myAnimation { get; set; }
        public Texture2D staticAnimation { get; set; }
        public float frameTime { get; set; }
        public bool isLooping { get; set; }

        public Animation(Texture2D[] animationFrames, float frameTime, bool isLooping) 
        { 
            myAnimation = animationFrames;
            this.frameTime = frameTime;
            this.isLooping = isLooping;
        }

    }
}
