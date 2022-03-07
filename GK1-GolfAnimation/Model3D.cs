using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Diagnostics;

namespace GK1_GolfAnimation
{
    struct Triangle
    {
        public int[] idx;

        public Triangle(int v1, int v2, int v3)
        {
            idx = new int[3];
            idx[0] = v1;
            idx[1] = v2;
            idx[2] = v3;
        }
    }

    static class NormalComputer
    {
        public static void computeNormals(Model3D m) {
            for(int i = 0; i < m.vertices.Count; i++)
                m.verticesNormals.Add(Vector4.Zero);

            for (int i = 0; i < m.triangles.Count; i++)
            {
                Vector4 A = m.vertices[m.triangles[i].idx[1]] - m.vertices[m.triangles[i].idx[0]];
                Vector4 B = m.vertices[m.triangles[i].idx[2]] - m.vertices[m.triangles[i].idx[0]];
                Vector4 N = new Vector4(A.Y * B.Z - A.Z * B.Y, A.Z * B.X - A.X * B.Z, A.X * B.Y - A.Y * B.X, 0);
                m.faceNormals.Add(N);
                
                m.verticesNormals[m.triangles[i].idx[0]] += N;
                m.verticesNormals[m.triangles[i].idx[1]] += N;
                m.verticesNormals[m.triangles[i].idx[2]] += N;
            }

            for (int i = 0; i < m.verticesNormals.Count; i++)
            {
                m.verticesNormals[i] = Vector4.Normalize(m.verticesNormals[i]);
            }
        }
    }

    abstract class Model3D
    {
        public List<Vector4> vertices = new List<Vector4>();
        public List<Triangle> triangles = new List<Triangle>();
        public List<Vector4> faceNormals = new List<Vector4>();

        // calculated by averaging adjacent face normals
        public List<Vector4> verticesNormals = new List<Vector4>();
    }
}
