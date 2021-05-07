using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PerilInSpace.Collisions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerilInSpace.Sprites
{
    public class Asteroid
    {
        //Possible Textures for the Asteroids
        //public Texture2D asteroidTexture1;
        //public Texture2D asteroidTexture2;
        //public Texture2D asteroidTexture3;
        //public Texture2D asteroidTexture4;

        //Bounds for the Asteroids
        public BoundingCircle bounds;

        //Position for the Asteroids
        public Vector2 position;

        public float rotation;

        public Vector2 velocity;

        public Texture2D texture;

        public float radius;

        public SpriteBatch spriteBatch;

        public float scale;

        public Asteroid(Texture2D texture, int chooser)
        {
            Random random = new Random();
            position = new Vector2((random.Next(0, Globals.SCREEN_WIDTH)), (random.Next(0, 1) == 0) ? Globals.SCREEN_HEIGHT : 0);
            velocity = new Vector2((float)random.NextDouble() * 4 - 1, (float)random.NextDouble() * 4 - 1);
            rotation = (float)random.NextDouble() * MathHelper.Pi * 4 - (MathHelper.Pi * 2);
            scale = 0.5f;


            //int textureChooser = random.Next(1, 4);
            if (chooser == 1)
            {
                this.texture = texture;
                radius = 92 * scale;
            }
            else if(chooser == 2)
            {
                this.texture = texture;
                radius = 109 * scale;
            }
            else if (chooser == 3)
            {
                this.texture = texture;
                radius = 86 * scale;
            }
            else if (chooser == 4)
            {
                this.texture = texture;
                radius = 97 * scale;
            }

            
        }

        public void Activate(ContentManager content, SpriteBatch spriteBatch)
        {
            //asteroidTexture1 = content.Load<Texture2D>("asteroid1");
            //asteroidTexture2 = content.Load<Texture2D>("asteroid2");
            //asteroidTexture3 = content.Load<Texture2D>("asteroid3");
            //asteroidTexture4 = content.Load<Texture2D>("asteroid4");
            this.spriteBatch = spriteBatch;
        }
        public void Update()
        {
            position += velocity;

            if (position.X + radius < 0)
            {
                position = new Vector2(Globals.SCREEN_WIDTH, position.Y);
            }
            if (position.Y + radius < 0)
            {
                position = new Vector2(position.X, Globals.SCREEN_HEIGHT);
            }
            if (position.X - radius > Globals.SCREEN_WIDTH)
            {
                position = new Vector2(0, position.Y);
            }
            if (position.Y - radius > Globals.SCREEN_HEIGHT)
            {
                position = new Vector2(position.X, 0);
            }

            bounds = new BoundingCircle(position, radius);
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(texture, position, null, Color.White, rotation, new Vector2(radius / 2, radius / 2), scale, SpriteEffects.None, 1.0f);
        }
    }
}
