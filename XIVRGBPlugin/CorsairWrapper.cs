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
            public IntPtr ledId;
            public IntPtr top;
            public IntPtr left;
            public IntPtr height;
            public IntPtr width;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct CorsairLedPositions
        {
            public IntPtr numberOfLed;
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


        [DllImport("CUESDK.x64_2019.dll")]
        public static extern int CorsairGetDeviceCount();

        [DllImport("CUESDK.x64_2019.dll")]
        public static extern CorsairProtocolDetails CorsairPerformProtocolHandshake();


        [DllImport("CUESDK.x64_2019.dll")]
        public static extern CorsairLedPositions CorsairGetLedPositionsByDeviceIndex(int deviceIndex);

        [DllImport("CUESDK.x64_2019.dll")]
        public static extern IntPtr CorsairGetDeviceInfo(int deviceIndex);

    }
}
