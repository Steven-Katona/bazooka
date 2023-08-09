
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;


namespace FakeArcade2.GameStuff
{
    abstract internal class Enemy : Entity, IKineticBehavior
    {
        public Action myBehavior;
        public Action otherBehavior;
        public Enemy(Animation visual, Hitbox aabb, bool immobile, Vector2 myLocation) : base(visual, aabb, immobile, myLocation)
        {

        }
        public bool implementAction(Action result, bool bool_result)
        {
                if (bool_result)
                {
                    Behave(result);
                }

            return bool_result;
        }

        public new abstract void Update(GameTime _gameTime);
        
        public abstract void Behave(Action EnemyBehavior);

    }
}
