using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeArcade2.GameStuff
{

    internal class Entity : Sprite
    {
        public bool in_air { get; set; }

        public Entity(Animation visual, Hitbox aabb, bool immobile, Vector2 myLocation) : base(visual, aabb, immobile, myLocation)
        {

        }

        public void Intersects(List<Optic> level_objects, (int, int) movement)
        {
            bool on_ground = false;
            foreach (Sprite obj in level_objects)
            {
                
                if (touchingLeft(obj.myAABB, movement.Item1))
                {
                    if(horizontal < 0)
                        horizontal = 0;

                    while (this.myAABB.myBounds.Left > obj.myAABB.myBounds.Right + 1)
                    {
                        moveMe(-((myAABB.myBounds.Left - obj.myAABB.myBounds.Right) - 1), 0f);
                    }
                    //this.setPostion((int)(obj.getPosition().X + obj.myAABB.myBounds.Right), (int)getPosition().Y);
                }

                if (touchingRight(obj.myAABB, movement.Item1))
                {
                    if(horizontal > 0)
                        horizontal = 0;

                    while (this.myAABB.myBounds.Right < obj.myAABB.myBounds.Left - 1)
                    {
                        moveMe(((obj.myAABB.myBounds.Left - this.myAABB.myBounds.Right)), 0);
                    }
                    //this.setPostion((int)(obj.getPosition().X - obj.myAABB.myBounds.Left), (int)getPosition().Y);
                }

                if (touchingBottom(obj.myAABB, movement.Item2))
                {
  
                    if(vertical > 0)
                    {
                        vertical = 0;
                    }

                    on_ground = true;
                    while (this.myAABB.myBounds.Bottom < obj.myAABB.myBounds.Top - 1)
                    {
                        moveMe(0f, obj.myAABB.myBounds.Top - this.myAABB.myBounds.Bottom);
                    }
                }

                if (touchingTop(obj.myAABB, movement.Item2))
                {

                    if (vertical < 0)
                    {
                        vertical = 0;
                    }

                    while (this.myAABB.myBounds.Top > obj.myAABB.myBounds.Bottom)
                    {
                        moveMe(0f, -(this.myAABB.myBounds.Top - obj.myAABB.myBounds.Bottom));
                    }
                }



            }

            if (on_ground == true)
            {
                in_air = false;
            }
            else
            {
                in_air = true;
            }
        }
    }
}
