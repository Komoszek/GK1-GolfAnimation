using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace GK1_GolfAnimation.Models
{
    class Pyramid: Model3D
    {
        public Pyramid()
        {
            vertices.Add(new Vector4(0, 0, 1, 1));
            vertices.Add(new Vector4(-1, -1, 0, 1));
            vertices.Add(new Vector4(-1, 1, 0, 1));
            vertices.Add(new Vector4(1, 1, 0, 1));
            vertices.Add(new Vector4(1, -1, 0, 1));

            triangles.Add(new Triangle(0, 2, 1));
            triangles.Add(new Triangle(0, 3, 2));
            triangles.Add(new Triangle(0, 4, 3));
            triangles.Add(new Triangle(0, 1, 4));

            triangles.Add(new Triangle(1, 2, 4));
            triangles.Add(new Triangle(2, 3, 4));

            NormalComputer.computeNormals(this);
        }
    }
}
