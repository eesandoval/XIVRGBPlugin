using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIVRGBPlugin
{
    public interface IRGBDevice
    {
        List<RGBLed> RGBLeds
        {
            get;
            set;
        }

        abstract void SetSingleColor(int r, int g, int b);

        abstract void SetSingleColorPulseEffect(int r, int g, int b);

        abstract void SetSingleColorWaveEffect(int r, int g, int b);
    }
}
