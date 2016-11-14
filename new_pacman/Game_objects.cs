using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;


namespace new_pacman
{
    class Game_objects
    {
        protected Vector2 pos;
        protected Rectangle src_rec;
        protected Texture2D tex;
        protected Rectangle hit_box;

        public Game_objects(Texture2D tex, Rectangle src_rec, Rectangle hit_box, Vector2 pos)
        {
            this.tex = tex;
            this.src_rec = src_rec;
            this.hit_box = hit_box;
            this.pos = pos;
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos,src_rec, Color.White);
        }
        
        public bool Contains(int x, int y)
        {
            return hit_box.Contains(x, y);
        }

        public Vector2 GetPos()
        {
            return pos;
        }

        
    }
}
