using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Shared;
using GTMPVoice;
using NLog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace VoiceSupport
{
    public class VoiceController : Script
    {
        static Logger logger = LogManager.GetCurrentClassLogger();
        static System.Timers.Timer teamspeakTimer;
        static ConcurrentDictionary<int, Dictionary<string, VoiceLocationInformation>> PlayerHears = new ConcurrentDictionary<int, Dictionary<string, VoiceLocationInformation>>();
        static GTMPVoice.Server.VoiceServer _voiceServer;

        public static string VoiceServerIP = "";
        public static int VoiceServerPort = 4500;
        public static string VoiceServerSecret = "";
        public static string VoiceServerGUID = "";
        public static ulong VoiceDefaultChannel = 1;
        public static ulong VoiceIngameChannel = 115;
        public static int VoiceClientPort = 4239;
        public static string VoiceIngameChannelPassword = "egal";
        public static Version VoiceServerPluginVersion = new Version(0, 0, 0, 0);
        public static bool VoiceEnableLipSync = true;

        public VoiceController() : base()
        {
            API.onResourceStart += API_onResourceStart;

        }

        private void API_onResourceStart()
        {

            VoiceDefaultChannel = API.getSetting<ulong>("voice_defaultchannel");
            VoiceIngameChannel = API.getSetting<ulong>("voice_ingamechannel");
            VoiceIngameChannelPassword = API.getSetting<string>("voice_ingamechannelpassword");
            VoiceServerGUID = API.getSetting<string>("voice_serverguid");
            VoiceServerIP = API.getSetting<string>("voice_server");
            VoiceServerPort = API.getSetting<int>("voice_port");
            VoiceServerSecret = API.getSetting<string>("voice_secret");
            VoiceClientPort = API.getSetting<int>("voice_clientport");
            Version.TryParse(API.getSetting<string>("voice_minpluginversion"), out VoiceServerPluginVersion);
            VoiceEnableLipSync = API.getSetting<bool>("voice_enablelipsync");


            _voiceServer = new GTMPVoice.Server.VoiceServer(VoiceServerPort, VoiceServerSecret, VoiceServerGUID, VoiceServerPluginVersion,
                VoiceDefaultChannel, VoiceIngameChannel, VoiceIngameChannelPassword, VoiceEnableLipSync);
            _voiceServer.VoiceClientConnected += _voiceServer_VoiceClientConnected;
            _voiceServer.VoiceClientDisconnected += _voiceServer_VoiceClientDisconnected;
            _voiceServer.VoiceClientTalking += _voiceServer_VoiceClientTalking;
            _voiceServer.VoiceClientMicrophoneStatusChanged += _voiceServer_VoiceClientMicrophoneStatusChanged;
            _voiceServer.VoiceClientSpeakersStatusChanged += _voiceServer_VoiceClientSpeakersStatusChanged;

            teamspeakTimer = API.delay(200, false, () => UpdateTeamspeak());

            API.onPlayerDeath += API_onPlayerDeath;
            API.onPlayerFinishedDownload += API_onPlayerFinishedDownload;
            API.onPlayerDisconnected += API_onPlayerDisconnected;

        }

        private void API_onPlayerFinishedDownload(Client player)
        {
            PlayerHears[player.handle.Value] = new Dictionary<string, VoiceLocationInformation>();
            Connect(player);
        }

        private void API_onPlayerDisconnected(Client player, string reason)
        {
            PlayerHears[player.handle.Value] = new Dictionary<string, VoiceLocationInformation>();
            _voiceServer.SendCommand(player.getData("VOICE_ID"), "DISCONNECT", "");
        }

        private void API_onPlayerDeath(Client player, NetHandle entityKiller, int weapon)
        {
            Dictionary<string, VoiceLocationInformation> notused = null;
            PlayerHears.TryRemove(player.handle.Value, out notused);
            _voiceServer.MutePlayer(player.getData("VOICE_ID"), "_ALL_");
        }

        // LipSync
        private void _voiceServer_VoiceClientTalking(long connectionId, bool isTalking)
        {
            var p = GetPlayerByConnectionId(connectionId);
            if (p != null)
            {
                var pPos = p.position;
                //logger.Debug($"Talking {p.GetCharacterName()} {isTalking}");
                var pls = API.shared.getAllPlayers().ToList().Where(c => c.position.DistanceTo2D(pPos) < 20).ToList();
                if (isTalking)
                    pls.ForEach(pt => pt.triggerEvent("LIPSYNC", p, "mp_facial", "mic_chatter", true));
                else
                    pls.ForEach(pt => pt.triggerEvent("LIPSYNC", p, "facials@gen_male@variations@normal", "mood_normal_1", true));
            }
        }

        private Client GetPlayerByConnectionId(long connectionId)
        {
            return API.getAllPlayers().ToList().FirstOrDefault(p => p.getData("VOICE_ID") == connectionId);
        }

        private string GetTeamspeakID(Client streamedPlayer)
        {
            return streamedPlayer.getData("PLAYER_TEAMSPEAK_IDENT");
        }

        private ushort GetTeamspeakClientID(Client streamedPlayer)
        {
            return streamedPlayer.getData("VOICE_TS_ID");
        }

        private void _voiceServer_VoiceClientDisconnected(long connectionId)
        {

        }

        private void _voiceServer_VoiceClientSpeakersStatusChanged(long connectionId, bool isMuted)
        {
            var p = GetPlayerByConnectionId(connectionId);
            if (p != null)
            {
                logger.Debug("{0} Speakers muted {1}",p.name,isMuted);
            }
        }

        private void _voiceServer_VoiceClientMicrophoneStatusChanged(long connectionId, bool isMuted)
        {
            var p = GetPlayerByConnectionId(connectionId);
            if (p != null)
            {
                logger.Debug("{0} Mic muted {1}", p.name, isMuted);
            }
        }

        private void _voiceServer_VoiceClientConnected(string clientGUID, string teamspeakID, ushort teamspeakClientID, long connectionID, string clientName, bool micMuted, bool speakersMuted)
        {
            var p = API.getAllPlayers().ToList().FirstOrDefault(c => c.socialClubName == clientGUID);
            if (p != null)
            {
                logger.Debug("VoiceConnect {0} {1} {2} {3}", p.socialClubName, teamspeakID, teamspeakClientID, connectionID);
                _voiceServer.ConfigureClient(connectionID, p.name, false);
                p.setData("VOICE_ID", connectionID);
                p.setData("VOICE_TS_ID", teamspeakClientID);
                p.setData("PLAYER_TEAMSPEAK_IDENT", teamspeakID);
            }
        }


        /* Teamspeak Handling Functions */
        public static void Connect(Client player)
        {
            player.triggerEvent("GTMPVOICE", VoiceServerIP, VoiceServerPort, VoiceServerSecret, player.socialClubName, VoiceServerPluginVersion.ToString(), VoiceClientPort);
        }

        public void UpdateTeamspeak()
        {
            var players = API.getAllPlayers().ToList();
            players.ForEach(p => { try { UpdateTeamspeakForUser(p, players); } catch (Exception ex) { logger.Error(ex); } });
        }


        public void UpdateTeamspeakForUser(Client player, List<Client> allPlayers)
        {
            var playerPos = player.position;
            var playerRot = player.rotation;
            var rotation = Math.PI / 180 * (playerRot.Z * -1);
            var playerVehicle = player.vehicle;
            var cId = player.handle.Value;

            if (!player.hasData("VOICE_ID"))
                return;
            var targetId = player.getData("VOICE_ID");

            var playersIHear = new Dictionary<string, VoiceLocationInformation>();

            // Players near me
            var inRangePlayers = allPlayers.Where(cl => (cl != player) && (cl.position.DistanceTo2D(playerPos) <= 50) && (cl.dimension == player.dimension)).ToList();

            if (inRangePlayers != null)
            {
                foreach (var streamedPlayer in inRangePlayers)
                {
                    var n = GetTeamspeakID(streamedPlayer);
                    if (streamedPlayer == player)
                    {
                        continue;
                    }

                    var streamedPlayerPos = streamedPlayer.position;
                    var distance = playerPos.DistanceTo(streamedPlayerPos);
                    var range = 5; // VoiceRange of streamed player in meters
                    var volumeModifier = 0f;

                    if (distance <= range)
                    {
                        if (Math.Abs(streamedPlayerPos.Z - playerPos.Z) > range)
                        {
                            continue;
                        }

                        var subPos = streamedPlayerPos.Subtract(playerPos);

                        var x = subPos.X * Math.Cos(rotation) - subPos.Y * Math.Sin(rotation);
                        var y = subPos.X * Math.Sin(rotation) + subPos.Y * Math.Cos(rotation);

                        if (distance > 2)
                        {
                            volumeModifier = (float)(distance / range) * -10f;
                        }
                        if (volumeModifier > 0)
                        {
                            volumeModifier = 0;
                        }
                        volumeModifier = Math.Min(10.0f, Math.Max(-10.0f, volumeModifier));
                        if (!playersIHear.ContainsKey(n))
                            playersIHear[n] = new VoiceLocationInformation(n, GetTeamspeakClientID(streamedPlayer));
                        playersIHear[n].Update(new TSVector((float)(Math.Round(x * 1000) / 1000), (float)(Math.Round(y * 1000) / 1000), 0), (float)(Math.Round(volumeModifier * 1000) / 1000), false);
                    }
                }
            }


            // Players in same vehicle are always near
            if (player.isInVehicle)
            {
                var others = player.vehicle.occupants.ToList();
                if (others != null)
                {
                    others.Remove(player);
                    others.ForEach(p =>
                    {

                        var tsId = GetTeamspeakID(p);
                        if (!playersIHear.ContainsKey(tsId))
                            playersIHear[tsId] = new VoiceLocationInformation(tsId, GetTeamspeakClientID(p));
                        playersIHear[tsId].Update(new TSVector(0, 0, 0), 4, true);
                    });
                }
            }

            PlayerHears[player.handle.Value] = playersIHear;

            _voiceServer.SendUpdate(targetId, playersIHear.Count > 0 ? playersIHear.Values.ToList() : new List<VoiceLocationInformation>() );
        }


    }
}
