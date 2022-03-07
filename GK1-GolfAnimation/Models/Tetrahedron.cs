using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Diagnostics;

namespace GK1_GolfAnimation
{
    class Tetrahedron: Model3D
    {
        public Tetrahedron(Vector4 p1, Vector4 p2, Vector4 p3, Vector4 p4)
        {
            vertices.Add(p1);
            vertices.Add(p2);
            vertices.Add(p3);
            vertices.Add(p4);

            triangles.Add(new Triangle(0, 2, 1));
            triangles.Add(new Triangle(1, 2, 3));
            triangles.Add(new Triangle(0, 3, 2));
            triangles.Add(new Triangle(0, 1, 3));

            NormalComputer.computeNormals(this);

        }
    }
}
