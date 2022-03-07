using GK1_GolfAnimation.Lights;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GK1_GolfAnimation
{
    static class IlluminationModel
    {
        public static Color calculateColor(Vector3 P, float ka, Color objectColor, Color Ia, List<Light> lightSources, float kd, Vector3 N, float ks, int n, Vector3 V)
        {
            float objectColorR = objectColor.R / 255.0f;
            float objectColorG = objectColor.G / 255.0f;
            float objectColorB = objectColor.B / 255.0f;

            float R = ka * Ia.R / 255 * objectColorR;
            float G = ka * Ia.G / 255 * objectColorG;
            float B = ka * Ia.B / 255 * objectColorB;

            foreach (Light light in lightSources)
            {
                Vector3 L = light.GetLightDirection(P);
                Color lightColor = light.GetLightColor(P);

                float dotNL = Math.Max(Vector3.Dot(N, L), 0);
                Vector3 vecR = Vector3.Normalize(2 * dotNL * N - L);

                float dotVRN = MathF.Pow(Math.Min(Math.Max(Vector3.Dot(V, vecR), 0), 1), n);
                dotNL = Math.Max(dotNL, 0);
                float coefDiffuse = kd * dotNL;
                float coefspecular = ks * dotVRN;


                R += lightColor.R * (coefDiffuse * objectColorR + coefspecular) / 255.0f;
                G += lightColor.G * (coefDiffuse * objectColorG + coefspecular) / 255.0f;
                B += lightColor.B * (coefDiffuse * objectColorB + coefspecular) / 255.0f;
            }

            R = Math.Min(Math.Max((int)(R * 255), 0), 255);
            G = Math.Min(Math.Max((int)(G * 255), 0), 255);
            B = Math.Min(Math.Max((int)(B * 255), 0), 255);

            return Color.FromArgb((int)R, (int)G, (int)B);
        }
    }
}
