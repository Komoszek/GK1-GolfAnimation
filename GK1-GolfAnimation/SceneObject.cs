using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;

namespace GK1_GolfAnimation
{
    class SceneObject
    {
        public Model3D model;
        private Vector3 position;
        public Color[] trianglesColors;
        private Vector3 rotation;
        private Vector3 scale;

        private Matrix4x4 modelMatrix;
        private Matrix4x4 normalMatrix;

        private bool updated;

        public SceneObject(Model3D model, Vector3 position, Color[] tColors, Vector3 rotation, Vector3 scale)
        {
            this.model = model;
            this.position = position;
            trianglesColors = tColors;
            this.rotation = rotation;
            this.scale = scale;
            updated = true;
        }

        public Vector3 Position {
            get => position;
            set
            {
                if(value != position)
                {
                    updated = true;
                    position = value;
                }
            }
        }

        public Vector3 Rotation
        {
            get => rotation;
            set
            {
                if (value != rotation)
                {
                    updated = true;
                    rotation = value;
                }
            }
        }

        public Vector3 Scale
        {
            get => scale;
            set
            {
                if (value != scale)
                {
                    updated = true;
                    scale = value;
                }
            }
        }

        public Matrix4x4 ModelMatrix
        {
            get
            {
                if (updated)
                {
                    updateMatrix();
                }

                return modelMatrix;
            }
        }

        public Matrix4x4 NormalMatrix
        {
            get
            {
                if (updated)
                {
                    updateMatrix();
                }

                return normalMatrix;
            }
        }

        private void updateNormalMatrix()
        {
            Matrix4x4 newMatrix;
            Matrix4x4.Invert(modelMatrix, out newMatrix);

            normalMatrix = Matrix4x4.Transpose(newMatrix);
        }

        private void updateMatrix()
        {
            //scale
            Matrix4x4 scaleMatrix = new Matrix4x4();
            scaleMatrix.M11 = scale.X;
            scaleMatrix.M22 = scale.Y;
            scaleMatrix.M33 = scale.Z;
            scaleMatrix.M44 = 1;


            //rotate
            Matrix4x4 rotateMatrix = Matrix4x4.CreateFromYawPitchRoll(rotation.Y, rotation.X, rotation.Z);

            //move
            Matrix4x4 moveMatrix = Matrix4x4.Identity;

            moveMatrix.M14 = position.X;
            moveMatrix.M24 = position.Y;
            moveMatrix.M34 = position.Z;


            modelMatrix = Matrix4x4.Multiply(moveMatrix, Matrix4x4.Multiply(rotateMatrix, scaleMatrix));

            updateNormalMatrix();
            updated = false;
        }

    }
}
