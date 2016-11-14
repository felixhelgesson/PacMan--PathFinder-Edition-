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
    class Food_placement
    {
        public List<string> map = new List<string>();
        List<Food> foodlist = new List<Food>();
        public bool eaten;
        Texture2D SpriteSheet;
        

        public Food_placement(Texture2D tex, Vector2 pos, Rectangle srcrec, Rectangle hitbox, bool eaten)
        {
        }

        public void Load(ContentManager Content)
        {
            StreamReader sr = new StreamReader(@"MyMap.txt");

            while (!sr.EndOfStream)
            {
                map.Add(sr.ReadLine());
            }

            sr.Close();

            
            SpriteSheet = Content.Load<Texture2D>("SpriteSheet");

            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (eaten == false)
                    {
                        if (map[i][j] == 'f')
                        {
                            Food newFood = new Food(SpriteSheet, new Rectangle(60, 0, 16, 16),new Rectangle(16*j,16*i,16,16), new Vector2(16 * j, 16 * i),new Vector2(8,8));
                            foodlist.Add(newFood);
                        }
                    }
                }
            }
        }
        

        
        public bool Checktile(Vector2 pos)
        {
            foreach (Food f in foodlist)
            {
                if(f.Contains((int)pos.X, (int)pos.Y) && f.eaten == false)
                {
                    f.eaten = true;
                    Game1.dots++;
                    Game1.score++;
                    return true;
                }
            }
            return false;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Food f in foodlist)
            {
                f.Draw(spriteBatch);
            }            
        }

    }
}
