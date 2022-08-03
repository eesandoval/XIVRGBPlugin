using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIVRGBPlugin
{
    public class RGBKeyboard : IRGBDevice
    {
        public List<RGBLed> RGBLeds { get => RGBLeds; set => RGBLeds = value; }

        public void SetSingleColor(int r, int g, int b)
        {
            throw new NotImplementedException();
        }

        public void SetSingleColorPulseEffect(int r, int g, int b)
        {
            throw new NotImplementedException();
        }

        public void SetSingleColorWaveEffect(int r, int g, int b)
        {
            throw new NotImplementedException();
        }
    }
}
