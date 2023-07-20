using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeArcade2.GameStuff
{
    internal class Sprite : Optic
    {
        public int horizontal;
        public int vertical;
        public int negativeSlope;
        public int positiveSlope;
        (int, int) movement;
        public Sprite(Texture2D visual, Hitbox aabb, bool immobile, Vector2 myLocation) : base(visual, immobile, myLocation) 
        {
        
        }

        public void preUpdate()
        {
            int x_move = horizontal;
            int y_move = vertical;
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

        
    }
}
