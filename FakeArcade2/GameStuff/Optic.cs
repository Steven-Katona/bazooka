using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeArcade2.GameStuff
{
    internal class Optic
    {
        Animation myVisual;
        public bool immobile {get; set; }
        protected Vector2 myLocation;
        public Hitbox myAABB { get; set; }
        AnimationLogic animator;
        public Optic(Animation visual, Hitbox aabb, bool immobile, Vector2 myLocation)
        {
            myVisual = visual;
            this.immobile = immobile;
            this.myLocation = myLocation;
            myAABB = aabb;
            animator = new AnimationLogic();
            animator.animationPlay(myVisual);
        }


        

        public Vector2 getPosition()
        {
            return myLocation;
        }

        

        public void Draw(GameTime _gameTime, SpriteBatch _spriteBatch)
        {
            if (myVisual.myAnimation != null) 
            {
                animator.Draw(_gameTime, _spriteBatch, getPosition(), myAABB.myCenter, SpriteEffects.None);
            }
            else
            {
                throw new Exception("something is not right");
            }
        }

        public void moveMe(float x, float y)
        {
            myLocation.X += x;
            myLocation.Y += y;
        }


        static public double CalculateDiagonalMovement(int the_move)
        {
            double amount = the_move / 2;
            amount = Math.Sqrt(amount);
            return amount;
        }
    }
}
