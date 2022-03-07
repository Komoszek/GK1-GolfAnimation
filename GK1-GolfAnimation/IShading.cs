using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GK1_GolfAnimation.Lights;
using System.Numerics;
using System.Diagnostics;

namespace GK1_GolfAnimation
{
    public interface IShading
    {
        public Color ComputeColor(int x, int y);
    }
}
