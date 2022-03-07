using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Numerics;

namespace GK1_GolfAnimation.Lights
{

    class PointLight: Light
    {
        private Vector3 position;
        private Color color;
        public PointLight(Vector3 pos, Color c)
        {
            position = pos;
            color = c;

        }

        public Color GetLightColor(Vector3 observer)
        {
            return color;
        }

        public Vector3 GetLightDirection(Vector3 observer)
        {
            return Vector3.Normalize(position - observer);
        }
    }
}
