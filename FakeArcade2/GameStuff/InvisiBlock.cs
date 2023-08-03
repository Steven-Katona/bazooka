using System;
using Microsoft.Xna.Framework;

namespace FakeArcade2.GameStuff
{
    internal class InvisiBlock : Sprite
    {
        public int key_value {get; set;}
        public InvisiBlock(Animation myVisual, Hitbox aabb, bool immobile, Vector2 myLocation, int key_value) : base(myVisual, aabb, immobile, myLocation)
        {
            this.key_value = key_value;
            draw_me = false;
        }
    }
}
