using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;

namespace GK1_GolfAnimation.Lights
{
    class SpotLight: Light
    {
        public Vector3 position;
        public Vector3 direction;
        private Color color;
        private float exponent;

        public SpotLight(Vector3 position, Vector3 direction, Color color, float exponent)
        {
            this.position = position;
            this.direction = Vector3.Normalize(direction);
            this.color = color;
            this.exponent = exponent;
        }


        public void setColor(Color c)
        {
            color = c;
        }

        public void lookAt(Vector3 targetPos)
        {
            direction = Vector3.Normalize(targetPos - position);
        }


        public Color GetLightColor(Vector3 observer)
        {
            float spotLightDot = Vector3.Dot(-direction, GetLightDirection(observer));
            float coef = MathF.Pow(Math.Max(spotLightDot, 0), exponent);

            return Color.FromArgb((int)Math.Min(Math.Max(color.R * coef, 0), 255),
                                    (int)Math.Min(Math.Max(color.G * coef, 0), 255),
                                    (int)Math.Min(Math.Max(color.B * coef, 0), 255));
        }

        public Vector3 GetLightDirection(Vector3 observer)
        {
            return Vector3.Normalize(position - observer);
        }
    }
}
