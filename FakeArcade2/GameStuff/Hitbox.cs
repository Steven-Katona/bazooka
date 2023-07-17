using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;


namespace FakeArcade2.GameStuff
{
    
    internal class Hitbox
    {
        Rectangle myBounds;
        Texture2D drawnBox;
        Vector2 myCenter;
        private static readonly string hitboxColor = "#110a09";
        private static readonly string animationColor = "#d95763";
        public Hitbox(int x, int y, int width, int height) 
        { 
            myBounds = new(x, y, width, height);
            myCenter = new(myBounds.Center.X, myBounds.Center.Y);
        }

        public Hitbox(Texture2D mask, int x, int y)
        {
            Point firstpoint = new(0,0);
            Point lastpoint = new(0,0);

            Color[] color = new Color[mask.Height * mask.Width];
            for (int loop_y = 0; loop_y < mask.Height; loop_y++)
            {
                for (int loop_x = 0; loop_x < mask.Width; loop_x++)
                { 
                    Color currentPixel = color[loop_x + (loop_y * mask.Height)];
                    string hexTest = currentPixel.ToString();
                    if(hexTest.Equals(hitboxColor))
                    {
                        if (firstpoint.Equals(new Point(0,0))) { firstpoint = new Point(loop_x, loop_y); }
                        else
                        {lastpoint = new Point(loop_x, loop_y);
        }
                    }
                }
            }

            myBounds = new(x, y, firstpoint.X - lastpoint.X, firstpoint.Y - lastpoint.Y);
            //myCenter = myBounds.Center;
        }

        public void Draw(GameTime gameTime, SpriteBatch _spriteBatch, GraphicsDevice _graphics)
        {

            if (drawnBox == null)
            {
                Color[] box = new Color[myBounds.Width * myBounds.Height];
                for (int loop_y = 0; loop_y < myBounds.Height; loop_y++)
                {
                    for (int loop_x = 0; loop_x < myBounds.Width; loop_x++)
                    {
                        if (loop_y == 0 || loop_x == 0)
                        {
                            box[loop_x + (loop_y * myBounds.Height)] = Color.Yellow;
                        }

                        if (loop_y == myBounds.Height || loop_x == myBounds.Width)
                        {
                            box[loop_x + (loop_y * myBounds.Height)] = Color.Yellow;
                        }
                    }
                }
                drawnBox = new(_graphics, myBounds.Width, myBounds.Height);
            }



            _spriteBatch.Draw(drawnBox, myCenter, Color.White);
        }
    }
}
