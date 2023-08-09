
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;



namespace FakeArcade2.GameStuff
{
    internal class HazardBlocks : Hazard
    {
        double timer;
        double startingTime;
        private Action myBehavior;
        private Action otherBehavior;
        private bool invisible;
        Func<bool, bool> func;
        bool active;
        bool disappearing;

        public HazardBlocks(Animation visual, Hitbox aabb, bool immobile, Vector2 myLocation) : base(visual, aabb, immobile, myLocation)
        {
            timer = 0;
            startingTime = 10;
            invisible = false;
            disappearing = true;
            myBehavior = () => { this.draw_me = false; this.collisionBehavior = Collision.None; invisible = true; };
            otherBehavior = () => { this.draw_me = true; this.collisionBehavior = Collision.Bounce; invisible = false; };
            func = (bool test) => { return test; };
        }
        public override void Update(GameTime _gameTime)
        {
            if (active)
            {
                double timeDelta = timer - _gameTime.ElapsedGameTime.TotalSeconds;
                bool choice;

                if (active && disappearing)
                {
                    if (!invisible)
                    {
                        choice = implementAction(myBehavior, func(timeDelta < 0));
                    }
                    else
                    {
                        choice = implementAction(otherBehavior, func(timeDelta < 0));
                    }


                    if (choice)
                    {
                        timer = startingTime;
                    }
                    else
                    {
                        timer = timeDelta;
                    }
                }

                ((Sprite)this).Update(_gameTime);
            }
            else
            {
                if (draw_me == true)
                {
                    active = true;
                }
            }
        }

        public void SpecialOption(double value, double active_test)
        {
            startingTime = value;
            if (startingTime == 0)
            {
                disappearing = false;
            }

            if (active_test != 0)
                active = false;
        }



        public override void Behave(Action behave)
        {
            behave();
        }
    }
}
