using ImGuiNET;
using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace XIVRGBPlugin
{
    // It is good to have this be disposable in general, in case you ever need it
    // to do any cleanup
    class PluginUI : IDisposable
    {
        private Configuration configuration;

        private ImGuiScene.TextureWrap goatImage;

        // this extra bool exists for ImGui, since you can't ref a property
        private bool visible = false;
        public bool Visible
        {
            get { return this.visible; }
            set { this.visible = value; }
        }

        private bool settingsVisible = false;
        public bool SettingsVisible
        {
            get { return this.settingsVisible; }
            set { this.settingsVisible = value; }
        }

        // passing in the image here just for simplicity
        public PluginUI(Configuration configuration, ImGuiScene.TextureWrap goatImage)
        {
            this.configuration = configuration;
            this.goatImage = goatImage;
        }

        public void Dispose()
        {
            this.goatImage.Dispose();
        }

        public void Draw()
        {
            // This is our only draw handler attached to UIBuilder, so it needs to be
            // able to draw any windows we might have open.
            // Each method checks its own visibility/state to ensure it only draws when
            // it actually makes sense.
            // There are other ways to do this, but it is generally best to keep the number of
            // draw delegates as low as possible.

            DrawSettingsWindow();
        }

        public void DrawSettingsWindow()
        {
            if (!SettingsVisible)
            {
                return;
            }

            // Window size
            ImGui.SetNextWindowSize(new Vector2(375, 375), ImGuiCond.FirstUseEver);

            // Set the title, set the GUI flags
            if (ImGui.Begin("RGB Settings Window", ref this.settingsVisible))
            {
                if (ImGui.CollapsingHeader("Protocols"))
                {
                    // Get the SDK flags and add checkboxes to modify them, save immediately upon change
                    var enableCorsairSDKValue = this.configuration.EnableCorsairSDK;
                    if (enableCorsairSDKValue)
                        CorsairWrapper.CorsairPerformProtocolHandshake();
                    var checkBoxString = "Enable Corsair SDK" + (enableCorsairSDKValue ? $" ({CorsairWrapper.CorsairGetDeviceCount()} devices found)" : "");
                    if (ImGui.Checkbox(checkBoxString, ref enableCorsairSDKValue))
                    {
                        this.configuration.EnableCorsairSDK = enableCorsairSDKValue;
                        this.configuration.Save();
                    }
                }
                bool itDoesntDoAnythingYet = true;
                // Now display device information
                if (ImGui.CollapsingHeader("Devices"))
                {
                    ImGui.Columns(4, "Device Options", true);
                    ImGui.TextColored(new Vector4(0, 1, 1, 1), "Protocol");
                    ImGui.NextColumn();
                    ImGui.TextColored(new Vector4(0, 1, 1, 1), "Device");
                    ImGui.NextColumn();
                    ImGui.TextColored(new Vector4(0, 1, 1, 1), "LEDs");
                    ImGui.NextColumn();
                    ImGui.TextColored(new Vector4(0, 1, 1, 1), "Enabled");
                    ImGui.NextColumn();
                    if (this.configuration.EnableCorsairSDK)
                    {
                        CorsairWrapper.CorsairPerformProtocolHandshake();
                        int deviceCount = CorsairWrapper.CorsairGetDeviceCount();
                        for (int deviceIndex = 0, size = CorsairWrapper.CorsairGetDeviceCount(); deviceIndex < size; deviceIndex++)
                        {
                            IntPtr deviceInfoPtr = CorsairWrapper.CorsairGetDeviceInfo(deviceIndex);
                            if (deviceInfoPtr == IntPtr.Zero)
                                continue; // Null ptr; no struct was returned
                            var deviceInfoObject = Marshal.PtrToStructure(deviceInfoPtr, typeof(CorsairWrapper.CorsairDeviceInfo));
                            if (deviceInfoObject == null)
                                continue; // Something went wrong in the conversion
                            var deviceInfo = (CorsairWrapper.CorsairDeviceInfo)deviceInfoObject;
                            var deviceName = Marshal.PtrToStringAnsi(deviceInfo.model);
                            if (deviceName != null)
                            {
                                ImGui.Text("Corsair");
                                ImGui.NextColumn();
                                ImGui.Text($"{deviceName}");
                                ImGui.NextColumn();
                                ImGui.Text($"{deviceInfo.ledsCount}");
                                ImGui.NextColumn();
                                if (ImGui.Checkbox("##thischeckboxdoesntdoanything", ref itDoesntDoAnythingYet))
                                {
                                    // TODO
                                }
                                ImGui.NextColumn();
                            }

                        }
                    }
                }
            }

            // Close the window on exit
            ImGui.End();
        }
    }
}