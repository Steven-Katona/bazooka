using Microsoft.Xna.Framework;
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
        SpriteEffect myEffects;
        public float rotate { get; set; }
        Point offset;

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
            currentDrawnTexture = animation.myAnimation;
            frameIndex = this.animation.startFrame;
            time = 0;
            animationEnd = false;

        }

        public void setDrawPriority(float value)
        {
            draw_priority = value;
        }

        public void setEffects(SpriteEffect effect)
        {
            this.myEffects = effect;
        }

        public void setOffset(Point offset) { this.offset = offset; }

        public void Draw(GameTime _gameTime, SpriteBatch _spriteBatch, Vector2 position, Rectangle mySource, Vector2 center, SpriteEffects _spriteEffects )
        {
            if(currentDrawnTexture == null)
            {
                throw new NotSupportedException("No animation is present at draw time!");
            }

            time += (float)_gameTime.ElapsedGameTime.TotalSeconds;

            while (time > animation.frameTime)
            {
                if (animation.isLooping)
                {
                    frameIndex = (frameIndex + 1) % (animation.myAnimation.Width / animation.myWidth);
                }
                else
                {
                    frameIndex = Math.Min((frameIndex + 1) % (animation.myAnimation.Width / animation.myWidth), (animation.myAnimation.Width / animation.myWidth - 1));

                    if (frameIndex == animation.myAnimation.Width / animation.myWidth - 1)
                    { animationEnd = true; }
                }
                time = 0;
            }

            
            

            Rectangle source = new(animation.myWidth * frameIndex, 0, animation.myWidth, animation.myHeight);
            _spriteBatch.Draw(currentDrawnTexture, new Rectangle((int)position.X + offset.X, (int)position.Y + offset.Y, mySource.Width, mySource.Height), source, Color.White, rotate, center,_spriteEffects,draw_priority);
            //_spriteBatch.Draw()
            //_spriteBatch.Draw(currentDrawnTexture, new Rectangle((int)position.X,(int)position.Y,mySource.Width,mySource.Height), Color.Wheat);
  
        }

    }


}
