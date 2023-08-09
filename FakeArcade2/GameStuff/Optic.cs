using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace FakeArcade2.GameStuff
{
    internal class Optic
    {
        protected Animation myVisual;
        public enum Collision : ushort
        {
            None = 0,
            Danger = 1,
            Solid = 2,
            Jump = 3,
            Sturdy = 4,
            End = 5,
            Safe = 6,
            Key = 7,
            Checkpoint = 8,
            Enemy = 9,
            Stable = 10
        };
        public bool remove { get; set; }
        public bool immobile {get; set; }
        public bool is_dead { get; set; }
        public bool breakable { get; set; }
        public int trigger { get; set; }
        public int true_collision { get; set; }
        public int my_exit_code { get; set; }
        public bool appear_disappear { get; set; }
        public bool fog { get; set; }
        public bool suprise { get; set; }
  

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
            breakable = false;
            my_exit_code = 1;
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
            myAABB.set_Offset(myAABB.my_offset);
        }

        public Vector2 getVector()
        { return myLocation; }


        static public double CalculateDiagonalMovement(double the_move)
        {
            double amount = the_move / 2;
            amount = Math.Sqrt(amount);
            return amount;
        }

        public void Optic_behavior_alteration(int code, int my_collision,int my_trigger)
        {
            this.trigger = my_trigger;
            this.true_collision = my_collision;
            switch(code)
            {
                case 1:
                    draw_me = false;
                    break;
                case 2:
                    breakable = true;
                    break;
                case 3:
                    draw_me = false;
                    collisionBehavior = Collision.None;
                    break;
                case 4:
                    appear_disappear = true;
                    draw_me = false;
                    break;
                case 5:
                    appear_disappear = true;
                    break;
                case 6:
                    fog = true;
                    draw_me = false;
                    break;
                case 7:
                    suprise = true;
                    draw_me = false;
                    break;
                case 8:
                    draw_me = false;
                    true_collision = this.myAABB.myBehavior;
                    collisionBehavior = Collision.None;
                    appear_disappear = true;
                    break;
                case 9:
                    collisionBehavior = (Collision)my_collision;
                    break;

                default: break;
            }
        }
    }
}
