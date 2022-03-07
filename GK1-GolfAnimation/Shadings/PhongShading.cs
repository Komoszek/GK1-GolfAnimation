using GK1_GolfAnimation.Lights;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GK1_GolfAnimation.Shadings
{
    internal class PhongShading : IShading
    {
        private Color color;
        private Vector3[] normals;
        private Vector3[] verticesPos;
        private Point[] precalculatedPoints;
        private float ka;
        private float ks;
        private float kd;
        private int N;
        private List<Light> lights;
        private Vector3 VPos;
        private Color Ia;
        
        public PhongShading(Point[] precalculatedPoints, Vector4[] vertices, Vector4[] verticesNormals, List<Light> lights,
                                float ka, float kd, float ks, Color objectColor, Color Ia, int N, Vector3 VPos)
        {
            this.precalculatedPoints = precalculatedPoints;
            this.normals = new Vector3[3];
            this.verticesPos = new Vector3[3];
            this.ka = ka;
            this.ks = ks;
            this.kd = kd;
            this.N = N;
            this.VPos = VPos;
            this.lights = lights;
            this.color = objectColor;
            this.Ia = Ia;

            for(int i = 0; i < 3; i++)
            {
                this.verticesPos[i] = new Vector3(vertices[i].X, vertices[i].Y, vertices[i].Z);
                this.normals[i] = Vector3.Normalize(new Vector3(verticesNormals[i].X , verticesNormals[i].Y , verticesNormals[i].Z ));
            }


        }
        public Color ComputeColor(int x, int y)
        {
            var (v, w, u) = Helper.calculateVWU(x, y, precalculatedPoints);

            Vector3 pos = v * verticesPos[0] + w * verticesPos[1] + u * verticesPos[2];

            float normX = v * normals[0].X + w * normals[1].X + u * normals[2].X;
            float normY = v * normals[0].Y + w * normals[1].Y + u * normals[2].Y;
            float normZ = v * normals[0].Z + w * normals[1].Z + u * normals[2].Z;

            Vector3 normal = Vector3.Normalize(new Vector3(normX, normY, normZ));

            return IlluminationModel.calculateColor(pos, ka, color, Ia, lights, kd, normal, ks, N, Vector3.Normalize(VPos - pos));
        }
    }
}
