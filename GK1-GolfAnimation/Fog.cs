using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GK1_GolfAnimation
{
    class Fog {    
        public Color fogColor;
        public float density;

        public Fog(Color fogColor, float density){
            this.fogColor = fogColor;
            this.density = density;
        }

        private float CalculateAlpha(float dist)
        {
            float factor = Math.Abs(dist) * density;

            return 1.0f / MathF.Exp(factor * factor);
        }

        public int applyFogChannel(float alpha, int fogChannel, int colorChannel)
        {
            float alphaP = 1 - alpha;

            return Math.Max(Math.Min((int)(fogChannel * alphaP + colorChannel * alpha), 255), 0);
        }

        public Color applyFog(float dist, Color c)
        {
            float alpha = CalculateAlpha(dist);

            int R = applyFogChannel(alpha, fogColor.R, c.R);
            int G = applyFogChannel(alpha, fogColor.G, c.G);
            int B = applyFogChannel(alpha, fogColor.B, c.B);

            return Color.FromArgb(R, G, B);

        }
        
    }
}
