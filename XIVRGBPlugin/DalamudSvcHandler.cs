using Dalamud.Data;
using Dalamud.Game.ClientState;
using Dalamud.Game.Gui;
using Dalamud.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIVRGBPlugin
{
    public class DalamudSvcHandler
    {
        [PluginService] static internal ChatGui ChatGui { get; private set; }
        [PluginService] static internal ClientState ClientState { get; private set; }
        [PluginService] public static DataManager DataManager { get; private set; }

    }
}
