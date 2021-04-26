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
    public class ControlsScreen : GameScreen
    {
        ContentManager _content;
        Texture2D _controlsScreen;
        Texture2D _darkPurpleBackground;

        MouseState _currentMouse;
        MouseState _previousMouse;
        public override void Activate()
        {
            base.Activate();

            if (_content == null) _content = new ContentManager(ScreenManager.Game.Services, "Content");

            _controlsScreen = _content.Load<Texture2D>("ControlScreen");
            _darkPurpleBackground = _content.Load<Texture2D>("Backgrounds/darkPurple");
            
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            base.HandleInput(gameTime, input);

            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            if (_currentMouse.LeftButton == ButtonState.Pressed && _previousMouse.LeftButton != ButtonState.Pressed)
            {
                ExitScreen();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.SpriteBatch.Begin(samplerState: SamplerState.PointWrap);
            Rectangle source = new Rectangle(0, 0, ScreenManager.GraphicsDevice.Viewport.Width, ScreenManager.GraphicsDevice.Viewport.Height);
            ScreenManager.SpriteBatch.Draw(_darkPurpleBackground, Vector2.Zero, source, Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
            ScreenManager.SpriteBatch.Draw(_controlsScreen, new Rectangle(80, 0, 632, 480), Color.White);
            ScreenManager.SpriteBatch.End();
        }
    }
}
