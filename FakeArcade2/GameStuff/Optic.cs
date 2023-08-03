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
        public enum Collision : ushort
        {
            None = 0,
            Danger = 1,
            Solid = 2,
            Jump = 3,
            Sturdy = 4,
            End = 5,
            Safe = 6,
            Key = 7
        };
        public bool remove { get; set; }
        public bool immobile {get; set; }
        public bool is_dead { get; set; }
        protected Vector2 myLocation;
        public Hitbox myAABB { get; set; }
        protected AnimationLogic animator;
        public Collision collisionBehavior { get; set; }
        public bool draw_me = true;
        public Optic(Animation visual, Hitbox aabb, bool immobile, Vector2 myLocation, bool is_dead = false)
        {
            myVisual = visual;
            this.immobile = immobile;
            this.myLocation = myLocation;
            myAABB = aabb;
            animator = new AnimationLogic();
            animator.animationPlay(myVisual);
            collisionBehavior = (Collision)myAABB.myBehavior;
            this.is_dead = is_dead;
        }

        public Vector2 getPosition()
        {
            return myLocation;
        }

        public void setToRemove()
        {
            remove = true;
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
            myAABB.getBounds().X += (int)x;
            myAABB.getBounds().Y += (int)y;
        }

        public void setPostion(int newX, int newY)
        {
            myLocation.X = newX;
            myLocation.Y = newY;
            myAABB.getBounds().X = newX;
            myAABB.getBounds().Y = newY;
        }

        public Vector2 getVector()
        { return myLocation; }


        static public double CalculateDiagonalMovement(double the_move)
        {
            double amount = the_move / 2;
            amount = Math.Sqrt(amount);
            return amount;
        }
    }
}
