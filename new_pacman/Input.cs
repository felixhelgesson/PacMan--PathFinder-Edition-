using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

static class Input
{
	public static KeyboardState keyState, oldKeyState = Keyboard.GetState();
    public static MouseState mouse, oldmouse = Mouse.GetState();
    public static bool Key(Keys key)
    {
        return keyState.IsKeyDown(key);
    }
    public static bool OneKeypress(Keys key)
    {
        return keyState.IsKeyDown(key) && oldKeyState.IsKeyUp(key);
    }
    public static bool Left()
    {
        return mouse.LeftButton == ButtonState.Pressed && oldmouse.LeftButton == ButtonState.Released;
    }
	public static void Update() 
    {
		oldKeyState = keyState;
		keyState = Keyboard.GetState();
	}
}