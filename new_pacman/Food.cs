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
    class Food:Game_objects
    {
        Vector2 origin;
        public bool eaten = false;

        public Food(Texture2D tex, Rectangle src_rec, Rectangle hit_box, Vector2 pos, Vector2 origin)
            : base(tex, src_rec, hit_box, pos)
        {
            this.origin = origin;
            origin = new Vector2(8, 8);
        }

        

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!eaten)
            {
                spriteBatch.Draw(tex, pos, src_rec, Color.White, 0, origin, 1, SpriteEffects.None, 1);
            }

            
        }
        public override void Update(GameTime gameTime)
        {
            
        }

    }
}
