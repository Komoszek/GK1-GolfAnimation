using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Diagnostics;

namespace GK1_GolfAnimation.Models
{
    class Ball: Model3D
    {
        public Ball()
        {
            int N = 8;
            int b = N * 4;
            /*vertices.Add(new Vector4(0, 0, 0, 1));

            triangles.Add(new Triangle(4, 5, 7));
            */
            
            int sliceCount = N;
            int polygonDegree = b;
            int k = b;
            vertices.Add(new Vector4(0, 0, 1, 1));

            double alpha = Math.PI / (2.0 * N);
            double beta = Math.Tau/ k;

            double ia = alpha;

            // up vertices
            for (int i = 0; i < N; i++)
            {
                double r = Math.Sin(ia);
                double z = Math.Cos(ia);
                double ib = (-beta / 2) * i;

                for (int j = 0; j < k; j++)
                {
                    double x = r * Math.Cos(ib);
                    double y = r * Math.Sin(ib);
                    vertices.Add(new Vector4((float)x, (float)y, (float)z, 1));

                    ib += beta;
                }

                ia += alpha;
            }

            ia -= 2*alpha;

            int l = N;

            for (int i = N-2; i >= 0; i--)
            {
                double r = Math.Sin(ia);
                double z = -Math.Cos(ia);
                double ib = (-beta / 2.0) * (l);

                for (int j = 0; j < k; j++)
                {
                    double x = r * Math.Cos(ib);
                    double y = r * Math.Sin(ib);
                    vertices.Add(new Vector4((float)x, (float)y, (float)z, 1));

                    ib += beta;
                }

                ia -= alpha;
                l++;
            }

            vertices.Add(new Vector4(0, 0, -1, 1));
            

            // first Row
            for(int i = 1; i < b; i++)
            {
                triangles.Add(new Triangle(0, i, i+1));
            }

            triangles.Add(new Triangle(0, b, 1));

            //down triangles upper half

            int upperHalfVertices = 1 + k * N;
            
            for(int i = 1; i < upperHalfVertices - polygonDegree; i++)
            {
                int k1 = i + polygonDegree;
                int k2 = i + polygonDegree + 1;
                if (i % polygonDegree == 0)
                    k2 -= polygonDegree;
                triangles.Add(new Triangle(i, k1, k2));
            }
            
            // up triangles upper half
            
            for (int i = 1 + polygonDegree; i < upperHalfVertices; i++)
            {
                int k1 = i - polygonDegree;
                int k2 = i - polygonDegree - 1;

                if ((i - 1) % polygonDegree == 0)
                    k2 += polygonDegree;

                triangles.Add(new Triangle(i, k1, k2));
            }
            
            for (int i = upperHalfVertices - polygonDegree; i < vertices.Count - polygonDegree-1; i++)
            {
                int k1 = i + polygonDegree;
                int k2 = i + polygonDegree + 1;
                if (i % polygonDegree == 0)
                    k2 -= polygonDegree;

                triangles.Add(new Triangle(i, k1, k2));
            }

            for (int i = upperHalfVertices; i < vertices.Count-1; i++)
            {
                int k1 = i - polygonDegree;
                int k2 = i - polygonDegree - 1;

                if ((i - 1) % polygonDegree == 0)
                    k2 += polygonDegree;

                triangles.Add(new Triangle(i, k1, k2));
            }




            
            //last Row
            int vLast = vertices.Count - 1;
            int vLastRowFirst = vLast - b;
            for (int i = 0; i < b - 1; i++)
            {
                triangles.Add(new Triangle(vLast, vLastRowFirst + i +1, vLastRowFirst + i ));
            }

            triangles.Add(new Triangle(vLast, vLastRowFirst, vLastRowFirst + b - 1));


            NormalComputer.computeNormals(this);


        }
    }
}
