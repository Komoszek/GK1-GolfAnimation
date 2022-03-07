using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;

namespace GK1_GolfAnimation.Lights
{
    interface Light
    {
        public Vector3 GetLightDirection(Vector3 observer);
        public Color GetLightColor(Vector3 observer);

    }
}
