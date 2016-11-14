using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace new_pacman
{
    class Button
    {
        Texture2D ButtonTex;
        Vector2 ButtonPos;
        Rectangle ButtonRec;
        Color ButtonColor = new Color(225, 225, 255, 255);

        public bool ButtonFade;
        public bool Clicked;

        public Button()
        {

        }

        public void Load(Texture2D tex, Vector2 newpos)
        {
            this.ButtonTex = tex;
            ButtonPos = newpos;
        }
       
        public void Update(MouseState mouse)
        {
            mouse = Mouse.GetState();
            ButtonRec = new Rectangle((int)ButtonPos.X, (int)ButtonPos.Y, ButtonTex.Width, ButtonTex.Height);

            Rectangle mouseRec = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRec.Intersects(ButtonRec))
            {
                if (ButtonColor.A == 255) ButtonFade = false;
                if (ButtonColor.A == 0) ButtonFade = true;
                if (ButtonFade) ButtonColor.A += 3; else ButtonColor.A -= 3;

                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    Clicked = true;
                    ButtonColor.A = 255;
                }              
            }
            else if (ButtonColor.A < 255)
            {
                ButtonColor.A += 3;
                Clicked = false;
            }
        }

        public void draw(SpriteBatch sb)
        {
            sb.Draw(ButtonTex, ButtonRec, ButtonColor);
        }      
    }
}
