using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace new_pacman
{
    class HighScoreText
    {
        KeyboardState oldKeyState;
        KeyboardState keyState;
        public string name = "";

        public void TextUpdate(GameTime gT)
        {
            oldKeyState = keyState;

            keyState = Keyboard.GetState();

            foreach (Keys key in keyState.GetPressedKeys())
            {

                if (oldKeyState.IsKeyUp(key))
                {

                    if (key == Keys.Back)
                    {

                        name = name.Remove(name.Length - 1, 1);

                    }

                    else if (key == Keys.Enter)
                    {

                        

                        name = "";

                    }

                    else
                    {
                        if (key == Keys.Space)
                        {
                            name += " ";
                        }
                        else
                        {
                        name += key.ToString();

                        }
                    }
                }
            }
        }
    }
}
