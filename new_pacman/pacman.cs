using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.IO;

namespace new_pacman
{
    class pacman:Game_objects
    {
        List<string> map = new List<string>();
        Texture2D spriteSheet;
        Tiles tiles;
        Direction direction = Direction.StandingStill;
        SpriteEffects pacFx;        
        float rotation;
        int frame;
        public static Vector2 targetpos;
        Food_placement food;
        public bool dead;
        double frameTimer = 10f;
        double frameInterval = 75f;
        public Vector2 poSITION
        {
            get { return pos; }
        }

        

        public pacman(Texture2D tex,Rectangle src_rec,Rectangle pac_hitrec,Vector2 pos,Tiles tiles,Food_placement food,bool dead)
            :base(tex,src_rec,pac_hitrec,pos)
        {
            //this.pos = pos;
           targetpos = this.pos;
           this.tiles = tiles;
           this.food = food;
           
        }
        

        public void Load(ContentManager Content)
        {
            StreamReader sr = new StreamReader(@"MyMap.txt");

            while (!sr.EndOfStream)
            {
                map.Add(sr.ReadLine());
            }

            sr.Close();

            spriteSheet = Content.Load<Texture2D>("SpriteSheet");
            src_rec = new Rectangle(0, 0, 16, 16);            
            
            
            
        }

        public override void Update(GameTime gameTime)
        {
            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;

            if (frameTimer <= 0)
            {
                frameTimer = frameInterval;

                src_rec.X = (frame % 4) * 16;

            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                direction = Direction.Left;
                rotation = MathHelper.ToRadians(0);
                pacFx = SpriteEffects.FlipHorizontally;
                frame++;
                frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                direction = Direction.Right;
                rotation = MathHelper.ToRadians(0);
                pacFx = SpriteEffects.None;
                frame++;
                frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                direction = Direction.Up;
                
                rotation = MathHelper.ToRadians(-90);
                pacFx = SpriteEffects.None;
                frame++;
                frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                direction = Direction.Down;

                rotation = MathHelper.ToRadians(-90);
                pacFx = SpriteEffects.FlipHorizontally;
                frame++;
                frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;

            }

            if (pos == targetpos && direction != Direction.StandingStill)
            {
                if (Checktile(targetpos, direction) == true)
                {
                    switch (direction)
                    {
                        case Direction.Up:
                            targetpos.Y -= 16;
                            break;
                        case Direction.Down:
                            targetpos.Y += 16;
                            break;
                        case Direction.Left:
                            targetpos.X -= 16;
                            break;
                        case Direction.Right:
                            targetpos.X += 16;
                            break;
                        default:
                            break;
                    }
                }

                

            }
            
            if (food.Checktile(pos) == true)
            {
                for (int i = 0; i < food.map.Count; i++)
                {
                    food.eaten = true;
                }
            }

                if (targetpos != pos)
                {
                    if (targetpos.X < pos.X)
                        pos.X -= 2f;
                    else if (targetpos.X > pos.X)
                        pos.X += 2f;
                    else if (targetpos.Y < pos.Y)
                        pos.Y -= 2f;
                    else if (targetpos.Y > pos.Y)
                        pos.Y += 2f;
                }
                if (dead == true)
                {
                    targetpos = new Vector2(64, 48);
                    pos = targetpos;
                    dead = false;
                    direction = Direction.StandingStill;
                    
                }
        }
            
       
            
        
        
        public override void Draw(SpriteBatch sb)
        {
            
            sb.Draw(spriteSheet, pos, src_rec, Color.White, rotation, new Vector2(8, 8), 1, pacFx, 1);
            
        }

        public bool Checktile(Vector2 pos, Direction dir)
        {
            

            
            Point tile = new Point((int)pos.X / 16, (int)pos.Y / 16);
            switch (dir)
            {
                case Direction.Up:
                    tile.Y -= 1;
                    break;
                case Direction.Down:
                    tile.Y += 1;
                    break;
                case Direction.Left:
                    tile.X -= 1;
                    break;
                case Direction.Right:
                    tile.X += 1;
                    break;
                default:
                    break;
            }
            if (map[tile.Y][tile.X] == 'w')
                return false;
            else
                return true;
        }
        
        public bool Checkghost(Vector2 pos)
        {
                Rectangle hitbox=new Rectangle((int)this.pos.X,(int)this.pos.Y,16,16);       
                if (hitbox.Contains((int)pos.X, (int)pos.Y) && dead == false)
                {
                    dead = true;
                    
                    return false;
                }
                return true;
            }
        }
        
    }

