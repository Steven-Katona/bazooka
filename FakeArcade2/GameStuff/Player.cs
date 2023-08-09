using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace FakeArcade2.GameStuff
{
    internal class Player : Entity
    {
        int finalSpeed { get; set; }
        private Point launchingPosition { get; set; }
        public Point launchedPoint;
        public (int, int) projectile_path { get; set; }
        bool can_Shoot;
        public bool shoot_grenade {get; set;}
        public int hasKey = 0;
        public int exit_found { get; set; }

        public bool at_Exit;
        


        public Player(Animation face, Hitbox aabb, int x, int y , bool immobile = false ): base(face, aabb, immobile, new Vector2(x,y))
        {
            horizontal = 0;
            vertical = 0;
            launchingPosition = new Point(this.myAABB.myBounds.Right, (int)this.getPosition().Y);
            shoot_grenade = false;
            can_Shoot = true;
            at_Exit = false;
        }



        public void Update(GameTime gameTime, KeyboardState state)
        {
            base.Update(gameTime);

            if (!is_dead)
            {
                if (state.IsKeyDown(Keys.NumPad6))
                {
                    launchingPosition = new (this.myAABB.myBounds.Right,this.myAABB.myBounds.Center.Y);
                    projectile_path = (1,0);
                    horizontal += 6;
                }

                if (state.IsKeyDown(Keys.NumPad4))
                {
                    launchingPosition = new(this.myAABB.myBounds.Left, this.myAABB.myBounds.Center.Y);
                    horizontal -= 6;
                    projectile_path = (-1, 0);
                }

                if (state.IsKeyDown(Keys.NumPad2))
                {
                    launchingPosition = new(this.myAABB.myBounds.Center.X, (int)this.myAABB.myBounds.Bottom);
                    projectile_path = (0, 1);
                }

                if (state.IsKeyDown(Keys.NumPad8))
                {
                    launchingPosition = new(this.myAABB.myBounds.Center.X, (int)this.myAABB.myBounds.Top);
                    projectile_path = (0, -1);
                }

                if (state.IsKeyDown(Keys.NumPad1))
                {
                    launchingPosition = new(this.myAABB.myBounds.Left, (int)this.myAABB.myBounds.Bottom);
                    projectile_path = (-1, 1);
                }

                if (state.IsKeyDown(Keys.NumPad3))
                {
                    launchingPosition = new(this.myAABB.myBounds.Right, (int)this.myAABB.myBounds.Bottom);
                    projectile_path = (1, 1);
                }

                if (state.IsKeyDown(Keys.NumPad7))
                {
                    launchingPosition = new(this.myAABB.myBounds.Left, (int)this.myAABB.myBounds.Top);
                    projectile_path = (-1, -1);
                }

                if (state.IsKeyDown(Keys.NumPad9))
                {
                    launchingPosition = new(this.myAABB.myBounds.Right, (int)this.myAABB.myBounds.Top);
                    projectile_path = (1, -1);
                }


                if (!in_air && state.IsKeyDown(Keys.Space))
                {
                    vertical -= 200;
                }

                if(state.IsKeyDown(Keys.Q) && can_Shoot)
                {
                    can_Shoot = false;
                    launchedPoint = launchingPosition;
                    shoot_grenade = true;
                }

                if(state.IsKeyUp(Keys.Q))
                {
                    can_Shoot = true;
                }
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

            if (this.is_dead == true && state.IsKeyDown(Keys.R))
            {
                this.Revive();
            }
        }
    }
}
