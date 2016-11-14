using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace new_pacman
{
    class GameObject
    {
        public Vector2 pos;
        protected Texture2D tex;
        protected Rectangle srcrec, hitbox;
        public GameObject(Texture2D tex, Vector2 pos, Rectangle srcrec, Rectangle hitbox)
        {
            this.tex = tex;
            this.pos = pos;
            this.srcrec = srcrec;
            this.hitbox = hitbox;
        }

        

        public virtual void Update(GameTime gameTime) 
        {
            hitbox.X = (int)pos.X;
            hitbox.Y = (int)pos.Y;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, srcrec, Color.White);
        }

        public bool Contains(int x, int y)
        {
            return hitbox.Contains(x, y);
        }
    }
}
