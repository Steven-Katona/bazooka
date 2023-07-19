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
        Vector2 myLocation;
        public Optic(Texture2D visual, bool immobile, Vector2 myLocation)
        {
            myTexture = visual;
            this.immobile = immobile;
            this.myLocation = myLocation;
        }


        

        public Vector2 getPosition()
        {
            return myLocation;
        }

        

        public void Draw(GameTime _gameTime, SpriteBatch _spriteBatch)
        {
            if (myTexture != null) 
            {
                _spriteBatch.Draw(myTexture, myLocation, Color.White);
            }
        }

        public void moveMe(float x, float y)
        {
            myLocation.X += x;
            myLocation.Y += y;
        }


        static public double CalculateDiagonalMovement(int the_move)
        {
            double amount = the_move / 2;
            amount = Math.Sqrt(amount);
            return amount;
        }
    }
}
