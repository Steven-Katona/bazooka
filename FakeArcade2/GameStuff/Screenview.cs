using Microsoft.Xna.Framework;
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

        public Screenview(Vector2 startingPosition,Optic focus, int maxWidth, int maxHeight, int worldWidth, int worldHeight ) 
        {
            //focus.getPosition().X  
            offset_x_axis = maxWidth / 2;
            offset_y_axis = maxHeight / 2;
            zoneWidth = worldWidth;
            zoneHeight = worldHeight;
            this.startingPosition = startingPosition;
            this.focus = focus;
            transition = new(0, 0);

        }

        public Matrix getOffsetTransformation() //act as an update to be called in the level update method
        {
            if (!focus.is_dead)
            {
                if (transition != focus.getPosition())
                {
                    transition = focus.getPosition();
                }
            }
            
            
            Matrix transform = Matrix.CreateTranslation(-(transition.X - offset_x_axis), -(transition.Y - offset_y_axis), 0);
            return transform;
        }

        public void newFocus(Optic newSubject)
        {
            focus = newSubject;
        }
    }
}
