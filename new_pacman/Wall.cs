using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace new_pacman
{
    class Wall : GameObject
    {
        public Wall(Texture2D tex, Vector2 pos, Rectangle srcrec, Rectangle hitbox, Color color)
            : base(tex, pos, srcrec, hitbox)
        {

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, srcrec, Color.Blue);
        }
    }
}
