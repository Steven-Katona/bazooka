using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeArcade2.GameStuff
{
    internal class Sprite : Optic
    {
        public Sprite(Texture2D visual, Hitbox aabb, bool immobile) : base(visual, immobile) 
        {
        
        }
    }
}
