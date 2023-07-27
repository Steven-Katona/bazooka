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
        GraphicsDevice graphicDevice;
        ContentManager content;
        public (int, int) levelDimensions { get; set; }
        public Level(IServiceProvider service, ContentManager Content, GraphicsDevice _graphicsDevice, Texture2D colorMap, int maxWidth, int maxHeight)
        {
            graphicDevice = _graphicsDevice;
            content = Content;
            level_objects= new List<Optic>();
            buildingDecoder = new Dictionary<string, int>()
            {   { "{R:0 G:0 B:1 A:255}", 1 }, //block
                { "{R:1 G:35 B:160 A:255}", 2 },
                { "{R:130 G:0 B:15 A:255}", 3 },
                { "{R:170 G:170 B:170 A:255}", 4 }
                };
            Vector2 positioning = new Vector2(0, 0);
            Color[] colorArray = new Color[colorMap.Height * colorMap.Width];
            colorMap.GetData(colorArray);
            view = new(new Vector2(0, 0), the_Player, maxWidth, maxHeight, colorMap.Width * 32, colorMap.Height * 32);
            levelDimensions = (colorMap.Width * 32, colorMap.Height * 32);

            for (int loop_y = 0; loop_y < colorMap.Height; loop_y++)
            {
                for (int loop_x = 0; loop_x < colorMap.Width; loop_x++)
                {
                    string value = colorArray[loop_x + (loop_y * colorMap.Width)].ToString();
                    int the_out;
                    buildingDecoder.TryGetValue(value, out the_out);
                    if (the_out != 0)
                    {   
                        placeObject(the_out, positioning);
                    }
                    positioning.X += 32;
                }
                positioning.Y += 32;
                positioning.X = 0;
            }
        }

        public void placeObject(int code, Vector2 position)
        {
            switch (code)
            {
            case 1:
                Sprite block = new Sprite(new Animation(AnimationConstruction.createAnimationTexture("block_fakeArcade2", graphicDevice, content), .20f, false),new ((int)position.X,(int)position.Y, 32, 32, new Point(0,0), 2), true, position);
                level_objects.Add(block);
                break;
            case 2:
                    (int, int, Point) man_tupple = (AnimationConstruction.createHitbox("man_man_hitbox", content));
                    the_Player = new Player(new Animation(AnimationConstruction.createAnimationTexture("man_sprite_sheet", graphicDevice, content), .20f, true), new Hitbox((man_tupple.Item1,man_tupple.Item2), ((int)position.X, (int)position.Y), man_tupple.Item3, 0), (int)position.X, (int)position.Y, false);
                break;
            case 3:
                    (int, int, Point) lava_tupple = (AnimationConstruction.createHitbox("lava_hitbox", content));
                    Sprite lava = new Sprite(new Animation(AnimationConstruction.createAnimationTexture("lava_sprite_sheet", graphicDevice, content), .20f, true), new Hitbox((lava_tupple.Item1, lava_tupple.Item2), ((int)position.X, (int)position.Y), lava_tupple.Item3, 1), true, position);
                level_objects.Add(lava);
                break;
            case 4:
                Sprite end_point = new Sprite(new Animation(AnimationConstruction.createAnimationTexture("door_sprite_sheet", graphicDevice, content), .20f, false), new((int)position.X, (int)position.Y, 32, 32, new Point(0, 0), 5), true, position);
                level_objects.Add(end_point);
                break;
            default: break;
            }


        }

        public void Update(GameTime gameTime, KeyboardState _keyState)
        {
            foreach(Sprite item in level_objects)
            {
                item.Update(gameTime);
            }

            the_Player.Intersects(level_objects, the_Player.movement);
            the_Player.Update(gameTime, _keyState);
            
        }

        public void Draw(GameTime gameTime, SpriteBatch _spriteBatch, GraphicsDevice _graphicsDevice)
        {
            foreach(Sprite item in level_objects) 
            { 
                item.Draw(gameTime, _spriteBatch);
                //item.myAABB.Draw(gameTime, _spriteBatch, _graphicsDevice);
            }
            //the_Player.myAABB.Draw(gameTime, _spriteBatch, _graphicsDevice);
            the_Player.Draw(gameTime, _spriteBatch);
        }

        public void Dispose()
        {
            level_objects.Clear();
        }
    }
}

