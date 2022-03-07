using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace GK1_GolfAnimation.Models
{
    class Cylinder: Model3D
    {

        private void AddBase(int N, float z)
        {
            double alpha = Math.Tau / N;
            double ia = 0;
            double r = 1.0;

            for (int i = 0; i < N; i++)
            {
                double x = r * Math.Cos(ia);
                double y = r * Math.Sin(ia);

                vertices.Add(new Vector4((float)x, (float)y, z, 1.0f));


                ia += alpha;
            }
        }
        public Cylinder()
        {
            int N = 8;
            vertices.Add(new Vector4(0, 0, 1, 1.0f));


            AddBase(N, 1.0f);
            AddBase(N, -1.0f);

            vertices.Add(new Vector4(0, 0, -1, 1.0f));

            for (int i = 1; i <= N; i++)
            {
                int i2 = i;
                int i3 = i + 1;
                if (i3 == N + 1)
                    i3 = 1;

                triangles.Add(new Triangle(0, i2, i3));
            }

            int vLastIdx = vertices.Count - 1;


            for (int i = 1; i <= N; i++)
            {
                int i1 = i;
                int i2 = i + N;
                int i3, i4;

                if(i == N)
                {
                    i3 = i + 1;
                    i4 = 1;
                } else
                {
                    i3 = i + N + 1;
                    i4 = i + 1;
                }

                triangles.Add(new Triangle(i1, i2, i3));
                triangles.Add(new Triangle(i1, i3, i4));

            }


            for (int i = N+1; i < vLastIdx; i++)
            {
                int i2 = i;
                int i3 = i + 1;
                if (i3 == vLastIdx)
                    i3 = N+1;

                triangles.Add(new Triangle(vLastIdx, i3, i2));
            }

            NormalComputer.computeNormals(this);
        }
    }
}
