using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeArcade2.GameStuff
{
    internal class Optic
    {
        Texture2D myTexture;
        bool immobile;
        public int horizontal;
        public int vertical;
        public int negativeSlope;
        public int positiveSlope;
        Vector2 myLocation;
        (int, int) movement;
        public Optic(Texture2D visual, bool immobile, Vector2 myLocation)
        {
            myTexture = visual;
            this.immobile = immobile;
            this.myLocation = myLocation;
        }







        public void preUpdate()
        {
            int x_move = horizontal;
            int y_move = horizontal;
            double xy_move = CalculateDiagonalMovement(negativeSlope);
            double yx_move = CalculateDiagonalMovement(positiveSlope);
            x_move = (int)Math.Ceiling((x_move * 1.0d) + xy_move);
            y_move = (int)Math.Ceiling((y_move * 1.0d) - xy_move);
            x_move = (int)Math.Ceiling((x_move * 1.0d) + yx_move);
            y_move = (int)Math.Ceiling((y_move * 1.0d) + yx_move);
            movement = (x_move, y_move);
        }


        public void Update(GameTime gameTime)
        {
            int x_move = (int)Math.Ceiling(movement.Item1 * gameTime.ElapsedGameTime.TotalSeconds);
            int y_move = (int)Math.Ceiling(movement.Item2 * gameTime.ElapsedGameTime.TotalSeconds);
            moveMe(x_move, y_move);
        }

        public void moveMe(float x, float y)
        {
            myLocation.X += x;
            myLocation.Y += y;
        }

        public void Draw(GameTime _gameTime, SpriteBatch _spriteBatch)
        {
            if (myTexture != null) 
            {
                _spriteBatch.Draw(myTexture, myLocation, Color.White);
            }
        }


        static public double CalculateDiagonalMovement(int the_move)
        {
            double amount = the_move / 2;
            amount = Math.Sqrt(amount);
            return amount;
        }
    }
}
