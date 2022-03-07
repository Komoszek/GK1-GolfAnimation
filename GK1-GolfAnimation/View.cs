using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace GK1_GolfAnimation
{
    class View
    {
        public Vector3 cameraPosition;
        public Vector3 cameraDirection;
        private Matrix4x4 matrix;
        private Vector3 up;

        bool changed;
        public View()
        {
            up = new Vector3(0, 0, 1);
            cameraDirection = new Vector3(1, 0, 0);
            cameraPosition = new Vector3(0, 0, 0);

            changed = true;
        }

        public Matrix4x4 Matrix
        {
            get
            {
                if (changed)
                {
                    updateView();
                }

                return matrix;

            }
        }


        public void moveTo(Vector3 newPosition)
        {
            if(newPosition != cameraPosition)
            {
                cameraPosition = newPosition;
                changed = true;
            }
        }

        public void lookAt(Vector3 targetPosition)
        {
            lookTowards(cameraPosition - targetPosition);
        }

        public void lookTowards(Vector3 direction)
        {
            if (direction == Vector3.Zero) return;

            cameraDirection = Vector3.Normalize(direction);

            if(cameraDirection == up)
            {
                // Hot Fix
                cameraDirection = Vector3.Normalize(new Vector3(1, 0, 100000));
            }

            changed = true;
        }

        public void updateView()
        {
            Matrix4x4 positionMatrix = Matrix4x4.Identity;

            positionMatrix.M14 = -cameraPosition.X;
            positionMatrix.M24 = -cameraPosition.Y;
            positionMatrix.M34 = -cameraPosition.Z;

            Vector3 rightVector = Vector3.Normalize(Vector3.Cross(up, cameraDirection));
            Vector3 upVector = Vector3.Normalize(Vector3.Cross(cameraDirection, rightVector));

            matrix = new Matrix4x4
            {
                M11 = rightVector.X,
                M12 = rightVector.Y,
                M13 = rightVector.Z,

                M21 = upVector.X,
                M22 = upVector.Y,
                M23 = upVector.Z,

                M31 = cameraDirection.X,
                M32 = cameraDirection.Y,
                M33 = cameraDirection.Z,

                M44 = 1
            };

            matrix = Matrix4x4.Multiply(matrix, positionMatrix);

            changed = false;
        }

    }
}
