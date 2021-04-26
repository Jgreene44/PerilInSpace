using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Runtime.InteropServices;

namespace PerilInSpace
{
    public class GameBackground
    {
        private Texture2D texture;
        private Game game;

        public GameBackground(Game game)
        {
            this.game = game;
        }


        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("gameBackground");
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var viewport = game.GraphicsDevice.Viewport;
            Rectangle source = new Rectangle(0, 0, viewport.Width, viewport.Height);
            spriteBatch.Draw(texture, Vector2.Zero, source, Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);

        }
    }
}
