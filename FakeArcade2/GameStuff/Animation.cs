using Microsoft.Xna.Framework;
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
        public Texture2D myAnimation { get; set; }
        public float frameTime { get; set; }
        public bool isLooping { get; set; }
        public int startFrame { get; set; }
        public int myHeight { get; set; }
        public int myWidth { get; set; }

        public Animation(Texture2D animationFrame, float frameTime, bool isLooping, int myHeight, int myWidth)
        {
            myAnimation = animationFrame;
            this.frameTime = frameTime;
            this.isLooping = isLooping;
            this.startFrame = startFrame;
            this.myHeight = myHeight;
            this.myWidth = myWidth;
        }

        public Animation(Animation animation)
        {
            myAnimation = animation.myAnimation;
            this.frameTime = animation.frameTime;
            this.isLooping = animation.isLooping;
            this.startFrame = animation.startFrame;
            this.myHeight = animation.myHeight;
            this.myWidth = animation.myWidth;
        }

    }
}
