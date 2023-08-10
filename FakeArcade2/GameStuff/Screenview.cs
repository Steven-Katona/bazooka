using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeArcade2.GameStuff
{
    internal class Screenview
    {
        private Vector2 startingPosition;
        private int offset_x_axis;
        private int offset_y_axis;
        private int zoneWidth;
        private int zoneHeight;
        Optic focus;
        Vector2 transition;
        Texture2D pointer;

        public Screenview(Vector2 startingPosition,Optic focus, int maxWidth, int maxHeight, int worldWidth, int worldHeight, Texture2D pointer) 
        {
            //focus.getPosition().X  
            offset_x_axis = maxWidth / 2;
            offset_y_axis = maxHeight / 2;
            zoneWidth = worldWidth;
            zoneHeight = worldHeight;
            this.startingPosition = startingPosition;
            this.focus = focus;
            transition = new(0, 0);
            this.pointer = pointer;

        }

        public Matrix getOffsetTransformation() //act as an update to be called in the level update method
        {
            if (!focus.is_dead)
            {
                if (transition != focus.getPosition())
                {

                    if (focus.getPosition().X > offset_x_axis && focus.getPosition().X < (zoneWidth - offset_x_axis))
                    {
                        transition.X = focus.getPosition().X;
                    }
                    else
                    {
                        if (focus.getPosition().X > offset_x_axis)
                        {
                            transition.X = zoneWidth - offset_x_axis;
                        }
                        else
                        {
                            transition.X = offset_x_axis;
                        }
                    }

                    if (focus.getPosition().Y > offset_y_axis - 32 && focus.getPosition().Y < (zoneHeight - offset_y_axis))
                    {
                        transition.Y = focus.getPosition().Y + 32;
                    }
                    else
                    {
                        if (focus.getPosition().Y > offset_y_axis)
                        {
                            transition.Y = zoneHeight - offset_y_axis + 32;
                        }
                        else
                        {
                            transition.Y = offset_y_axis;
                        }
                    }

                    //transition = focus.getPosition();
                }

                if(focus.getPosition().Y > zoneHeight)
                {
                    focus.is_dead= true;
                }
            }
            
            
            Matrix transform = Matrix.CreateTranslation(-(transition.X - offset_x_axis), -(transition.Y - offset_y_axis), 0);
            return transform;
        }

        public void newFocus(Optic newSubject)
        {
            focus = newSubject;
        }

        public void Draw(GameTime _gameTime, SpriteBatch _spriteBatch)
        {
            if(focus.getPosition().Y < 0)
            {
                _spriteBatch.Draw(pointer, new Vector2(focus.getPosition().X, 0), Color.White);
            }
        }
    }
}
