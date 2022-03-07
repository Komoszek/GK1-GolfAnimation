using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace GK1_GolfAnimation.Models
{
    class GolfHill: Model3D
    {
        public GolfHill()
        {
            float slope = 2.0f;
            vertices.Add(new Vector4(-1, -1, 1, 1));
            vertices.Add(new Vector4(-1, 1, 1, 1));
            vertices.Add(new Vector4(1, 1, 1, 1));
            vertices.Add(new Vector4(1, -1, 1, 1));

            vertices.Add(new Vector4(-slope, -slope, 0, 1));;
            vertices.Add(new Vector4(-slope, slope, 0, 1));
            vertices.Add(new Vector4(slope, slope, 0, 1));
            vertices.Add(new Vector4(slope, -slope, 0, 1));

            triangles.Add(new Triangle(0, 2, 1));
            triangles.Add(new Triangle(0, 3, 2));

            triangles.Add(new Triangle(4, 5, 6));
            triangles.Add(new Triangle(4, 6, 7));

            triangles.Add(new Triangle(0, 1, 4));
            triangles.Add(new Triangle(1, 5, 4));

            triangles.Add(new Triangle(1, 2, 5));
            triangles.Add(new Triangle(2, 6, 5));

            triangles.Add(new Triangle(3, 6, 2));
            triangles.Add(new Triangle(7, 6, 3));

            triangles.Add(new Triangle(0, 7, 3));
            triangles.Add(new Triangle(4, 7, 0));



            NormalComputer.computeNormals(this);
        }
    }
}
