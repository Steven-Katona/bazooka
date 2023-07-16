using Microsoft.Xna.Framework.Graphics;
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
        public Entity(Texture2D visual, Hitbox aabb, bool immobile) : base(visual, aabb, immobile)
        {

        }
    }
}
