
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Transactions;

namespace FakeArcade2.GameStuff
{
    internal class JumpingMan : Enemy
    {
        int myJumpStrength { get; set; }
        double jumpTiming { get; set; }
        double timer { get; set; }
        Func<bool,bool> func;
        public JumpingMan(Animation visual, Hitbox aabb, bool immobile, Vector2 myLocation) : base(visual, aabb, immobile, myLocation)
        {
            myJumpStrength = 500;
            jumpTiming = 3;
            timer = jumpTiming;
            myBehavior = () => { vertical -= myJumpStrength; };
            func = (bool test) => { return test; };
        }
        public override void Update(GameTime _gameTime)
        {
            
            if (!is_dead)
            {
                double timeDelta = timer - _gameTime.ElapsedGameTime.TotalSeconds;
                if (implementAction(myBehavior, func(timeDelta < 0)))
                {
                    timer = jumpTiming;
                }
                else
                {
                    timer = timeDelta;
                }
            }
            ((Entity)this).Update(_gameTime);

        }

        public void SpecialOptions(int newJump, int newTime)
        {
            myJumpStrength = newJump * 10;
            jumpTiming = newTime;
        }
        public override void Behave(Action behavior)
        {
            behavior();
        }
    }
}