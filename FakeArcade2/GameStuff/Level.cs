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
        Dictionary<int, int> item_lookup;
        Screenview view;
        Color[] lookupValues;
        public Player the_Player { get; set; }
        public int level_addition { get; set; }
        List<Optic> level_objects;
        List<Optic> remove_me;
        GraphicsDevice graphicDevice;
        ContentManager content;
        int maxWidth;
        int maxHeight;
        int maxLevelWidth;
        int maxLevelHeight;
        int lava_offset = 0;
        bool endLevel = false;
        enum Combinations
        {

            Key = 7
        };

 
        Point Player_Offset;
        public (int, int) levelDimensions { get; set; }
        public Level(IServiceProvider service, ContentManager Content, GraphicsDevice _graphicsDevice, Texture2D colorMap, int maxWidth, int maxHeight)
        {
            item_lookup = new Dictionary<int, int>()
            {
                { 255,1}, //block
                { 1,2 }, //player
                { 254,3 }, //lava
                { 20, 4}, //end point
                { 150, 6}, //key
                { 200, 7} // checkPoint
            };
            graphicDevice = _graphicsDevice;
            content = Content;
            level_objects = new List<Optic>();
            remove_me = new List<Optic>();

            level_addition = 0;
            Vector2 positioning = new Vector2(0, 0);
            Color[] colorArray = new Color[colorMap.Height * colorMap.Width]; //height - 1?
            colorMap.GetData(colorArray);

            this.maxWidth = maxWidth;
            this.maxHeight = maxHeight;
            maxLevelWidth = colorMap.Width * 32;
            maxLevelHeight = (colorMap.Height * 32 - 32); //32 is now 31?
            lookupValues = new Color[maxWidth];

            levelDimensions = (colorMap.Width * 32, (colorMap.Height * 32));
            for(int look = 0; look < colorMap.Width; look++)
            {
                lookupValues[look] = colorArray[look];
            }

            for (int loop_y = 1; loop_y < colorMap.Height; loop_y++) //loop_y starts at one?
            {
                for (int loop_x = 0; loop_x < colorMap.Width; loop_x++)
                {
                    int the_out = 0;
                    item_lookup.TryGetValue(colorArray[loop_x + (loop_y * colorMap.Width)].R + colorArray[loop_x + (loop_y * colorMap.Width)].G, out the_out);
                    if (the_out != 0)
                    {
                        placeObject(the_out, positioning, colorArray[loop_x  + (loop_y * colorMap.Width)].B);
                    }
                    
                    positioning.X += 32;
                }
                positioning.Y += 32;
                positioning.X = 0;
            }
        }

        public bool level_Status()
        {
            return endLevel;
        }

        public void placeObject(int code, Vector2 position, int lookup_color_data) //G is Behavior, B is true_collision, and A is Key
        {
            switch (code)
            {
                case 1:
                    Optic block = new Optic(new Animation(AnimationConstruction.createAnimationTexture("block_fakeArcade2", graphicDevice, content), .20f, true),new ((int)position.X,(int)position.Y, 32,32, new Point(0,0), 2), true, position);
                    if(lookup_color_data != 0)
                    {
                        Color special_level_data = lookupValues[lookup_color_data];
                        block.Optic_behavior_alteration(special_level_data.R, special_level_data.G, special_level_data.B);
                    }
                    level_objects.Add(block);
                    break;
                case 2:
                    (int, int, Point) man_tupple = (AnimationConstruction.createHitbox("man_man_hitbox", content));
                    the_Player = new Player(new Animation(AnimationConstruction.createAnimationTexture("man_sprite_sheet", graphicDevice, content), .20f, true), new Hitbox((man_tupple.Item1,man_tupple.Item2), ((int)position.X, (int)position.Y), man_tupple.Item3, 6), (int)position.X, (int)position.Y, false);
                    the_Player.PlayerStart = position;
                    Player_Offset = man_tupple.Item3;
                    initilizeView();
                    break;
                case 3:
                    (int, int, Point) lava_tupple = (AnimationConstruction.createHitbox("lava_hitbox", content));
                    Optic lava = new Optic(new Animation(AnimationConstruction.createAnimationTexture("lava_sprite_sheet", graphicDevice, content), .20f, true, (lava_offset % 7)), new Hitbox(lava_tupple,position, 1), true, position);
                    if(lookup_color_data != 0)
                    {
                        Color special_level_data = lookupValues[lookup_color_data];
                        lava.Optic_behavior_alteration(special_level_data.R, special_level_data.G, special_level_data.B);
                    }
                    level_objects.Add(lava);
                    lava_offset += 1;
                    break;
                case 4:
                    Sprite end_point = new Sprite(new Animation(AnimationConstruction.createAnimationTexture("door_sprite_sheet", graphicDevice, content), .20f, false), new((int)position.X, (int)position.Y, 32, 32, new Point(0, 0), 5), true, position);
                    if (lookup_color_data != 0)
                    {
                        Color special_level_data = lookupValues[lookup_color_data];
                        end_point.my_exit_code = lookupValues[lookup_color_data - 1].B;
                        end_point.Optic_behavior_alteration(special_level_data.R, special_level_data.G, special_level_data.B);
                    }
                    level_objects.Add(end_point);
                    break;
                case 5:
                    (int, int, Point) grenade_tupple = AnimationConstruction.createHitbox("grenade_hitbox", content);
                    Grenade newGrenade = new Grenade(new Animation(AnimationConstruction.createAnimationTexture("grenade", graphicDevice, content),.20f, true), new Hitbox(grenade_tupple,position, 6), new Animation(AnimationConstruction.createAnimationTexture("explosion_sprite_sheet", graphicDevice, content), .05f, false),  position, the_Player.projectile_path);
                    level_objects.Insert(0,newGrenade);
                    break;
                case 6:
                    Key newKey = new Key(new Animation(AnimationConstruction.createAnimationTexture("key_sprite_sheet",graphicDevice,content), .30f, true),new((int)position.X, (int)position.Y, 32, 32, new Point(0, 0), 7),true,position,lookup_color_data-1);
                    if (lookup_color_data != 0)
                    {
                        Color special_level_data = lookupValues[lookup_color_data];
                        newKey.Optic_behavior_alteration(special_level_data.R, special_level_data.G, special_level_data.B);
                    }
                    level_objects.Add(newKey);
                    break;
                case 7:
                    Sprite check = new (new Animation(AnimationConstruction.createAnimationTexture("flag_sprite_sheet", graphicDevice, content), .30f, true), new((int)position.X, (int)position.Y, 32, 32, new Point(0, 0), 8), true, position);
                    if (lookup_color_data != 0)
                    {
                        Color special_level_data = lookupValues[lookup_color_data];
                        check.Optic_behavior_alteration(special_level_data.R, special_level_data.G, special_level_data.B);
                    }
                    level_objects.Add(check);
                    break;

                default: break;
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

            

            foreach(Optic op in remove_me)
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
                item.myAABB.Draw(gameTime, _spriteBatch, _graphicsDevice);
            }
            //the_Player.myAABB.Draw(gameTime, _spriteBatch, _graphicsDevice);
            the_Player.Draw(gameTime, _spriteBatch);
        }

        public void Dispose()
        {
            level_objects.Clear();
            item_lookup.Clear();
        }
    }
}

