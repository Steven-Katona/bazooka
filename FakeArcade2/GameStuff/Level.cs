using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace FakeArcade2.GameStuff
{
    internal class Level : IDisposable
    {
        readonly Dictionary<string, int> buildingDecoder;
        Screenview view;
        Player the_Player;
        List<Optic> level_objects;
        List<Optic> remove_me;
        GraphicsDevice graphicDevice;
        ContentManager content;
        int maxWidth;
        int maxHeight;
        int maxLevelWidth;
        int maxLevelHeight;
        int lava_offset = 0;
        public (int, int) levelDimensions { get; set; }
        public Level(IServiceProvider service, ContentManager Content, GraphicsDevice _graphicsDevice, Texture2D colorMap, int maxWidth, int maxHeight)
        {
            graphicDevice = _graphicsDevice;
            content = Content;
            level_objects= new List<Optic>();
            remove_me = new List<Optic>();
            buildingDecoder = new Dictionary<string, int>()
            {   { "{R:0 G:0 B:1 A:255}", 1 }, //block
                { "{R:1 G:35 B:160 A:255}", 2 },
                { "{R:130 G:0 B:15 A:255}", 3 },
                { "{R:170 G:170 B:170 A:255}", 4 },
                { "{R:186 G:187 B:0 A:255}", 6},
                { "{R:38 G:187 B:0 A:255}", 7},
                { "{R:39 G:187 B:0 A:255}", 8}
                };
            Vector2 positioning = new Vector2(0, 0);
            Color[] colorArray = new Color[colorMap.Height * colorMap.Width];
            colorMap.GetData(colorArray);

            this.maxWidth = maxWidth;
            this.maxHeight = maxHeight;
            maxLevelWidth = colorMap.Width * 32;
            maxLevelHeight = colorMap.Height * 32;

            levelDimensions = (colorMap.Width * 32, colorMap.Height * 32);

            for (int loop_y = 0; loop_y < colorMap.Height; loop_y++)
            {
                for (int loop_x = 0; loop_x < colorMap.Width; loop_x++)
                {
                    int the_out;
                    string value;

                    if(colorArray[loop_x + (loop_y * colorMap.Width)].G == 187)
                    {
                        int variable_value = colorArray[loop_x + (loop_y * colorMap.Width)].B;
                        colorArray[loop_x + (loop_y * colorMap.Width)].B = 0;
                        value = colorArray[loop_x + (loop_y * colorMap.Width)].ToString();
                        buildingDecoder.TryGetValue(value, out the_out);
                        if (the_out != 0)
                        {
                            placeObject(the_out, positioning, variable_value);
                        }
                    }
                    else
                    {
                        value = colorArray[loop_x + (loop_y * colorMap.Width)].ToString();
                        buildingDecoder.TryGetValue(value, out the_out);
                        if (the_out != 0)
                        {
                            placeObject(the_out, positioning, 0);
                        }
                    }
                    
                    
                    positioning.X += 32;
                }
                positioning.Y += 32;
                positioning.X = 0;
            }
        }

        public void placeObject(int code, Vector2 position, int key)
        {
            switch (code)
            {
            case 1:
                Optic block = new Optic(new Animation(AnimationConstruction.createAnimationTexture("block_fakeArcade2", graphicDevice, content), .20f, false),new ((int)position.X,(int)position.Y, 32, 32, new Point(0,0), 2), true, position);
                level_objects.Add(block);
                break;
            case 2:
                (int, int, Point) man_tupple = (AnimationConstruction.createHitbox("man_man_hitbox", content));
                the_Player = new Player(new Animation(AnimationConstruction.createAnimationTexture("man_sprite_sheet", graphicDevice, content), .20f, true), new Hitbox((man_tupple.Item1,man_tupple.Item2), ((int)position.X, (int)position.Y), man_tupple.Item3, 6), (int)position.X, (int)position.Y, false);
                initilizeView();
                break;
            case 3:
                (int, int, Point) lava_tupple = (AnimationConstruction.createHitbox("lava_hitbox", content));
                Optic lava = new Optic(new Animation(AnimationConstruction.createAnimationTexture("lava_sprite_sheet", graphicDevice, content), .20f, true, (lava_offset % 7)), new Hitbox((lava_tupple.Item1, lava_tupple.Item2), ((int)position.X, (int)position.Y), lava_tupple.Item3, 1), true, position);
                level_objects.Add(lava);
                lava_offset += 1;
                break;
            case 4:
                Sprite end_point = new Sprite(new Animation(AnimationConstruction.createAnimationTexture("door_sprite_sheet", graphicDevice, content), .20f, false), new((int)position.X, (int)position.Y, 32, 32, new Point(0, 0), 5), true, position);
                level_objects.Add(end_point);
                break;
            case 5:
                (int, int, Point) grenade_tupple = AnimationConstruction.createHitbox("grenade_hitbox", content);
                Grenade newGrenade = new Grenade(new Animation(AnimationConstruction.createAnimationTexture("grenade", graphicDevice, content),.20f, true), new Hitbox((grenade_tupple.Item1, grenade_tupple.Item2),((int)position.X,(int)position.Y), grenade_tupple.Item3, 6), new Animation(AnimationConstruction.createAnimationTexture("explosion_sprite_sheet", graphicDevice, content), .05f, false),  position, the_Player.projectile_path);
                level_objects.Insert(0,newGrenade);
                break;
            case 6:
                Key newKey = new Key(new Animation(AnimationConstruction.createAnimationTexture("key_sprite_sheet",graphicDevice,content), .30f, true),new((int)position.X, (int)position.Y, 32, 32, new Point(0, 0), 7),true,position,key);
                level_objects.Add(newKey);
                break;
            case 7:
                KeyBlock keyBlock = new KeyBlock(new Animation(AnimationConstruction.createAnimationTexture("block_locked", graphicDevice, content), .30f, true), new((int)position.X, (int)position.Y, 32, 32, new Point(0, 0), 2), true, position, key);
                level_objects.Add(keyBlock);
                break;
            case 8:
                InvisiBlock invisi = new InvisiBlock(new Animation(AnimationConstruction.createAnimationTexture("block_locked", graphicDevice, content), .30f, true), new((int)position.X, (int)position.Y, 32, 32, new Point(0, 0), 0), true, position, key);
                level_objects.Add(invisi);
                break;
            default: 
                break;
            }


        }

        public Matrix getViewOffset()
        {
            return view.getOffsetTransformation();
        }

        protected void initilizeView()
        {
            view = new(new Vector2(0, 0), the_Player, maxWidth, maxHeight, maxLevelWidth, maxLevelHeight);
        }

        public void Update(GameTime gameTime, KeyboardState _keyState)
        {
            foreach(Optic item in level_objects)
            {
                if (item as Sprite != null)
                {
                    Sprite sprite = (Sprite)item;
                    sprite.Update(gameTime);

                    if(sprite as KeyBlock != null)
                    {
                        KeyBlock block = (KeyBlock)sprite;
                        if(the_Player.hasKey.Item1)
                        {
                            if(the_Player.hasKey.Item2 == block.key_value)
                            {
                                block.remove = true;
                            }
                        }
                    }

                    if (sprite as InvisiBlock != null)
                    {
                        if (!item.draw_me)
                        {
                            InvisiBlock block = (InvisiBlock)sprite;
                            if (the_Player.hasKey.Item1)
                            {
                                if (the_Player.hasKey.Item2 == block.key_value)
                                {
                                    block.draw_me = true;
                                    block.collisionBehavior = Optic.Collision.Solid;

                                }
                            }
                        }
                    }

                    if (item as Entity != null)
                    {
                        if (item as Grenade != null)
                        {
                            Grenade grenade = (Grenade)item;
                            grenade.Update(gameTime);
                            grenade.Intersects(level_objects, grenade.movement);
                        }
                    }
                }

                if(item.remove)
                {
                    remove_me.Add(item);
                }
            }

            if (the_Player.shoot_grenade)
            {
                the_Player.shoot_grenade = false;
                placeObject(5, the_Player.getVector(), 0);
            }

            the_Player.Intersects(level_objects, the_Player.movement);
            the_Player.Update(gameTime, _keyState);

            

            foreach(Sprite op in remove_me)
            {
                level_objects.Remove(op);
            }
            remove_me.Clear();

        }

        public void Draw(GameTime gameTime, SpriteBatch _spriteBatch, GraphicsDevice _graphicsDevice)
        {
            foreach(Optic item in level_objects) 
            {
                if (item.draw_me)
                {
                    item.Draw(gameTime, _spriteBatch);
                }
                //item.myAABB.Draw(gameTime, _spriteBatch, _graphicsDevice);
            }
            //the_Player.myAABB.Draw(gameTime, _spriteBatch, _graphicsDevice);
            the_Player.Draw(gameTime, _spriteBatch);
        }

        public void Dispose()
        {
            level_objects.Clear();
            buildingDecoder.Clear();
        }
    }
}

