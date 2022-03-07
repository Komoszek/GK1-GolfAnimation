using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
namespace GK1_GolfAnimation
{
    class Projection
    {
        private float near;
        private float far;
        private float fov;
        private Matrix4x4 matrix;
        private float aspect;

        bool changed;

        public Projection()
        {
            near = 1;
            far = 100;
            fov = 45;
            aspect = 1;

            changed = true;
        }

        public float Near
        {
            get => near;
            set
            {
                if(near != value)
                {
                    changed = true;
                    near = value;
                }
            }
        }

        public float Far
        {
            get => far;
            set
            {
                if (far != value)
                {
                    changed = true;
                    far = value;
                }
            }
        }

        public float FOV
        {
            get => fov;
            set
            {
                if (fov != value)
                {
                    changed = true;
                    fov = value;
                }
            }
        }

        public float Aspect
        {
            get => aspect;
            set
            {
                if (aspect != value)
                {
                    changed = true;
                    aspect = value;
                }
            }
        }

        public Matrix4x4 Matrix
        {
            get
            {
                if (changed)
                {
                    updateProjectionMatrix();
                }

                return matrix;
            }
        }

        public void updateProjectionMatrix()
        {
            float rad = (float)(fov / 180.0f * Math.PI);
            float e = 1.0f / (float)Math.Tan((float)(rad / 2.0f));
            matrix.M11 = e;
            matrix.M22 = e / aspect;
            matrix.M33 = -(far + near) / (far - near);
            matrix.M34 = -(2 * far * near) / (far - near);
            matrix.M43 = -1;

            changed = false;
        }

    }
}
