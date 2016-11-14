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
    class Tiles:Game_objects
    {
        
        List<string> map = new List<string>();

        Texture2D tex_wall;
        Texture2D SpriteSheet;
        
        public Tiles(Texture2D tex, Rectangle src_rec, Rectangle hit_box, Vector2 pos)
            : base(tex, src_rec,hit_box, pos)
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
                    
                            

            src_rec = new Rectangle(60, 0, 16, 16);
            tex_wall = Content.Load<Texture2D>("square.png");
            SpriteSheet = Content.Load<Texture2D>("SpriteSheet.png");
        }
        


        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j] == 'w')
                        spriteBatch.Draw(tex_wall, new Vector2(16 * j, 16 * i), new Rectangle(0, 0, 16, 16), Color.Green, 0, new Vector2(8,8), 1, SpriteEffects.None, 1);
                    
                    
                    
                }

            }
            
        }

    }
}
