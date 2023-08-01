﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace FakeArcade2.GameStuff
{
    internal class Grenade : Entity
    {
        int speed = 500;
        bool is_exploding;
        Animation explosion;
        public Grenade(Animation visual, Hitbox aabb, Animation explosion, Vector2 myLocation, (int,int) direction_path, bool immobile = false) : base(visual, aabb, immobile, myLocation)
        {
            is_exploding = false;
            this.explosion = explosion;
            in_air = true;
            horizontal = direction_path.Item1 * speed;
            vertical = direction_path.Item2 * speed;
        }

        public new void Update(GameTime gameTime)
        {
            if(animator.is_animation_over())
            {
                remove = true;
            }

            preUpdate(gameTime);
            moveMe(movement.Item1, movement.Item2);

            if(in_air)
            {
                vertical += 10;
            }
        }

        public new void Intersects(List<Optic> level_objects, (int, int) movement)
        {
            foreach (Sprite obj in level_objects)
            {
                if (this.myAABB.myBounds.Intersects(obj.myAABB.myBounds) && !is_exploding)
                {
                    animator.animationPlay(explosion);
                    this.setPostion((int)this.getPosition().X - 29, (int)this.getPosition().Y - 29);
                    myAABB = new((int)this.getPosition().X, (int)this.getPosition().Y, 64, 64, new Point(0, 0), 3);
                    is_exploding = true;
                    in_air = false;
                    horizontal = 0;
                    vertical = 0;
                }
            }
        }
    }
}
