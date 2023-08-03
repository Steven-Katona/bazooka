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

        public Rectangle myBounds;
        Texture2D drawnBox;
        public Vector2 myCenter {get; set;}
        public int myBehavior { get; }
        
        public Hitbox(int x, int y, int width, int height, Point offset, ushort behavior) 
        { 
            myBounds = new(x + offset.X, y + offset.Y, width, height);
            myCenter = new(myBounds.Center.X, myBounds.Center.Y);
            myBehavior = behavior;
        }

        public Hitbox((int,int) newDimensions, (int, int) location, Point offset, int behavior)
        {
            myBounds = new(location.Item1 + offset.X, location.Item2 + offset.Y, newDimensions.Item1, newDimensions.Item2);
            myCenter = new(myBounds.Center.X, myBounds.Center.Y);
            myBehavior = behavior;
        }

        public ref Rectangle getBounds()
        {
            return ref myBounds;
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



            _spriteBatch.Draw(drawnBox, myBounds, Color.White);
            //_spriteBatch.Draw(drawnBox, new Vector2 (myBounds.X,myBounds.Y), myBounds, Color.White, 0.0f, myCenter, 1.0f, SpriteEffects.None, 0.9f);
        }

        
    }
}
