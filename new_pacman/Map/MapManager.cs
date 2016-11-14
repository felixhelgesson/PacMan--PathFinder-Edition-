using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace new_pacman
{
    class MapManager
    {
        public Texture2D tex, tex2;
        public bool eaten;
        public List<string> map = new List<string>();
        List<Food> foodList = new List<Food>();
        List<Cherry> cherryList = new List<Cherry>();
        List<Wall> wallList = new List<Wall>();

        public MapManager(Texture2D tex, Texture2D tex2, Vector2 pos, Rectangle srcrec, Rectangle hitbox, bool eaten)
        {
            StreamReader Map1 = new StreamReader(@"Map1.txt");
            while (!Map1.EndOfStream)
            {
                map.Add(Map1.ReadLine());
            }
            Map1.Close();

            MapInfo(tex, tex2, eaten);

            this.tex = tex;
            this.eaten = eaten;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Food f in foodList)
            {
                f.Draw(spriteBatch);
            }
            foreach (Wall w in wallList)
            {
                w.Draw(spriteBatch);
            }
            foreach (Cherry c in cherryList)
            {
                c.Draw(spriteBatch);
            }
        }

        void MapInfo(Texture2D tex, Texture2D tex2, bool eaten)
        {
            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (eaten == false)
                    {
                        if (map[i][j] == '-')
                        {
                            Food newFood = new Food(tex, new Rectangle(104, 81, 32, 32), new Rectangle(30 * j, 30 * i, 30, 30), new Vector2(30 * j, 30 * i),new Vector2(tex.Width/2,tex.Height/2));
                            foodList.Add(newFood);
                        }

                        if (map[i][j] == 'C')
                        {
                            //Cherry newCherry = new Cherry(tex, new Vector2(30 * j, 30 * i), new Rectangle(72, 81, 32, 32), new Rectangle(30 * j, 30 * i, 30, 30));
                            //cherryList.Add(newCherry);
                        }

                        if (map[i][j] == 'W')
                        {
                            Wall newWall = new Wall(tex2, new Vector2(30 * j, 30 * i), new Rectangle(0, 0, 32, 32), new Rectangle(30 * j, 30 * j, 30, 30), Color.Blue);
                            wallList.Add(newWall);
                        }
                    }
                }
            }
        }
        public bool CheckFood(Rectangle rec)
        {
            foreach (Food f in foodList)
            {
                if (f.Contains(rec.X, rec.Y) && f.eaten == false)
                {
                    f.eaten = true;
                    Game1.score++;
                    return true;
                }
            }
            foreach (Cherry c in cherryList)
            {
                if (c.Contains(rec.X, rec.Y) && c.eaten == false)
                {
                    c.eaten = true;
                    Game1.score += 10;
                    return true;
                }
            }
            return false;
        }
        public bool CheckWall(Vector2 pos, Direction dir)
        {
            Point tile = new Point((int)pos.X / 30, (int)pos.Y / 30);
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

            if (map[tile.Y][tile.X] == 'W')
                return false;
            else
                return true;
        }
        public void MapReset()
        {
            foodList.Clear();
            wallList.Clear();
            cherryList.Clear();
        }
    }
}