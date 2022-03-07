using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Diagnostics;
using System.Drawing.Imaging;
using GK1_GolfAnimation.Models;

namespace GK1_GolfAnimation
{
    enum CameraMode
    {
        Static = 0,
        TrackingBall = 1,
        FollowingBall = 2
    }

    enum ShadingMode
    {
        Constant = 0,
        Gouraud = 1,
        Phong = 2
    }

    public partial class Form1 : Form
    {
        readonly int NumberOfCameraModes = Enum.GetNames(typeof(CameraMode)).Length;
        readonly int NumberOfShadingModes = Enum.GetNames(typeof(ShadingMode)).Length;
        private CameraMode cameraMode;
        private ShadingMode shadingMode;

        private int fov;
        private Bitmap bitmap;
        private byte[] bitmapBytes;
        private float[,] zBuffer;
        private Scene scene;

        private Stopwatch stopwatch;
        private Timer timer;
        private int width;
        private int height;

        public Form1()
        {
            InitializeComponent();

            fov = (int)FOVnumericUpDown.Value;
            scene = new Scene();

            scene.projection.FOV = fov;
            scene.projection.Near = 1;
            scene.projection.Far = 100;
            scene.projection.Aspect = (float)((float)mainPictureBox.Height / (float)mainPictureBox.Width);

            bitmap = new Bitmap(mainPictureBox.Width, mainPictureBox.Height);
            bitmapBytes = new Byte[mainPictureBox.Width * mainPictureBox.Height * 4];
            mainPictureBox.Image = bitmap;

            zBuffer = new float[mainPictureBox.Width, mainPictureBox.Height];

            stopwatch = new Stopwatch();
            timer = new Timer
            {
                Interval = 40
            };
            timer.Tick += new EventHandler(OnTimedEvent);


            timer.Start();
            stopwatch.Start();

            cameraMode = CameraMode.Static;
            shadingMode = ShadingMode.Gouraud;

            gouraudRadioButton.Checked = true;
            staticRadioButton.Checked = true;
            width = mainPictureBox.Width;
            height = mainPictureBox.Height;

        }

        private void OnTimedEvent(Object source, EventArgs e)
        {
            if(scene != null && scene.sceneObjects.Count > 0)
            {
                float elapsedTime = (float)(stopwatch.ElapsedMilliseconds / 1000.0);

                scene.ballAnimation.update(scene.ball, elapsedTime);

                switch (cameraMode)
                {
                    case CameraMode.TrackingBall:
                        updateTrackingCamera();
                        break;
                    case CameraMode.FollowingBall:
                        updateFollowingCamera();
                        break;

                }
                mainPictureBox.Refresh();

            }
        }

        private void setCameraToStatic()
        {
            scene.view.moveTo(new Vector3(10, 20, 15));
            scene.view.lookAt(new Vector3(0, 0, 0));
            scene.fog.density = 0.02f;
        }

        private void updateTrackingCamera()
        {
            scene.view.lookAt(scene.ball.Position);

        }

        private void setCameraToTracking()
        {
            scene.view.moveTo(new Vector3(-10, 10, 10));
            scene.fog.density = 0.02f;

            updateTrackingCamera();
        }

        private void updateFollowingCamera()
        {
            Vector3 cameraDir = Vector3.Normalize(new Vector3(scene.ball.Position.X, scene.ball.Position.Y, 0));
            Vector3 cameraPos = scene.ball.Position + cameraDir * 3.5f + Vector3.UnitZ * 0.5f;
            Vector3 dir = new Vector3(0, 0, cameraPos.Z);


            scene.view.moveTo(cameraPos);
            scene.view.lookAt(dir);
        }

        private void setCameraToFollowing()
        {
            updateFollowingCamera();

            scene.fog.density = 0.15f;

        }

        private void updateFov(int newFov)
        {
            if(newFov != fov)
            {
                fov = newFov;
                scene.projection.FOV = fov;
                mainPictureBox.Refresh();
            }
        }

        private void FOVnumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            fovTrackBar.Value = (int)FOVnumericUpDown.Value;

            updateFov(fovTrackBar.Value);
        }

        private void fovTrackBar_Scroll(object sender, EventArgs e)
        {
            FOVnumericUpDown.Value = fovTrackBar.Value;
            updateFov(fovTrackBar.Value);

        }

        private void mainPictureBox_Paint(object sender, PaintEventArgs e)
        {
            Painter.clearSceneHelpers(bitmapBytes, scene.fog.fogColor, zBuffer);
            Painter.drawScene(scene, width, height, bitmapBytes, zBuffer, 0.4f, 0.8f, 0.8f, 20, shadingMode);
            Painter.copyBitmapBytes(bitmap, bitmapBytes);
        }

        private void mainPictureBox_SizeChanged(object sender, EventArgs e)
        {
            bitmap = new Bitmap(mainPictureBox.Width, mainPictureBox.Height);
            bitmapBytes = new Byte[mainPictureBox.Width * mainPictureBox.Height * 4];

            mainPictureBox.Image = bitmap;
            zBuffer = new float[mainPictureBox.Width, mainPictureBox.Height];
            width = mainPictureBox.Width;
            height = mainPictureBox.Height;
            if (scene != null)
            {
                scene.projection.Aspect = (float)((float)height / (float)width);
            }

            mainPictureBox.Refresh();
        }

        private void refreshShadingMode()
        {
            switch (shadingMode)
            {
                case ShadingMode.Constant:
                default:
                    constantRadioButton.Checked = true;
                    break;
                case ShadingMode.Gouraud:
                    gouraudRadioButton.Checked = true;
                    break;
                case ShadingMode.Phong:
                    phongRadioButton.Checked = true;
                    break;
            }
        }

        private void refreshCameraMode()
        {
            switch (cameraMode)
            {
                case CameraMode.Static:
                default:
                    staticRadioButton.Checked = true;
                    setCameraToStatic();
                    break;
                case CameraMode.TrackingBall:
                    trackingBallRadioButton.Checked = true;
                    setCameraToTracking();
                    break;
                case CameraMode.FollowingBall:
                    followingBallRadioButton.Checked = true;
                    setCameraToFollowing();
                    break;

            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C)
            {
                cameraMode = (CameraMode)((int)(cameraMode + 1) % NumberOfCameraModes);
                refreshCameraMode();
            }

            if (e.KeyCode == Keys.S)
            {
                shadingMode = (ShadingMode)((int)(shadingMode + 1) % NumberOfShadingModes);
                refreshShadingMode();
            }
        }

        private void staticRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (staticRadioButton.Checked)
            {
                cameraMode = CameraMode.Static;
                refreshCameraMode();
            }
        }

        private void trackingBallRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (trackingBallRadioButton.Checked)
            {
                cameraMode = CameraMode.TrackingBall;
                refreshCameraMode();
            }
        }

        private void followingBallRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (followingBallRadioButton.Checked)
            {
                cameraMode = CameraMode.FollowingBall;
                refreshCameraMode();
            }
        }

        private void constantRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (constantRadioButton.Checked)
            {
                shadingMode = ShadingMode.Constant;
            }
        }

        private void gouraudRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (gouraudRadioButton.Checked)
            {
                shadingMode = ShadingMode.Gouraud;
            }
        }

        private void phongRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (phongRadioButton.Checked)
            {
                shadingMode = ShadingMode.Phong;
            }
        }
    }
}
