﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpStar.Lib.Packets;
using SharpStar.Lib.Plugins;
using SharpStar.Lib.Server;

namespace EssentialCommandsPlugin.Commands
{
    public class MotdCommand
    {

        [Command("setmotd")]
        public void SetMotd(StarboundClient client, string[] args)
        {

            if (!EssentialCommands.IsAdmin(client) && !client.Server.Player.HasPermission("setmotd"))
            {

                client.SendChatMessage("Server", "You do not have permission to use this command!");

                return;

            }

            string motd = string.Join(" ", args);

            EssentialCommands.Config.ConfigFile.Motd = motd;
            EssentialCommands.Config.Save();

            client.SendChatMessage("Server", "MOTD Set!");

        }

        [Event("afterConnectionResponse")]
        public void AfterConnectionResponse(IPacket packet, StarboundClient client)
        {

            if (!string.IsNullOrEmpty(EssentialCommands.Config.ConfigFile.Motd)) //if motd has been set
            {
                client.SendChatMessage("MOTD", EssentialCommands.Config.ConfigFile.Motd);
            }

        }

    }
}