using Microsoft.Xna.Framework;
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
            animator.setDrawPriority(1);
            in_air = true;
            horizontal = direction_path.Item1 * speed;
            vertical = direction_path.Item2 * speed;
            setDisplacement(((int)(horizontal/ 4), (int)(vertical/ 4)));
        }

        public new void Update(GameTime gameTime)
        {
            ((Entity)this).Update(gameTime);
            if(animator.is_animation_over())
            {
                remove = true;
            }
        }

        public new void Intersects(List<Optic> level_objects, (int, int) movement)
        {
            foreach (Optic obj in level_objects)
            {
                if ((obj.collisionBehavior != Collision.Safe && obj.collisionBehavior != Collision.None))
                {
                    if (this.myAABB.myBounds.Intersects(obj.myAABB.myBounds) && !is_exploding)
                    {
                        animator.animationPlay(explosion);
                        myAABB = new((int)this.getPosition().X, (int)this.getPosition().Y, 128, 128, new Point(-48, -32), 0);
                        drawnRectangle = new((int)this.getPosition().X, (int)this.getPosition().Y, 128, 128);
                        animator.setOffset(new Point(-48, -32));
                        collisionBehavior = Collision.Jump;
                        is_exploding = true;
                        in_air = false;
                        horizontal = 0;
                        vertical = 0;
                        if (obj.breakable == true)
                        {
                            obj.remove = true;
                        }
                    }
                }

                if(this.myAABB.myBounds.Intersects(obj.myAABB.myBounds) && is_exploding)
                {
                    if (obj.breakable == true)
                    {
                        obj.remove = true;
                    }
                }
            }
        }
    }
}
