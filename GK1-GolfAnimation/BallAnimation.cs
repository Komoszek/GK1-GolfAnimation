using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GK1_GolfAnimation.Models;
using System.Numerics;
using System.Drawing;
using System.Diagnostics;
using GK1_GolfAnimation.Lights;

namespace GK1_GolfAnimation
{
    enum WallSide
    {
        xPos = 0,
        yPos = 1,
        xNeg = 2,
        yNeg = 3
    }

    class BallAnimation
    {
        public int ballAnimationIdx;
        public Vector3 ballStartPosition;
        private float pedestalH;
        private Vector3 pedestalPoint;
        private WallSide wallSide;
        private Vector3 fieldPos;
        private Vector3 hillBottomPoint;
        private Random random;

        private SpotLight light;

        private float swingH;

        private int wallOffset;

        readonly float pedestalTan;
        readonly float ballRadius;
        readonly float pedestalDist;
        readonly float fieldDist;
        readonly float animationTime;
        readonly float randomPartTime;
        readonly float pedestalFlightPartTime;
        readonly float hillPartTime;
        readonly float ballPedestalOffset;
        readonly float ballPedestalZOffset;
        readonly float bottomPedestalPos;



        public BallAnimation(int seed, Vector3 ballPosition, float ballRadius, float pedestalH, float pedestalEdge, float pedestalTan, float fieldDist,
                                float randomPartTime, float pedestalFlightPartTime, float hillPartTime, SpotLight light)
        {
            random = new Random(seed);
            ballAnimationIdx = -1;
            ballStartPosition = new Vector3(ballPosition.X, ballPosition.Y, ballPosition.Z);
            fieldPos = ballStartPosition;
            this.ballRadius = ballRadius;

            this.pedestalTan = pedestalTan;
            this.pedestalDist = (pedestalEdge / 2) - pedestalTan * pedestalH;
            this.bottomPedestalPos = pedestalEdge / 2 + pedestalTan * (ballRadius - ballPedestalZOffset*2);

            this.pedestalH = pedestalH;
            this.fieldDist = fieldDist;

            this.randomPartTime = randomPartTime;
            this.pedestalFlightPartTime = pedestalFlightPartTime;
            this.hillPartTime = hillPartTime;

            float tanSqr = this.pedestalTan * this.pedestalTan;
            float sinSqr = tanSqr / (1 + tanSqr);
            float cosSqr = tanSqr / (1 + tanSqr);

            this.ballPedestalOffset = MathF.Sqrt(cosSqr) * ballRadius;
            this.ballPedestalZOffset = MathF.Sqrt(sinSqr) * ballRadius;

            this.animationTime = randomPartTime + pedestalFlightPartTime + hillPartTime;
            wallSide = WallSide.xPos;

            this.light = light;
        }

        private void pedestalFlightUpdate(float timeframe, SceneObject ball)
        {
            float timeFactor = timeframe / pedestalFlightPartTime;

            Vector3 newPos = ballStartPosition * (1 - timeFactor) + pedestalPoint * timeFactor;

            if (timeFactor < 0.5)
            {
                float timeFactorSqrt = MathF.Sqrt(timeFactor * 2);

                newPos.Z = ballStartPosition.Z * (1 - timeFactorSqrt) + swingH * timeFactorSqrt;
            }
            else
            {
                float adjtimeFactor = (timeFactor - 0.5f) * 2;

                float timeFactorSqr = adjtimeFactor * adjtimeFactor;

                newPos.Z = swingH * (1 - timeFactorSqr) + (pedestalH + ballPedestalZOffset) * timeFactorSqr;
            }

            ball.Position = newPos;

            if (wallOffset != 0)
            {
                ball.Rotation = new Vector3(0, 0, wallOffset * MathF.Tau * timeFactor * 2);
            }
        }

        private void updateHillPart(float timeframe, SceneObject ball)
        {
            float timeFactor = timeframe / hillPartTime;

            float split = 0.3f;
            if (timeFactor < split) // hill slide
            {
                float adjtimeFactor = timeFactor / split;
                adjtimeFactor *= adjtimeFactor;
                ball.Position = pedestalPoint * (1 - adjtimeFactor) + hillBottomPoint * adjtimeFactor;
            }
            else
            {
                float adjtimeFactor = MathF.Pow((timeFactor - split) / (1 - split), 0.8f);
                ball.Position = hillBottomPoint * (1 - adjtimeFactor) + fieldPos * adjtimeFactor;
            }

            float rot = MathF.Tau * timeFactor;


            switch (wallSide)
            {
                case WallSide.xPos:
                    ball.Rotation = new Vector3(0, -rot, 0);
                    break;
                case WallSide.yPos:
                    ball.Rotation = new Vector3(rot, 0, 0);
                    break;
                case WallSide.xNeg:
                    ball.Rotation = new Vector3(0, rot, 0);
                    break;
                case WallSide.yNeg:
                    ball.Rotation = new Vector3(-rot, 0, 0);
                    break;
            }
        }

        private void setupNextAnimation(int currBallAnimationIdx)
        {
            ballAnimationIdx = currBallAnimationIdx;
            wallOffset = random.Next(-1, 1);
            wallSide = (WallSide)((4 + (int)wallSide + wallOffset) % 4);

            swingH = (float)(random.NextDouble() * 5 + 2);

            float pedestalPosition = (float)random.NextDouble() * pedestalDist * 2 - pedestalDist;
            ballStartPosition = fieldPos;

            switch (wallSide)
            {
                case WallSide.xPos:
                    pedestalPoint = new Vector3(pedestalDist + ballPedestalOffset, pedestalPosition, pedestalH + ballPedestalZOffset);
                    hillBottomPoint = new Vector3(bottomPedestalPos, pedestalPosition, ballRadius);
                    fieldPos = new Vector3(fieldDist, pedestalPosition, ballRadius);
                    break;
                case WallSide.yPos:
                    pedestalPoint = new Vector3(pedestalPosition, pedestalDist + ballPedestalOffset, pedestalH + ballPedestalZOffset);
                    hillBottomPoint = new Vector3(pedestalPosition, bottomPedestalPos, ballRadius);
                    fieldPos = new Vector3(pedestalPosition, fieldDist, ballRadius);
                    break;
                case WallSide.xNeg:
                    pedestalPoint = new Vector3(-(pedestalDist + ballPedestalOffset), pedestalPosition, pedestalH + ballPedestalZOffset);
                    hillBottomPoint = new Vector3(-bottomPedestalPos, pedestalPosition, ballRadius);

                    fieldPos = new Vector3(-fieldDist, pedestalPosition, ballRadius);
                    break;
                case WallSide.yNeg:
                    pedestalPoint = new Vector3(pedestalPosition, -(pedestalDist + ballPedestalOffset), pedestalH + ballPedestalZOffset);
                    hillBottomPoint = new Vector3(pedestalPosition, -bottomPedestalPos, ballRadius);
                    fieldPos = new Vector3(pedestalPosition, -fieldDist, ballRadius);
                    break;
            }
        }

        public void update(SceneObject ball, float elapsedTime)
        {
            int currBallAnimationIdx = (int)(elapsedTime / animationTime);

            float timeframe = elapsedTime - currBallAnimationIdx * animationTime;

            if (currBallAnimationIdx != ballAnimationIdx)
            {
                setupNextAnimation(currBallAnimationIdx);
            }

            if(timeframe < randomPartTime)
            {
                //do nothing
            } 
            else
            {
                timeframe -= randomPartTime;

                if (timeframe < pedestalFlightPartTime)
                {
                    pedestalFlightUpdate(timeframe, ball);
                } else
                {
                    timeframe -= pedestalFlightPartTime;
                    updateHillPart(timeframe, ball);
                }
            }

            updateReflector(ball);
        }

        private void updateReflector(SceneObject ball)
        {
            light.position = ball.Position;
            Vector3 newTarget = new Vector3(light.position.X, light.position.Y, 0);
            newTarget = newTarget - Vector3.Normalize(newTarget) * 2f;
            newTarget.Z = ball.Position.Z - 0.5f;
            light.lookAt(newTarget);

            light.setColor(getReflectorColor(Vector3.Distance(new Vector3(0, 0, ball.Position.Z), ball.Position)));
        }

        private Color getReflectorColor(float dist)
        {
            float distEnd = 5.0f;

            if (dist > distEnd)
                return Color.Yellow;

            int green = 255;
            int red = (int)(255 * (1 - ((distEnd - dist) / distEnd)));

            return Color.FromArgb(red, green, 0);

        }
    }
}
