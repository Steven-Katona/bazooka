using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace FakeArcade2.GameStuff
{
    internal class Breakable : Optic 
    {
        public Breakable(Animation myVisual, Hitbox aabb, bool immobile, Vector2 myLocation) : base(myVisual, aabb, immobile, myLocation)
        {

        }
    }
}
