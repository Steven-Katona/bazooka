﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeArcade2.GameStuff
{
    struct AnimationLogic
    {
        Animation animation { get; set; }
        public Texture2D currentDrawnTexture { get; set; }
        private int frameIndex;
        private float time;
        private float draw_priority;
        private bool animationEnd;
        public float rotate { get; set; }

        public bool is_animation_over()
        {
            return animationEnd;
        }

        public void animationPlay(Animation the_current_Animation)
        {
            if (animation == the_current_Animation)
            {
                return;
            }

            this.animation = the_current_Animation;
            frameIndex = this.animation.startFrame;
            time = 0;
            if (animation.myAnimation.Length == 1)
            {
                animation.staticAnimation = animation.myAnimation[0];
                currentDrawnTexture = animation.staticAnimation;
            }
            else
            {
                currentDrawnTexture = animation.myAnimation[0];
            }


            if(animation.myAnimation == null)
            {
                currentDrawnTexture = animation.staticAnimation;
            }

            animationEnd = false;

        }

        public void setDrawPriority(float value)
        {
            draw_priority = value;
        }

        public void Draw(GameTime _gameTime, SpriteBatch _spriteBatch, Vector2 position, Vector2 center, SpriteEffects _spriteEffects )
        {
            if(currentDrawnTexture == null)
            {
                throw new NotSupportedException("No animation is present at draw time!");
            }



            time += (float)_gameTime.ElapsedGameTime.TotalSeconds;

            if(animation.myAnimation.GetType() == typeof(Texture2D[]))
            { 
                while(time > animation.frameTime)
                {
                    if (animation.isLooping)
                    {
                        frameIndex = (frameIndex + 1) % animation.myAnimation.Length;
                        currentDrawnTexture = animation.myAnimation[frameIndex];
                    }
                    else
                    {
                        frameIndex = Math.Min((frameIndex + 1) % animation.myAnimation.Length, animation.myAnimation.Length - 1);
                        currentDrawnTexture = animation.myAnimation[frameIndex];

                        if (frameIndex == animation.myAnimation.Length - 1)
                        { animationEnd = true; }
                    }
                    time = 0;
                }
                
            }


            _spriteBatch.Draw(currentDrawnTexture, position, Color.White);
        }

    }


}
