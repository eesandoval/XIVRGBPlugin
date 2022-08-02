using Dalamud.Game.Text;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIVRGBPlugin
{
    public partial class Plugin : IDalamudPlugin
    {
        private void ChatMessage(XivChatType type, uint senderId, ref SeString sender, ref SeString message, ref bool isHandled)
        {
        }

        private void TerritoryChanged(object? sender, ushort e)
        {
            if (DalamudSvcHandler.ClientState.LocalPlayer == null)
            {
                return;
            }
            else
            {
                switch (e)
                {
                    case 979:
                        // Empyreum
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
