using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FakeArcade2.GameStuff
{
    internal class ExitDoor : Sprite
    {
        public int my_exit { get; set; }
        public ExitDoor(Animation myVisual, Hitbox aabb, bool immobile, Vector2 myLocation, int my_exit) : base(myVisual, aabb, immobile, myLocation)
        {
            this.my_exit = my_exit;
        }
    }
}
