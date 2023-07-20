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
        enum behavior : ushort
        {
            safe = 0,
            danger = 1,
            solid = 2,
            jump = 3,
            sturdy = 4
        };
        Rectangle myBounds { get; set; }
        Texture2D drawnBox;
        Vector2 myCenter;
        bool firstPointFound = false;
        private static readonly string hitboxColor = "{R:17 G:10 B:9 A:255}";
        public Hitbox(int x, int y, int width, int height) 
        { 
            myBounds = new(x, y, width, height);
            myCenter = new(myBounds.Center.X, myBounds.Center.Y);
        }

        public bool isDrawnBox()
        {
            if (drawnBox == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void setCenter(Vector2 newCenter)
        {
            myCenter = newCenter;
        }

        public Hitbox(Texture2D mask, int x, int y)
        {
            Point firstpoint = new(0,0);
            Point lastpoint = new(0,0);

            Color[] color = new Color[mask.Height * mask.Width];
            mask.GetData(color);
            for (int loop_y = 0; loop_y < mask.Height; loop_y++)
            {
                for (int loop_x = 0; loop_x < mask.Width; loop_x++)
                { 
                    Color currentPixel = color[loop_x + (loop_y * mask.Width)];
                    string hexTest = currentPixel.ToString();
                    if(hexTest.Equals(hitboxColor))
                    {
                        if (firstPointFound) 
                        {
                            lastpoint = new Point(loop_x, loop_y);
                        }
                        else
                        {
                            firstpoint = new Point(loop_x, loop_y);
                            firstPointFound = true;

                        }
                    }
                }
            }

            myBounds = new(x, y, lastpoint.X - firstpoint.X, lastpoint.Y - firstpoint.Y);
            myCenter = new(myBounds.Center.X, myBounds.Center.Y);
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
                            box[loop_x + (loop_y * myBounds.Width)] = Color.Yellow;
                        }

                        if (loop_y == myBounds.Height - 1 || loop_x == myBounds.Width - 1)
                        {
                            box[loop_x + (loop_y * myBounds.Width)] = Color.Yellow;
                        }
                    }
                }
                drawnBox = new(_graphics, myBounds.Width, myBounds.Height);
                drawnBox.SetData(box);
            }



            _spriteBatch.Draw(drawnBox, myCenter, Color.White);
        }
    }
}
