using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace XIVRGBPlugin
{
    public static class CorsairWrapper
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct CorsairProtocolDetails
        {
            public IntPtr skdVersion;
            public IntPtr serverVersion;
            public IntPtr sdkProtocolVersion;
            public IntPtr serverProtocolVersion;
            public IntPtr breakingChanges;
        }


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct CorsairLedPosition
        {
            public int ledId;
            public double top;
            public double left;
            public double height;
            public double width;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct CorsairLedPositions
        {
            public int numberOfLed;
            public IntPtr pLedPosition;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CorsairDeviceInfo
        {
            public int type;
            public IntPtr model;
            public int physicalLayout;
            public int logicalLayout;
            public int capsMask;
            public int ledsCount;
            public CorsairChannelsInfo channels;
            public int deviceId;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CorsairChannelsInfo
        {
            int channelsCount;
            IntPtr channels;
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct CorsairLedColor
        {
            public int ledId;
            public int r;
            public int g;
            public int b;
        }


        [DllImport("CUESDK.x64_2019.dll")]
        public static extern int CorsairGetDeviceCount();

        [DllImport("CUESDK.x64_2019.dll")]
        public static extern CorsairProtocolDetails CorsairPerformProtocolHandshake();


        [DllImport("CUESDK.x64_2019.dll")]
        public static extern IntPtr CorsairGetLedPositionsByDeviceIndex(int deviceIndex);

        [DllImport("CUESDK.x64_2019.dll")]
        public static extern IntPtr CorsairGetDeviceInfo(int deviceIndex);

        public static Dictionary<int, List<CorsairLedColor>> GetAvailableKeys()
        {
            Dictionary<int, List<CorsairLedColor>> result = new Dictionary<int, List<CorsairLedColor>>();
            for (var deviceIndex = 0; deviceIndex < CorsairGetDeviceCount(); deviceIndex++)
            {
                var ledPositionsPtr = CorsairGetLedPositionsByDeviceIndex(deviceIndex);
                var ledPositions = (CorsairLedPositions)Marshal.PtrToStructure(ledPositionsPtr, typeof(CorsairLedPositions));
                var ledColorsVector = new List<CorsairLedColor>();
                for (var i = 0; i < ledPositions.numberOfLed; i++)
                {
                    var ledIdPtr = ledPositions.pLedPosition + Marshal.SizeOf(typeof(CorsairLedPosition));
                    var ledId = ((CorsairLedPosition)Marshal.PtrToStructure(ledIdPtr, typeof(CorsairLedPosition))).ledId;
                    ledColorsVector.Add(new CorsairLedColor() { ledId = ledId, r = 0, g = 0, b = 0 });
                }
                result.Add(deviceIndex, ledColorsVector);
            }
            return result;
        }

    }
}
