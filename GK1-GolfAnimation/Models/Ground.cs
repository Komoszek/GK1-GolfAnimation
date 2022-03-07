using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GK1_GolfAnimation.Models
{
    internal class Ground: Model3D
    {
        readonly private int R = 10;
        readonly private int NextLine;

        private void addBase(float z)
        {
            for(int i = 0; i < NextLine; i++)
            {
                for(int j = 0; j < NextLine; j++)
                {
                    vertices.Add(new Vector4(j*(10/R) - 10, i*(10/R) - 10, z, 1));
                }

            }

        }
        public Ground()
        {
            NextLine = R * 2 + 1;
            addBase(0);
            int s = vertices.Count;

            for (int i = 0; i < s- NextLine; i++)
            {
                if (i > 0 && (i + 1) % (NextLine) == 0) continue;
                triangles.Add(new Triangle(i, i + 1, i + NextLine));
                triangles.Add(new Triangle(i + NextLine, i + 1, i + NextLine + 1));
                

            }


            addBase(-1);

            int lastLine = NextLine * (NextLine - 1);

            for(int i = 0; i < R*2; i++)
            {
                int k = NextLine * i;
                int ks = k + s;

                int k2 = k + R * 2;
                int k2s = k2 + s;
                
                triangles.Add(new Triangle(k, k + NextLine, ks));
                triangles.Add(new Triangle(ks, k + NextLine, ks + NextLine));
                
                triangles.Add(new Triangle(k2s, k2 + NextLine, k2));
                triangles.Add(new Triangle(k2s, k2s + NextLine, k2 + NextLine));



                triangles.Add(new Triangle(i, i + s, i + 1));
                triangles.Add(new Triangle(i+s, i + s + 1, i + 1));

                triangles.Add(new Triangle(lastLine + i, lastLine + i + 1, lastLine + i + s));
                triangles.Add(new Triangle(lastLine +i + s, lastLine + i + 1, lastLine + i + 1 + s));


            }







            NormalComputer.computeNormals(this);
        }
    }
}
