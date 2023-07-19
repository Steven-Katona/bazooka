using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FakeArcade2.GameStuff
{
    internal class Player : Entity
    {
        
        public Player(Texture2D face, Hitbox aabb, int x, int y , bool immobile = false ): base(face, aabb, immobile, new Vector2(x,y))
        {
            
        }
    }
}
