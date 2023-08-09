using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace FakeArcade2.GameStuff
{
    abstract internal class Hazard : Sprite, IKineticBehavior
    {
        
        public Hazard(Animation visual, Hitbox aabb, bool immobile, Vector2 myLocation) : base(visual, aabb, immobile, myLocation)
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

