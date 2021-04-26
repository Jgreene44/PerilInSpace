using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Runtime.InteropServices;
using PerilInSpace.Collisions;


namespace PerilInSpace
{
    public class Bullet : Sprite
    {
        private float _timer;

        public BoundingRectangle bulletBounds;

        public Bullet(Texture2D texture)
            :base(texture)
        {

        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            
            if(_timer > LifeSpan)
            {
                IsRemoved = true;
            }

            //TODO CHANGE LINEAR VELOCITY
            Position += Direction * LinearVelocity;
            bounds = new BoundingCircle(Position, Width/2);
        }
    }
}
