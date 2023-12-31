﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FakeArcade2.GameStuff
{
    static internal class AnimationConstruction
    {
        static readonly string AnimationColor = "{R:217 G:87 B:99 A:255}";
        static readonly string HitboxColor = "{R:17 G:10 B:9 A:255}";
        private static Dictionary<string, (int, int, Point)> getHitbox;
        private static Dictionary<string, Animation> getTextureArray;

        static public void Initilize()
        {
            getHitbox = new Dictionary<string, (int, int,Point)> { };
            getTextureArray = new Dictionary<string, Animation> { };
        }

        static public Animation createAnimationTexture(string fileName, GraphicsDevice _graphicDevice, ContentManager content, float frameTime, bool isLooping)
        {
            Animation result;
            Texture2D file = content.Load <Texture2D>("texture2d/" + fileName);
            if(getTextureArray.TryGetValue(fileName, out result))
            {
                return result;
            }
            else
            {
                int newWidth = 0;
                Color[] color = new Color[file.Width * file.Height];
                file.GetData(color);
                for(int loop_x = 0; loop_x < file.Width; loop_x++)
                {
                    if (color[loop_x].ToString().Equals(AnimationColor))
                    {
                        newWidth = loop_x + 1;
                        color[loop_x] = new Color(0,0,0);
                    }
                }

                

                if (newWidth == 0)
                {
                    result = new Animation(file, frameTime, isLooping, file.Width ,file.Height);
                }
                else 
                {
                    Texture2D frame = new(_graphicDevice, file.Width, file.Height);
                    frame.SetData(color);
                    result = new Animation(frame, frameTime, isLooping, newWidth, file.Height);
                }

                /*
                int frameCount = file.Width / newWidth;
                result = new Texture2D[frameCount];
                int dummy_Value = 0;

                for(int iteration = 0; iteration < frameCount;  iteration++)
                {
                    
                    Texture2D frame;
                    Color[] newframe = new Color[file.Height * newWidth]; 
                    for (int loop_y = 0; loop_y < file.Height; loop_y++)
                    {
                        for(int loop_x = dummy_Value; loop_x < (dummy_Value + newWidth); loop_x++)
                        {
                            int indexed_in_file = loop_x + (loop_y * file.Width);
                            int indexed_in_game = loop_x + (loop_y * newWidth);
                            if (color[loop_x + (loop_y * file.Width)].ToString().Equals(AnimationColor))
                            {
                                newframe[(loop_x - dummy_Value) + (loop_y * newWidth)] = new Color(0, 0, 0, 0);
                            }
                            else
                            {
                                newframe[(loop_x - dummy_Value) + (loop_y * newWidth)] = color[loop_x + (loop_y * file.Width)];
                            }
                        }
                    }


                    frame = new(_graphicDevice, newWidth, file.Height);
                    frame.SetData(newframe);

                    dummy_Value += newWidth;
                    result[iteration] = frame;
                }*/

                return result;
            }

        }

        

        static public (int,int,Point) createHitbox(string fileName, ContentManager content)
        {
            (int, int, Point) value;
            
            if (getHitbox.TryGetValue(fileName, out value))
            {
                return value;
            }
            else
            {
                Texture2D mask = content.Load<Texture2D>("hitbox2d/" + fileName);
                bool firstPointFound = false;
                Point firstpoint = new(0, 0);
                Point lastpoint = new(0, 0);

                Color[] color = new Color[mask.Height * mask.Width];
                mask.GetData(color);
                for (int loop_y = 0; loop_y < mask.Height; loop_y++)
                {
                    for (int loop_x = 0; loop_x < mask.Width; loop_x++)
                    {
                        Color currentPixel = color[loop_x + (loop_y * mask.Width)];
                        string hexTest = currentPixel.ToString();
                        if (hexTest.Equals(HitboxColor))
                        {
                            if (firstPointFound)
                            {
                                lastpoint = new Point(loop_x, loop_y);
                            }
                            else
                            {
                                firstpoint = new Point(loop_x, loop_y);
                                firstPointFound = true;

                            }
                        }
                    }
                }
                value = new(lastpoint.X - firstpoint.X, lastpoint.Y - firstpoint.Y, firstpoint);
                getHitbox.Add(fileName, value);
                return value;
            }

            

        }
    }
}
