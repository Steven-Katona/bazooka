using Microsoft.Xna.Framework;
using System;

namespace FakeArcade2.GameStuff
{
    internal class Key : Sprite
    {
        public int key_value { get; set; }
        public Key(Animation myVisual, Hitbox aabb, bool immobile, Vector2 myLocation, int key_value) : base(myVisual, aabb, immobile, myLocation)
        {
            this.key_value = key_value;  
        }
    }
}
