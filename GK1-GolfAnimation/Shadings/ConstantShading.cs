using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using GK1_GolfAnimation.Lights;
using System.Diagnostics;

namespace GK1_GolfAnimation.Shadings
{
    internal class ConstantShading : IShading
    {
        private Color color;

        public ConstantShading(Vector4 normal, Vector3 pos, List<Light> lights, float ka, float kd, float ks, Color objectColor, Color Ia, int N, Vector3 VPos)
        {
            Vector3 newNormal = Vector3.Normalize(new Vector3(normal.X, normal.Y, normal.Z));

            color = IlluminationModel.calculateColor(pos, ka, objectColor, Ia, lights, kd, newNormal, ks, N, Vector3.Normalize(VPos - pos));
        }
        public Color ComputeColor(int x, int y)
        {
            return color;
        }
    }
}
