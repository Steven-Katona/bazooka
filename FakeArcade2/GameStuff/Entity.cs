
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace FakeArcade2.GameStuff
{

    internal class Entity : Sprite
    {
        public bool in_air { get; set; }
        
        public Entity(Animation visual, Hitbox aabb, bool immobile, Vector2 myLocation) : base(visual, aabb, immobile, myLocation)
        {
 
        }

        public void setDead()
        {
            this.is_dead = true;
        }

        public void Intersects(List<Optic> level_objects, (int, int) movement)
        {
            bool on_ground = false;
            foreach (Optic obj in level_objects)
            {
                if (this as Player != null)
                {
                    if (obj as Key != null)
                    {
                        if (this.myAABB.myBounds.Intersects(obj.myAABB.myBounds))
                        {
                            Key thatKey = (Key)obj;
                            Player play = (Player)this;
                            play.hasKey = (true, thatKey.key_value);
                            thatKey.remove = true;
                        }
                    }
                }

                if (obj as Sprite != null)
                {
                    Sprite newObj = (Sprite)obj;
                    if (newObj.collisionBehavior == Collision.Jump)
                    {
                        if (this.myAABB.myBounds.Intersects(newObj.myAABB.myBounds))
                        {
                            this.horizontal += -newObj.displacement.Item1;
                            this.vertical += -newObj.displacement.Item2;
                          
                        }
                    }
                }

                if (obj.collisionBehavior == Collision.Solid || obj.collisionBehavior == Collision.Sturdy)
                {
                    

                    if (touchingLeft(obj.myAABB, movement.Item1))
                    {
                        if (horizontal < 0)
                            horizontal = 0;

                        while (this.myAABB.myBounds.Left > obj.myAABB.myBounds.Right + 1)
                        {
                            moveMe(-((myAABB.myBounds.Left - obj.myAABB.myBounds.Right) - 1), 0f);
                        }
                    }

                    if (touchingRight(obj.myAABB, movement.Item1))
                    {
                        if (horizontal > 0)
                            horizontal = 0;

                        while (this.myAABB.myBounds.Right < obj.myAABB.myBounds.Left - 1)
                        {
                            moveMe(((obj.myAABB.myBounds.Left - this.myAABB.myBounds.Right)), 0);
                        }
                    }

                    if (touchingBottom(obj.myAABB, movement.Item2))
                    {

                        if (vertical > 0)
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

                if (!this.is_dead)
                {
                    
                    if (obj.collisionBehavior == Collision.Danger)
                    {
                        if (this.myAABB.myBounds.Intersects(obj.myAABB.myBounds))
                        {
                            this.setDead();
                        }
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
