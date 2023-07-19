using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
        public Level(IServiceProvider service, ContentManager Content, Texture2D colorMap, int maxWidth, int maxHeight)
        {
            buildingDecoder = new Dictionary<string, int>()
            {   { "{R:0 G:0 B:1 A:255}", 1 },
                { "{R:1 G:35 B:160 A:255}", 2 },
                { "{R:130 G:0 B:15 A:255}", 3 },
                { "{R:170 G:170 B:170 A:255}", 4 }
                };
            Vector2 positioning = new Vector2(0, 0);
            Color[] colorArray = new Color[colorMap.Height * colorMap.Width];
            colorMap.GetData(colorArray);


            for (int loop_y = 0; loop_y < colorMap.Height; loop_y++)
            {
                for (int loop_x = 0; loop_x < colorMap.Width; loop_x++)
                {
                    string value = colorArray[loop_x + (loop_y * colorMap.Width)].ToString();
                    int the_out;
                    buildingDecoder.TryGetValue(value, out the_out);
                    placeObject(the_out, positioning);
                    positioning.X += 32;
                }
                positioning.Y += 32;
                view = new(new Vector2(0, 0), the_Player, maxWidth, maxHeight, colorMap.Width * 32, colorMap.Height * 32);
            }
        }

            public void placeObject(int code, Vector2 position)
            {
                switch (code)
                {
                    case 0:
                        break;
                    default: break;
                }


            }

            public void Update(GameTime gameTime)
            {

            }

            public void Draw(GameTime gameTime, SpriteBatch _spriteBatch, GraphicsDevice _graphicsDevice)
            {

            }

            public void Dispose()
            {

            }
        }
    }

