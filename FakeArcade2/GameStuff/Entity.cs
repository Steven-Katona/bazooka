using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeArcade2.GameStuff
{

    internal class Entity : Sprite
    {
        public bool is_jumping { get; set; }
        public Entity(Animation visual, Hitbox aabb, bool immobile, Vector2 myLocation) : base(visual, aabb, immobile, myLocation)
        {

        }
    }
}
