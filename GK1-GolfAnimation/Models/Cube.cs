using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace GK1_GolfAnimation
{
    class Cube: Model3D
    {
        public Cube()
        {
            vertices.Add(new Vector4(-0.5f, -0.5f, -0.5f, 1));
            vertices.Add(new Vector4(0.5f, -0.5f, -0.5f, 1));
            vertices.Add(new Vector4(0.5f, 0.5f, -0.5f, 1));
            vertices.Add(new Vector4(-0.5f, 0.5f, -0.5f, 1));

            vertices.Add(new Vector4(-0.5f, -0.5f, 0.5f, 1));
            vertices.Add(new Vector4(0.5f, -0.5f, 0.5f, 1));
            vertices.Add(new Vector4(0.5f, 0.5f, 0.5f, 1));
            vertices.Add(new Vector4(-0.5f, 0.5f, 0.5f, 1));

            //up
            triangles.Add(new Triangle(4, 5, 7));
            triangles.Add(new Triangle(5, 6, 7));

            //down
            triangles.Add(new Triangle(0, 3, 1));
            triangles.Add(new Triangle(1, 3, 2));

            

            triangles.Add(new Triangle(6, 5, 2));
            triangles.Add(new Triangle(5, 1, 2));

            triangles.Add(new Triangle(7, 6, 3));
            triangles.Add(new Triangle(6, 2, 3));

            triangles.Add(new Triangle(4, 7, 0));
            triangles.Add(new Triangle(7, 3, 0));

            triangles.Add(new Triangle(5, 4, 1));
            triangles.Add(new Triangle(4, 0, 1));



            NormalComputer.computeNormals(this);
        }
    }
}
