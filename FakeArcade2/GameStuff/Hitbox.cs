using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using
using System.Security.Cryptography.X509Certificates;

namespace FakeArcade2.GameStuff
{
    
    internal class Hitbox
    {
        Rectangle myBounds;
        Point myCenter;
        private static readonly string hitboxColor = "#110a09";
        private static readonly string animationColor = "#d95763";
        public Hitbox(int x, int y, int width, int height) 
        { 
            myBounds = new(x, y, width, height);
            myCenter = myBounds.Center;
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
            myCenter = myBounds.Center;
        }
    }
}
