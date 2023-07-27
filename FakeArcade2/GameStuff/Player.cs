using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.Xna.Framework.Input;

namespace FakeArcade2.GameStuff
{
    internal class Player : Entity
    {
        int finalSpeed { get; set; }
        Point launchingPosition;
        public Player(Animation face, Hitbox aabb, int x, int y , bool immobile = false ): base(face, aabb, immobile, new Vector2(x,y))
        {
            horizontal = 0;
            vertical = 0;
            launchingPosition = new Point((int)this.myAABB.myCenter.X + myAABB.myBounds.Width/2, (int)this.myAABB.myCenter.Y);
        }

        public void Update(GameTime gameTime, KeyboardState state)
        {
            if(state.IsKeyDown(Keys.NumPad6))
            {
                horizontal += 5;
            }

            if (state.IsKeyDown(Keys.NumPad4))
            {
                horizontal -= 5;
            }

            if (!state.IsKeyDown(Keys.NumPad4) && !state.IsKeyDown(Keys.NumPad6))
            {
                if (horizontal > 2)
                {
                   horizontal -= 1;
                }
                else if (horizontal > .50) 
                {
                    horizontal -= .10f;
                }

                if(horizontal < -2)
                {
                    horizontal += 1;
                }
                else if(horizontal < -.50)
                {
                    horizontal += .10f;
                }
            }

            if(!in_air && state.IsKeyDown(Keys.Space))
            {
                vertical -= 400;
            }

            if (in_air)
            {
               vertical += 10;
            }



            Update(gameTime);
        }
    }
}
