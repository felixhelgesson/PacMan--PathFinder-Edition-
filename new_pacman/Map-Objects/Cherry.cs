using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace new_pacman
{
    class Cherry : Food
    {
        public Cherry(Texture2D tex, Rectangle srcrec, Rectangle hitbox, Vector2 pos, Vector2 pos2) : base(tex, srcrec, hitbox,pos,pos2)
        {

        }

        //public override void Update(GameTime gameTime)
        //{
        //    base.Update(gameTime);
        //}
    }
}
