using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace PerilInSpace.Collisions
{
    public struct BoundingCircle
    {
        public Vector2 Center;

        public float Radius;

        public BoundingCircle(Vector2 center, float rad)
        {
            Center = center;
            Radius = rad;
        }

        public bool CollidesWith(BoundingCircle other)
        {
            return CollisionHelper.Collides(this, other);
        }
    }
}
