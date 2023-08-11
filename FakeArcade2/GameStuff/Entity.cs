
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;


namespace FakeArcade2.GameStuff
{

    abstract internal class Entity : Sprite
    {
        public bool in_air { get; set; }
        public Vector2 myStart { get; set; }
        public struct AnimationCloset
        {
            public Animation starting_Animation;
            public Animation going_Left;
            public Animation going_Right;
            public Animation air_Born;
            public Animation dead_and_on_Fire;
        };
        AnimationCloset myCloset = new AnimationCloset();
        public Entity(Animation visual, Hitbox aabb, bool immobile, Vector2 myLocation) : base(visual, aabb, immobile, myLocation)
        {
            myStart = myLocation;
        }

        public void PopulateCloset(params Animation[] theAnimations)
        {
            myCloset.starting_Animation = this.myVisual;
            switch (theAnimations.Length)
            {
                case 1:
                    myCloset.dead_and_on_Fire = theAnimations[0];
                    break;
                case 2:
                    myCloset.dead_and_on_Fire = theAnimations[0];
                    myCloset.air_Born = theAnimations[1];
                    break;
                case 4:
                    myCloset.dead_and_on_Fire = theAnimations[0];
                    myCloset.air_Born = theAnimations[1];
                    myCloset.going_Left = theAnimations[2];
                    myCloset.going_Right = theAnimations[3];
                    break;
                default: break;
            }
        }

        public void setDead()
        {
            this.is_dead = true;
        }

        public void Revive()
        {
            this.horizontal = 0;
            this.vertical = 0;
            this.is_dead = false;
            this.setPostion((int)myStart.X, (int)myStart.Y);
            this.animator.animationPlay(myCloset.starting_Animation);
            collisionBehavior = (Collision)this.myAABB.myBehavior;
        }

        public new void Update(GameTime _gameTime)
        {
            CheckCloset();
            if(in_air)
            {
                vertical += 10;
            }
            base.Update( _gameTime);
        }

        public void CheckCloset()
        {
            if (in_air)
            {
                if (myCloset.air_Born != null && !myCloset.air_Born.Equals(animator.currentDrawnTexture))
                {
                    animator.animationPlay(myCloset.air_Born);
                }
            }
            else
            {
                if (is_dead)
                {
                    if (myCloset.dead_and_on_Fire != null && !myCloset.dead_and_on_Fire.Equals(animator.currentDrawnTexture))
                    {
                        animator.animationPlay(myCloset.dead_and_on_Fire);
                    }
                }
                else
                {
                    if (vertical < 0)
                    {
                        if (myCloset.going_Left != null && !myCloset.going_Left.Equals(animator.currentDrawnTexture))
                        {
                            animator.animationPlay(myCloset.going_Left);
                        }
                    }
                    else
                    {
                        if (myCloset.going_Right != null && !myCloset.going_Right.Equals(animator.currentDrawnTexture))
                        {
                            animator.animationPlay(myCloset.going_Right);
                        }
                    }
                }
            }

            
        }

        public void Intersects(List<Optic> level_objects, (int, int) movement)
        {
            bool on_ground = false;


            foreach (Optic obj in level_objects)
            {
                if (this as Player != null)
                {
                    int currentKey = ((Player)this).hasKey;

                    if(this.myAABB.myBounds.Intersects(obj.myAABB.myBounds))
                    {
                        if (obj.suprise)
                        {
                            obj.draw_me = true;
                        }

                        if (obj.collisionBehavior == Collision.Enemy)
                        {
                            this.horizontal = -horizontal;
                        }

                        if (obj.collisionBehavior == Collision.Checkpoint)
                        {
                            
                            obj.remove = true;
                            ((Player)this).myStart = obj.getPosition();
                            
                        }

                        if (obj.collisionBehavior == Collision.Key)
                        {
                       
                            Key thatKey = (Key)obj;
                            Player play = (Player)this;
                            play.hasKey = thatKey.key_value;
                            thatKey.remove = true;
                            
                        }

                        if (obj.collisionBehavior == Collision.End)
                        {
                           
                            Player play = (Player)this;
                            play.exit_found = obj.my_exit_code;
                            play.at_Exit = true;
                            
                        }

                        if(obj.collisionBehavior == Collision.Bounce)
                        {
                            this.vertical = -1 * Math.Abs(vertical) - 10;
                        }

                    }

                    if (obj.trigger == currentKey)
                    {
                        if (obj.appear_disappear)
                        {
                            if (obj.draw_me == false)
                            {
                                obj.draw_me = true;
                                obj.collisionBehavior = (Collision)obj.true_collision;
                                obj.appear_disappear = false;
                            }
                            else
                            {
                                obj.remove = true;
                                obj.appear_disappear = false;
                            }
                        }

                        if(obj.collisionBehavior == Collision.Checkpoint && obj.trigger != 0)
                        {
                            obj.remove = true;
                            ((Player)this).myStart = obj.getPosition();
                        }

                       
                    }

                    if(obj.fog)
                    {
                        if(Math.Abs((double)(obj.getPosition().X - this.getPosition().X)) < 80 && Math.Abs((double)(obj.getPosition().Y - this.getPosition().Y)) < 80)
                        {
                            obj.draw_me = true;
                            obj.fog = false;
                        }
                    }

                    if(this.is_dead && obj as Enemy != null)
                    {
                        if (obj.is_dead)
                            ((Enemy)obj).Revive();
                    }
                }


                if (obj as Sprite != null) //I don't think this is the right approach
                {
                    Sprite newObj = (Sprite)obj;
                    if (newObj.collisionBehavior == Collision.Jump)
                    {
                        if (this.myAABB.myBounds.Intersects(newObj.myAABB.myBounds))
                        {
                            
                            if (this.collisionBehavior == Collision.Enemy)
                            {
                                this.horizontal += newObj.displacement.Item1;
                                this.vertical += newObj.displacement.Item2;
                            }
                            else
                            {
                                this.horizontal += -newObj.displacement.Item1;
                                this.vertical += -newObj.displacement.Item2;
                            }
                            /*
                            if (Math.Abs(this.myAABB.myBounds.Center.X - newObj.myAABB.myBounds.Center.X) < 10)
                            {
                                if (this.myAABB.myBounds.Center.X - newObj.myAABB.myBounds.Center.X < 0)
                                {
                                    this.vertical -= 100;
                                }
                                else
                                {
                                    this.vertical += 100;
                                }
                            }
                            else
                            {
                                if (this.myAABB.myBounds.Center.X < newObj.myAABB.myBounds.Center.X)
                                {
                                    this.horizontal -= 100;
                                }
                                else
                                {
                                    this.horizontal += 100;
                                }
                            }

                            if (Math.Abs(this.myAABB.myBounds.Center.Y - newObj.myAABB.myBounds.Center.Y) < 10)
                            {
                                if (this.myAABB.myBounds.Center.Y - newObj.myAABB.myBounds.Center.Y < 0)
                                {
                                    this.horizontal += 100;
                                }
                                else
                                {
                                    this.horizontal -= 100;
                                }
                            }
                            else
                            {
                                if (this.myAABB.myBounds.Center.Y < newObj.myAABB.myBounds.Center.Y)
                                {
                                    this.vertical -= 100;
                                }
                                else
                                {
                                    this.vertical += 100;
                                }
                            }*/
                        }
                    }
                }
                


                if (obj.collisionBehavior == Collision.Solid || obj.collisionBehavior == Collision.Stable  && this as Player != null || obj.collisionBehavior == Collision.Sturdy && this as Enemy != null)
                {
                    /*
                    bool horizontal_collision = ((touchingLeft(obj.myAABB, movement.Item1)) || (touchingRight(obj.myAABB, movement.Item1)));
                    bool vertical_collision = ((touchingTop(obj.myAABB, movement.Item2)) || (touchingBottom(obj.myAABB, movement.Item2)));

                    int left_correction = -(this.myAABB.myBounds.Left + movement.Item1 - obj.myAABB.myBounds.Right); //has to be positive to be in effect
                    int right_correction = -(this.myAABB.myBounds.Right + movement.Item1 - obj.myAABB.myBounds.Left); //has to be negative to be in effect
                    int top_correction = -(this.myAABB.myBounds.Top + movement.Item2 - obj.myAABB.myBounds.Bottom); //has to be positive to be in effect
                    int bottom_correction = -(this.myAABB.myBounds.Bottom + movement.Item2 - obj.myAABB.myBounds.Top); //has to be negative to be in effect

                    int horizontal_correction = 0;
                    int vertical_correction = 0;

                    if(horizontal_collision || vertical_collision) 
                    {
                        if (left_correction > 0)
                        {
                            horizontal_correction = left_correction;

                        }

                        if (right_correction < 0)
                        {
                            horizontal_correction = right_correction;
                        }

                        if (top_correction > 0)
                        {
                            vertical_correction = top_correction;
                        }

                        if (bottom_correction < 0)
                        {
                            vertical_correction = bottom_correction;
                        }
                    }


                    if (horizontal_collision && vertical_collision) 
                    {
                        moveMe((float)horizontal_correction, (float)vertical_correction);
                    }
                    else if(horizontal_collision)
                    {
                        moveMe((float)horizontal_correction, (float)vertical_correction);
                    }
                    else if(vertical_collision)
                    {
                        moveMe((float)horizontal_correction, (float)vertical_correction);
                    }



                    */








                    if (touchingLeft(obj.myAABB, movement.Item1))
                    {
                        if (horizontal < 0)
                            horizontal = 0;
                        while (this.myAABB.myBounds.Left >= obj.myAABB.myBounds.Right + 1)
                        {
                            moveMe(-(this.myAABB.myBounds.Left - obj.myAABB.myBounds.Right), 0);
                        }
                    }

                    if (touchingRight(obj.myAABB, movement.Item1))
                    {
                        if (horizontal > 0)
                            horizontal = 0;
                        while (this.myAABB.myBounds.Right <= obj.myAABB.myBounds.Left - 1)
                        {
                            moveMe(obj.myAABB.myBounds.Left - this.myAABB.myBounds.Right, 0);
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
