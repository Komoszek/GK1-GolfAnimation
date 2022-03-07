using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GK1_GolfAnimation.Lights
{
    internal class DirectionLight : Light
    {
        private Color color;
        private Vector3 direction;

        public DirectionLight(Vector3 direction, Color color)
        {
            this.direction = Vector3.Normalize(direction);
            this.color = color;
        }
        public Color GetLightColor(Vector3 observer)
        {
            return color;
        }

        public Vector3 GetLightDirection(Vector3 observer)
        {
            return direction;
        }
    }
}
