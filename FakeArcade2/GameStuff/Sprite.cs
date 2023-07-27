using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeArcade2.GameStuff
{
    internal class Sprite : Optic
    {
        enum behavior : ushort
        {
            none = 0,
            danger = 1,
            solid = 2,
            jump = 3,
            sturdy = 4,
            end = 5,
            safe = 6
        };
        public double horizontal {get; set;}
        public double vertical {get; set;}
        public (int, int) movement { get; set; }
        public Sprite(Animation myVisual, Hitbox aabb, bool immobile, Vector2 myLocation) : base(myVisual, aabb, immobile, myLocation) 
        {
            
        }

        public void preUpdate(GameTime gameTime)
        {
            double x_move = horizontal;
            double y_move = vertical;
     
            x_move = (int)Math.Floor(Math.Abs(x_move * gameTime.ElapsedGameTime.TotalSeconds));
            y_move = (int)Math.Floor(Math.Abs(y_move * gameTime.ElapsedGameTime.TotalSeconds));
            
            if(horizontal < 0)
            {
                x_move = x_move * -1;
            }

            if(vertical < 0)
            {
                y_move = y_move * -1;
            }
            
            movement = ((int)x_move, (int)y_move);
        }

        public void Update(GameTime gameTime)
        {
            

            if (!this.immobile)
            {
                preUpdate(gameTime);
                moveMe(movement.Item1, movement.Item2);
            }
        }


        protected bool touchingLeft(Hitbox aabb, int move)
        {
            return (myAABB.myBounds.Left + move <= aabb.myBounds.Right) && (myAABB.myBounds.Right > aabb.myBounds.Right) && (myAABB.myBounds.Bottom > aabb.myBounds.Top) && (myAABB.myBounds.Top < aabb.myBounds.Bottom);
        }

        protected bool touchingRight(Hitbox aabb, int move)
        {
            return (myAABB.myBounds.Right + move >= aabb.myBounds.Left) && (myAABB.myBounds.Left < aabb.myBounds.Left) && (myAABB.myBounds.Bottom > aabb.myBounds.Top) && (myAABB.myBounds.Top < aabb.myBounds.Bottom);
        }

        protected bool touchingBottom(Hitbox aabb, int move)
        {
            return (myAABB.myBounds.Bottom + move >= aabb.myBounds.Top) && (myAABB.myBounds.Left < aabb.myBounds.Right) && (myAABB.myBounds.Right > aabb.myBounds.Left) && (myAABB.myBounds.Top < aabb.myBounds.Top);
        }

        protected bool touchingTop(Hitbox aabb, int move)
        {
            return (myAABB.myBounds.Top + move <= aabb.myBounds.Bottom) && (myAABB.myBounds.Left < aabb.myBounds.Right) && (myAABB.myBounds.Right > aabb.myBounds.Left) && (myAABB.myBounds.Bottom > aabb.myBounds.Bottom);
        }

    }
}
