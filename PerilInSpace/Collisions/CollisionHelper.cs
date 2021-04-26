using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace PerilInSpace.Collisions
{
    public static class CollisionHelper
    {
        public static bool Collides(BoundingCircle a, BoundingCircle b)
        {
            return Math.Pow(a.Radius + b.Radius, 2) >= 
                Math.Pow(a.Center.X - b.Center.X, 2) +
                Math.Pow(a.Center.Y - b.Center.Y, 2);
        }

        public static bool Collides(BoundingRectangle r1, BoundingRectangle r2)
        {
            return !(r1.X + r1.Width < r2.X    // r1 is to the left of r2
                    || r1.X > r2.X + r2.Width     // r1 is to the right of r2
                    || r1.Y + r1.Height < r2.Y    // r1 is above r2 
                    || r1.Y > r2.Y + r2.Height); // r1 is below r2
        }

        /// <summary>
        /// Determines if there is a collision between a circle and rectangle
        /// </summary>
        /// <param name="r">The bounding rectangle</param>
        /// <param name="c">The bounding circle</param>
        /// <returns>true for collision, false otherwise</returns>
        public static bool Collides(BoundingRectangle r, BoundingCircle c)
        {

            float nX = MathHelper.Clamp(c.Center.X, r.Left, r.Right);
            float nY = MathHelper.Clamp(c.Center.Y, r.Top, r.Bottom);
            return Math.Pow(c.Radius, 2) >= Math.Pow(c.Center.X - nX, 2) + Math.Pow(c.Center.Y - nY, 2);
        }
        /// <summary>
        /// Alternate call
        /// </summary>
        /// <param name="c"></param>
        /// <pa
    }
}
