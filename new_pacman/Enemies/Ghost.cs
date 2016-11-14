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
    class Ghost:Game_objects
    {
        List<string> map = new List<string>();
        Texture2D spriteSheet;
        Tiles tiles;
        AStarProgram AStar;
        SearchParameters sP;
        PathFinder pathF;
        pacman pac;
        List<Point> path;
        Direction direction = Direction.StandingStill;
        SpriteEffects pacFx;
        float rotation;
        float scale;
        int frame;
        public static Vector2 targetpos;
        Random rand = new Random();
        Rectangle src_rec2;
        Rectangle src_rec3;
        Rectangle src_rec4;
        int dire;
        public bool[,] Map;

        double frameTimer = 10f;
        double frameInterval = 75f;

        public Ghost(Texture2D tex, Rectangle src_rec, Rectangle pac_hitrec, Vector2 pos, Tiles tiles,pacman pac)
            : base(tex, src_rec, pac_hitrec, pos)
        {
            targetpos = this.pos;
            this.tiles = tiles;
            int dire = Rand(0, 5);    
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
            src_rec = new Rectangle(0, 16, 16, 16);
            src_rec2 = new Rectangle(32, 16, 16, 16);
            src_rec3 = new Rectangle(64, 16, 16, 16);
            src_rec4 = new Rectangle(96, 16, 16, 16);
        }

        public override void Update(GameTime gameTime)
        {
            InitializeMap(16, 6);

            
            
            
            

            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;


            //int dire = Rand(0, 5);

            if (frameTimer <= 0)
            {
                frameTimer = frameInterval;

                
                    src_rec.X = (frame % 2) * 16;
                
                
                
            }

           

                PathFinder pathF = new PathFinder(sP);
                List<Point> path = pathF.FindPath();

                if (path.Count != 0 && targetpos==pos)
                {
                    targetpos.X = path.First().X * 16;
                    targetpos.Y = path.First().Y * 16;

                }
            if (targetpos != pos)
            {

                if (targetpos.X < pos.X)
                    pos.X -= 1f;
                else if (targetpos.X > pos.X)
                    pos.X += 1f;
                else if (targetpos.Y < pos.Y)
                    pos.Y -= 1f;
                else if (targetpos.Y > pos.Y)
                    pos.Y += 1f;
            }

            
            
        }
       


        public override void Draw(SpriteBatch sb)
        {
             
             
            sb.Draw(spriteSheet, pos, src_rec, Color.White, rotation, new Vector2(8, 8), 1, SpriteEffects.None, 1);  
            
            
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
        public bool GhostPlayerContact(Vector2 playerpos)
        {
            if (playerpos == pos)
            {
                return true;
            }
            return false;
        }

        
        
        public int Rand(int min, int max)
        {
            int result = rand.Next(min, max);
            return result;
        }

        public void InitializeMap(int i, int j)
        {
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □
            //  □ S □ □ □ F □
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □

            this.Map = new bool[i, j];
            for (int y = 0; y < j; y++)
                for (int x = 0; x < i; x++)
                    Map[x, y] = true;

            for (int s = 0; s < map.Count; s++)
            {
                for (int d = 0; d < map[s].Length; d++)
                {
                    if (map[s][d] == 'w')
                    {

                        AddWalls(s, d);
                    }
                }
            }

            var startLocation = new Point((int)pacman.targetpos.X/16, (int)pacman.targetpos.Y/16);
            var endLocation = new Point((int)pos.X/16, (int)pos.Y/16);
            this.sP = new SearchParameters(startLocation, endLocation, Map);
        }
        public void AddWalls(int i, int j)
        {
            Map[j, i] = false;
        }
    }
}
