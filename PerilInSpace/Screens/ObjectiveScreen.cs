using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PerilInSpace.StateManagement;

namespace PerilInSpace.Screens
{
    public class ObjectiveScreen : GameScreen
    {
        ContentManager _content;
        Texture2D _background;
        TimeSpan _displayTime;

        MouseState _currentMouse;
        MouseState _previousMouse;

        bool buffer = true;
        public override void Activate()
        {
            base.Activate();

            if (_content == null) _content = new ContentManager(ScreenManager.Game.Services, "Content");

            _background = _content.Load<Texture2D>("ObjectiveScreen");
            //_displayTime = TimeSpan.FromSeconds(2);
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            base.HandleInput(gameTime, input);

            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            if (_currentMouse.LeftButton == ButtonState.Pressed && _previousMouse.LeftButton != ButtonState.Pressed && !buffer)
            {
                ExitScreen();
            }
            buffer = false;
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.SpriteBatch.Begin();
            ScreenManager.SpriteBatch.Draw(_background, new Rectangle(80,0, 632, 480), Color.White);
            ScreenManager.SpriteBatch.End();
        }
    }
}
