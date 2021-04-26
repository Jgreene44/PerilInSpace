using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace PerilInSpace.Collisions
{
    /// <summary>
    /// This class was modified from the CollisionExample tutorial in CIS 580
    /// </summary>
    public struct BoundingPoint
    {
        /// <summary>
        /// X location
        /// </summary>
        public float X;
        /// <summary>
        /// Y location
        /// </summary>
        public float Y;
        /// <summary>
        /// Construcotr 1
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public BoundingPoint(float x, float y)
        {
            X = x;
            Y = y;


        }
        /// <summary>
        /// Constructor 2
        /// </summary>
        /// <param name="position"></param>
        public BoundingPoint(Vector2 position)
        {
            X = position.X;
            Y = position.Y;


        }
    }
}