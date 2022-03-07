using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GK1_GolfAnimation
{

    public class Helper
    {
        public static float ParallelogramArea(Point p1, Point p2, Point p3)
        {
            float dx1 = p2.X - p1.X;
            float dx2 = p3.X - p1.X;

            float dy1 = p2.Y - p1.Y;
            float dy2 = p3.Y - p1.Y;

            return Math.Abs(dx1 * dy2 - dx2 * dy1);
        }

        public static float ParallelogramSignedArea(float x1, float y1, float x2, float y2, float x3, float y3)
        {
            return (x2 - x1) * (y3 - y1) - (x3 - x1) * (y2 - y1);
        }
        public static float ParallelogramArea(float x1, float y1, float x2, float y2, float x3, float y3)
        {
            return Math.Abs(ParallelogramSignedArea(x1, y1, x2, y2, x3, y3));
        }


        public static (float, float, float) calculateVWU(int x, int y, Point[] triangle)
        {
            float u = Helper.ParallelogramArea(x, y, triangle[0].X, triangle[0].Y, triangle[1].X, triangle[1].Y);
            float v = Helper.ParallelogramArea(x, y, triangle[1].X, triangle[1].Y, triangle[2].X, triangle[2].Y);
            float w = Helper.ParallelogramArea(x, y, triangle[0].X, triangle[0].Y, triangle[2].X, triangle[2].Y);
            float N = u + v + w;
            u /= N;
            v /= N;
            w /= N;

            return (v, w, u);
        }

        public static (float, float, float) calculateVWU(int x, int y, PointF[] triangle)
        {
            float u = Helper.ParallelogramArea(x, y, triangle[0].X, triangle[0].Y, triangle[1].X, triangle[1].Y);
            float v = Helper.ParallelogramArea(x, y, triangle[1].X, triangle[1].Y, triangle[2].X, triangle[2].Y);
            float w = Helper.ParallelogramArea(x, y, triangle[0].X, triangle[0].Y, triangle[2].X, triangle[2].Y);
            float N = u + v + w;
            u /= N;
            v /= N;
            w /= N;

            return (v, w, u);
        }

        public static float calculateFloat(float[] values, float v, float w, float u)
        {
            return v * values[0] + w * values[1] + u * values[2];
        }


        public static Vector4 calculateVector4(Vector4[] vector4s, float v, float w, float u)
        {
            return v * vector4s[0] + w * vector4s[1] + u * vector4s[2];
        }

        public static Vector4 recalculatePoint(Vector4 v, Matrix4x4 m)
        {

            float vX = m.M11 * v.X + m.M12 * v.Y + m.M13 * v.Z + m.M14 * v.W;
            float vY = m.M21 * v.X + m.M22 * v.Y + m.M23 * v.Z + m.M24 * v.W;
            float vZ = m.M31 * v.X + m.M32 * v.Y + m.M33 * v.Z + m.M34 * v.W;
            float vW = m.M41 * v.X + m.M42 * v.Y + m.M43 * v.Z + m.M44 * v.W;
            return new Vector4(vX, vY, vZ, vW);
        }
    }

}
