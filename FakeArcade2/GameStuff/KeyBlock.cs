using System;
using Microsoft.Xna.Framework;

namespace FakeArcade2.GameStuff
{
    internal class KeyBlock : Sprite
    {
        public int key_value {get; set;}
        public KeyBlock(Animation myVisual, Hitbox aabb, bool immobile, Vector2 myLocation, int key_value) : base(myVisual, aabb, immobile, myLocation)
        {
            this.key_value = key_value;
        }
    }
}
