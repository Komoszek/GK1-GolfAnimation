using GK1_GolfAnimation.Lights;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GK1_GolfAnimation.Shadings
{
    internal class GouraudShading : IShading
    {
        private Point[] precalculatedPoints;
        private Color[] colors;

        public GouraudShading(Point[] precalculatedPoints, Vector4[] vertices, Vector4[] verticesNormals, List<Light> lights, 
                                float ka, float kd, float ks, Color objectColor, Color Ia, int N, Vector3 VPos)
        {
            this.precalculatedPoints = new Point[3];
            colors = new Color[3];

            for(int i = 0; i < 3; i++)
            {
                Vector3 pos = new Vector3(vertices[i].X, vertices[i].Y, vertices[i].Z);
                Vector3 vertexNormal = Vector3.Normalize(new Vector3(verticesNormals[i].X, verticesNormals[i].Y, verticesNormals[i].Z));
                Color c = IlluminationModel.calculateColor(pos, ka, objectColor, Ia, lights, kd, vertexNormal, ks, N, Vector3.Normalize(VPos - pos));

                this.precalculatedPoints[i] = precalculatedPoints[i];
                this.colors[i] = c;
            }

        }

        public Color ComputeColor(int x, int y)
        {
            var (v, w, u) = Helper.calculateVWU(x, y, precalculatedPoints);
            int Red = (int)(v * colors[0].R + w * colors[1].R + u * colors[2].R);
            int Green = (int)(v * colors[0].G + w * colors[1].G + u * colors[2].G);
            int Blue = (int)(v * colors[0].B + w * colors[1].B + u * colors[2].B);
            return Color.FromArgb(Math.Max(Math.Min(Red, 255), 0), 
                Math.Max(Math.Min(Green, 255), 0), 
                Math.Max(Math.Min(Blue, 255), 0));
        }
    }
}
