using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;


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
        Texture2D bazooka;
        public bool at_Exit;
        float bazooka_rotation;
        


        public Player(Animation face, Texture2D bazooka, Hitbox aabb, int x, int y , bool immobile = false ): base(face, aabb, immobile, new Vector2(x,y))
        {
            horizontal = 0;
            vertical = 0;
            launchingPosition = new Point(this.myAABB.myBounds.Right, (int)this.getPosition().Y);
            shoot_grenade = false;
            can_Shoot = true;
            at_Exit = false;
            bazooka_rotation = 0f;
            this.bazooka = bazooka;
        }



        public void Update(GameTime gameTime, KeyboardState state)
        {
            base.Update(gameTime);

            if (!is_dead)
            {
                if (state.IsKeyDown(Keys.NumPad6) || state.IsKeyDown(Keys.Right) && !(state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.Up)))
                {
                    launchingPosition = new (this.myAABB.myBounds.Right,this.myAABB.myBounds.Center.Y);
                    projectile_path = (1,0);
                    horizontal += 6;
                    bazooka_rotation = 0f;
                }

                if ((state.IsKeyDown(Keys.NumPad4) || state.IsKeyDown(Keys.Left)) && !(state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.Up)))
                {
                    launchingPosition = new(this.myAABB.myBounds.Left, this.myAABB.myBounds.Center.Y);
                    horizontal -= 6;
                    projectile_path = (-1, 0);
                    bazooka_rotation = 0f;
                }

                if (state.IsKeyDown(Keys.NumPad2) || state.IsKeyDown(Keys.Down))
                {
                    launchingPosition = new(this.myAABB.myBounds.Center.X, (int)this.myAABB.myBounds.Bottom);
                    projectile_path = (0, 1);
                    bazooka_rotation = 4.71f;
                }

                if (state.IsKeyDown(Keys.NumPad8) || state.IsKeyDown(Keys.Up))
                {
                    launchingPosition = new(this.myAABB.myBounds.Center.X, (int)this.myAABB.myBounds.Top);
                    projectile_path = (0, -1);
                    bazooka_rotation = 4.71f;
                }

                if (state.IsKeyDown(Keys.NumPad1) || state.IsKeyDown(Keys.Down) && state.IsKeyDown(Keys.Left))
                {
                    launchingPosition = new(this.myAABB.myBounds.Left, (int)this.myAABB.myBounds.Bottom);
                    projectile_path = (-1, 1);
                    bazooka_rotation = 5.50f;
                }

                if (state.IsKeyDown(Keys.NumPad3) || state.IsKeyDown(Keys.Down) && state.IsKeyDown(Keys.Right))
                {
                    launchingPosition = new(this.myAABB.myBounds.Right, (int)this.myAABB.myBounds.Bottom);
                    projectile_path = (1, 1);
                    bazooka_rotation = 1.0f;
                }

                if (state.IsKeyDown(Keys.NumPad7) || state.IsKeyDown(Keys.Up) && state.IsKeyDown(Keys.Left))
                {
                    launchingPosition = new(this.myAABB.myBounds.Left, (int)this.myAABB.myBounds.Top);
                    projectile_path = (-1, -1);
                    bazooka_rotation = 1.0f;
                }

                if (state.IsKeyDown(Keys.NumPad9) || state.IsKeyDown(Keys.Up) && state.IsKeyDown(Keys.Right))
                {
                    launchingPosition = new(this.myAABB.myBounds.Right, (int)this.myAABB.myBounds.Top);
                    projectile_path = (1, -1);
                    bazooka_rotation = 5.50f;

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

        public new void Draw(GameTime _gameTime, SpriteBatch _spriteBatch )
        {

            if (!is_dead)
            {
                _spriteBatch.Draw(bazooka, new((int)myAABB.myBounds.Center.X,(int)myAABB.myBounds.Center.Y, 32,32), null, Color.White, bazooka_rotation,new Vector2(16,16),SpriteEffects.None,1);
            }
            ((Optic)this).Draw(_gameTime, _spriteBatch);
        }
    }
}
