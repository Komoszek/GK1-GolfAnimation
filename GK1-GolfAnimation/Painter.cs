using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;
using GK1_GolfAnimation.Shadings;
using GK1_GolfAnimation.Lights;
using System.Drawing.Imaging;

namespace GK1_GolfAnimation
{
    static class Painter
    {
        private static int PreprocessAET(int k, int y, int[] idx, Point[] vertices, List<AETPointer> AET)
        {
            while (k < idx.Length && vertices[idx[k]].Y < y)
            {
                int curr = idx[k];

                if (vertices[curr].Y + 1 == y)
                {
                    int prev = (curr - 1 + idx.Length) % idx.Length;
                    int next = (curr + 1) % idx.Length;

                    if (vertices[next].Y > vertices[curr].Y)
                    {
                        AET.Add(new AETPointer(curr, next, vertices[curr].X, vertices[curr].Y, vertices[next].X, vertices[next].Y));
                    }
                    else
                    {
                        AET.removeAETPointerWithIds(next, curr);
                    }

                    if (vertices[prev].Y > vertices[curr].Y)
                    {
                        AET.Add(new AETPointer(curr, prev, vertices[curr].X, vertices[curr].Y, vertices[prev].X, vertices[prev].Y));
                    }
                    else
                    {
                        AET.removeAETPointerWithIds(prev, curr);
                    }
                }

                k++;
                if (k >= idx.Length)
                    break;
            }

            AET.Sort((AETPointer a, AETPointer b) =>
            {
                return a.x.CompareTo(b.x);
            });

            return k;
        }

        private static void updatePixel(int x, int y, int width, float[,] zBuffer, IShading shading, float[] zValues, Byte[] argbData, Point[] vertices, Fog fog, float[] worldDist)
        {
            int t = (y * width + x) * 4;
            var (v, w, u) = Helper.calculateVWU(x, y, vertices);

            float z = MathF.Log(Helper.calculateFloat(zValues, v, w, u));


            if (z < zBuffer[x, y])
            {
                zBuffer[x, y] = z;

                Color c = shading.ComputeColor(x, y);

                if (fog != null)
                {
                    float dist = v * worldDist[0] + w * worldDist[1] + u * worldDist[2];
                    c = fog.applyFog(dist, c);
                }

                argbData[t] = c.B; // B
                argbData[t + 1] = c.G; // G
                argbData[t + 2] = c.R; // R
            }
        }

        public static void fillTriangle(Byte[] argbData, int width, int height, Point[] vertices, IShading shading, float[,] zBuffer, float[] zValues, Fog fog, float[] worldDist)
        {
            int[] idx = new int[vertices.Length];
            for (int i = 0; i < idx.Length; i++)
            {
                idx[i] = i;
            }

            Array.Sort(idx, (int a, int b) =>
            {
                int cmp = vertices[a].Y.CompareTo(vertices[b].Y);
                return cmp == 0 ? vertices[a].X.CompareTo(vertices[b].X) : cmp;
            });

            List<AETPointer> AET = new List<AETPointer>();
            int yMin = vertices[idx[0]].Y;
            int yMax = Math.Min(vertices[idx[idx.Length - 1]].Y, height-1);

            int k = 0;
            for (int y = yMin; y <= yMax; y++)
            {
                k = PreprocessAET(k, y, idx, vertices, AET);
               
                if(y >= 0)
                {
                    for (int i = 0; i < AET.Count; i += 2)
                    {
                        int xMin = Math.Max((int)AET[i].x, 0);
                        int xMax = Math.Min((int)AET[i + 1].x, width);
                        for (int x = xMin; x < xMax; x++)
                        {
                            updatePixel(x, y, width, zBuffer, shading, zValues, argbData, vertices, fog, worldDist);
                        }
                    }
                }

                for (int i = 0; i < AET.Count; i++)
                {
                    AET[i].x += AET[i].mInv;
                }
            }
        }

        private static bool processTriangle(Triangle triangle, Matrix4x4 m, List<Vector4> vertices, float[] zValues, float width, float height, Point[] tempTriangle)
        {
            for (int i = 0; i < 3; i++)
            {
                int vertexId = triangle.idx[i];
                Vector4 vTemp = Helper.recalculatePoint(vertices[vertexId], m);
                zValues[i] = vTemp.Z / vTemp.W;

                if (zValues[i] > 1 || zValues[i] < -1)
                {
                    return false;
                }

                tempTriangle[i] = new Point((int)((vTemp.X / vTemp.W + 1) * width * 0.5f), 
                                            (int)(height - ((vTemp.Y / vTemp.W + 1) * height * 0.5f)));

            }

            return true;
        }


        private static void processFog(out float[] worldDist, Vector4[] modelPos, Triangle triangle, List<Vector4> vertices, Matrix4x4 modelMatrix, Vector3 cameraPos)
        {
            worldDist = new float[3];
            if (modelPos == null)
            {
                modelPos = new Vector4[3];

                for (int i = 0; i < 3; i++)
                {
                    int vertexIdx = triangle.idx[i];
                    modelPos[i] = Helper.recalculatePoint(vertices[vertexIdx], modelMatrix);
                }
            }
            for (int i = 0; i < 3; i++)
            {
                worldDist[i] = (float)Vector3.Distance(new Vector3(modelPos[i].X / modelPos[i].W, modelPos[i].Y / modelPos[i].W, modelPos[i].Z / modelPos[i].W), cameraPos);
            }
        }

        public static void drawModel(float width, float height, SceneObject sceneObject, Matrix4x4 viewMatrix, Matrix4x4 projectionMatrix,
            byte[] argbvalues, int scan, float[,] zBuffer, Vector3 cameraPos, List<Light> lights, float ka, float kd, float ks, int n, ShadingMode shadingMode, Fog fog, Color Ia)
        {
            Model3D model = sceneObject.model;
            Matrix4x4 modelMatrix = sceneObject.ModelMatrix;
            Color[] colors = sceneObject.trianglesColors;
            Matrix4x4 m = Matrix4x4.Multiply(projectionMatrix, Matrix4x4.Multiply(viewMatrix, modelMatrix));

            Parallel.For(0, model.triangles.Count, (j) =>
            //for (int j = 0; j < model.triangles.Count; j++)
            {
                float[] zValues = new float[3];
                Point[] tempTriangle = new Point[3];
                Vector4[] modelPos = null;
                var triangle = model.triangles[j];

                bool triangleInView = processTriangle(triangle, m, model.vertices, zValues, width, height, tempTriangle);

                if (!triangleInView)
                {
                    return; //continue;
                }
                
                // backface culling
                if (Helper.ParallelogramSignedArea(tempTriangle[0].X, tempTriangle[0].Y, 
                                                   tempTriangle[1].X, tempTriangle[1].Y, 
                                                   tempTriangle[2].X, tempTriangle[2].Y) > 0)
                {
                    return; //continue;
                }
                IShading cc;

                switch (shadingMode)
                {
                    case ShadingMode.Constant:
                    default:
                        {
                            Vector4 fNormal = Vector4.Normalize(Helper.recalculatePoint(model.faceNormals[j], sceneObject.NormalMatrix));

                            int vertexIdx = triangle.idx[0];
                            Vector4 vPos = Helper.recalculatePoint(model.vertices[vertexIdx], modelMatrix);
                            Vector3 ccvPos = new Vector3(vPos.X / vPos.W, vPos.Y / vPos.W, vPos.Z / vPos.W);

                            cc = new ConstantShading(fNormal, ccvPos, lights, ka, kd, ks, colors[j], Ia, n, cameraPos);
                        }
                        break;
                    case ShadingMode.Gouraud:
                    case ShadingMode.Phong:
                        {
                            modelPos = new Vector4[3];
                            Vector4[] vNormals = new Vector4[3];
                            for (int i = 0; i < 3; i++)
                            {
                                int vertexIdx = triangle.idx[i];
                                modelPos[i] = Helper.recalculatePoint(model.vertices[vertexIdx], modelMatrix);
                                vNormals[i] = Vector4.Normalize(Helper.recalculatePoint(model.verticesNormals[vertexIdx], sceneObject.NormalMatrix));
                            }

                            if (shadingMode == ShadingMode.Gouraud) {
                                cc = new GouraudShading(tempTriangle, modelPos, vNormals, lights
                                                    , ka, kd, ks, colors[j], Ia, n, cameraPos);
                            } else {
                                cc = new PhongShading(tempTriangle, modelPos, vNormals, lights
                                                        , ka, kd, ks, colors[j], Ia, n, cameraPos);
                            }
                            
                        }
                        break;
                }

                float[] worldDist = null;

                if(fog != null)
                {
                    processFog(out worldDist, modelPos, triangle, model.vertices, modelMatrix, cameraPos);
                }

                fillTriangle(argbvalues, (int)width, (int)height, tempTriangle, cc, zBuffer, zValues, fog, worldDist);
            }

            );
        }

        public static void drawScene(Scene scene, int width, int height, Byte[] bitmapBytes, float[,] zBuffer, float ka, float kd, float ks, int n, ShadingMode shadingMode)
        {
            foreach (var obj in scene.sceneObjects)
            {
                Painter.drawModel(width, height, obj, scene.view.Matrix,
                                scene.projection.Matrix, bitmapBytes, width, zBuffer, scene.view.cameraPosition,
                                scene.lights, ka, kd, ks, n, shadingMode, scene.fog, scene.Ia);
            }
        }

        public static void clearSceneHelpers(Byte[] bitmapBytes, Color color, float[,] zBuffer)
        {

            for (int i = 0; i < bitmapBytes.Length; i+=4)
            {
                bitmapBytes[i] = color.B;
                bitmapBytes[i+1] = color.G;
                bitmapBytes[i+2] = color.R;
                bitmapBytes[i+3] = color.A;
            }

            for (int i = 0; i < zBuffer.GetLength(0); i++)
            {
                for (int j = 0; j < zBuffer.GetLength(1); j++)
                {
                    zBuffer[i, j] = float.PositiveInfinity;
                }
            }
        }

        public static void copyBitmapBytes(Bitmap bitmap, Byte[] bitmapBytes)
        {
            BitmapData bmpData = bitmap.LockBits(
                   new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                   ImageLockMode.WriteOnly, bitmap.PixelFormat);

            System.Runtime.InteropServices.Marshal.Copy(bitmapBytes, 0, bmpData.Scan0, bitmapBytes.Length);

            bitmap.UnlockBits(bmpData);
        }
    }
}
