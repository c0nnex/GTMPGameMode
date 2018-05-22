using GTANetworkInternals;
using GTANetworkAPI;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Concurrent;
using GTMPGameMode.Server.Base;
using GTMPVoice;

namespace GTMPGameMode.Server.VoiceSupport
{
    public class TeamspeakController : GameModeScript
    {
        static System.Timers.Timer teamspeakTimer;
        static ConcurrentDictionary<int, Dictionary<string, VoiceLocationInformation>> PlayerHears = new ConcurrentDictionary<int, Dictionary<string, VoiceLocationInformation>>();
        static HashSet<int> usedIds = new HashSet<int>();
        static GTMPVoice.Server.VoiceServer _voiceServer;

        public TeamspeakController() : base()
        {
            logger = LogManager.GetLogger("TeamspeakController");
            GameMode.OnWorldReady += World_OnWorldStartup;
            GameMode.OnWorldShutdown += World_OnWorldShutdown;

        }
        public override void OnScriptStart()
        {
            GTAAPI.onPlayerDeath += API_onPlayerDeath;
            GTAAPI.onPlayerFinishedDownload += API_onPlayerFinishedDownload;
            GTAAPI.onPlayerDisconnected += API_onPlayerDisconnected;
        }

        private void API_onPlayerFinishedDownload(Client player)
        {
            PlayerHears[player.GetCharacterId()] = new Dictionary<string, VoiceLocationInformation>();
            GenerateTeamspeakName(player);

            Connect(player);
        }

        private void API_onPlayerDisconnected(Client player, string reason)
        {
            PlayerHears[player.GetCharacterId()] = new Dictionary<string, VoiceLocationInformation>();
            _voiceServer.SendCommand(player.GetData("VOICE_ID", 0L), "DISCONNECT", "");
        }

        private void API_onPlayerDeath(Client player, NetHandle entityKiller, int weapon)
        {
            PlayerHears.TryRemove(player.GetCharacterId(), out var notused);
            _voiceServer.MutePlayer(player.GetData("VOICE_ID", 0L), "_ALL_");
        }

        private void World_OnWorldShutdown()
        {
        }

        private void World_OnWorldStartup()
        {
            _voiceServer = new GTMPVoice.Server.VoiceServer(GameMode.VoiceServerPort, GameMode.VoiceServerSecret, GameMode.VoiceServerGUID, GameMode.VoiceServerPluginVersion,
                GameMode.VoiceDefaultChannel, GameMode.VoiceIngameChannel, GameMode.VoiceIngameChannelPassword, GameMode.VoiceEnableLipSync);
            _voiceServer.VoiceClientConnected += _voiceServer_VoiceClientConnected;
            _voiceServer.VoiceClientDisconnected += _voiceServer_VoiceClientDisconnected;
            _voiceServer.VoiceClientOutdated += _voiceServer_VoiceClientOutdated;
            _voiceServer.VoiceClientTalking += _voiceServer_VoiceClientTalking;
            _voiceServer.VoiceClientMicrophoneStatusChanged += _voiceServer_VoiceClientMicrophoneStatusChanged;
            _voiceServer.VoiceClientSpeakersStatusChanged += _voiceServer_VoiceClientSpeakersStatusChanged;
            teamspeakTimer = API.Delay(200, false, () => UpdateTeamspeak());

        }

        // LipSync
        private void _voiceServer_VoiceClientTalking(long connectionId, bool isTalking)
        {
            var p = GetPlayerByConnectionId(connectionId);
            if (p != null)
            {
                if (!p.IsReady() || p.IsDead())
                    return;
                var pPos = p.GetVoicePosition();
                //logger.Debug($"Talking {p.GetCharacterName()} {isTalking}");
                var pls = GTAAPI.shared.GetAllPlayers().ToList().Where(c => c.IsReady() && c.GetVoicePosition().DistanceTo2D(pPos) < 20).ToList();
                if (isTalking)
                    pls.ForEach(pt => pt.TriggerEvent("LIPSYNC", p, "mp_facial", "mic_chatter", true));
                else
                    pls.ForEach(pt => pt.TriggerEvent("LIPSYNC", p, "facials@gen_male@variations@normal", "mood_normal_1", true));
            }
        }

        private Client GetPlayerBySessionId(string clientGUID)
        {
            return API.GetAllPlayers().ToList().FirstOrDefault(p => p.GetName() == clientGUID);
        }

        private Client GetPlayerByTeamspeakId(string teamspeakId)
        {
            return API.GetAllPlayers().ToList().FirstOrDefault(p => p.GetTeamspeakID() == teamspeakId);
        }

        private Client GetPlayerByConnectionId(long connectionId)
        {
            return API.GetAllPlayers().ToList().FirstOrDefault(p => p.GetVoiceConnectionID() == connectionId);
        }

        private void _voiceServer_VoiceClientOutdated(string clientGUID, Version hisVersion, Version ourVersion)
        {
            var p = GetPlayerBySessionId(clientGUID);
            if (p != null && !p.GetData("_VOICE_GOT_VERSION_WARNING", false))
            {
                p.SetData("_VOICE_GOT_VERSION_WARNING", true);
                p.Kick($"VoicePlugin outdated. Please update to {ourVersion}.");
                return;
            }
        }

        private void _voiceServer_VoiceClientDisconnected(long connectionId)
        {

        }


        private void _voiceServer_VoiceClientSpeakersStatusChanged(long connectionId, bool isMuted)
        {
            var p = GetPlayerByConnectionId(connectionId);
            if (p != null)
            {
                logger.Debug($"{p.GetCharacterName()} Speakers muted {isMuted}");
            }
        }

        private void _voiceServer_VoiceClientMicrophoneStatusChanged(long connectionId, bool isMuted)
        {
            var p = GetPlayerByConnectionId(connectionId);
            if (p != null)
            {
                logger.Debug($"{p.GetCharacterName()} Mic muted {isMuted}");
            }
        }

        private void _voiceServer_VoiceClientConnected(string clientGUID, string teamspeakID, ushort teamspeakClientID, long connectionID, string clientName, bool micMuted, bool speakersMuted)
        {
            var p = GetPlayerBySessionId(clientGUID);
            if (p != null)
            {
                logger.Debug($"VoiceConnect {p.SocialClubName} {teamspeakID} {teamspeakClientID} {connectionID}");
                _voiceServer.ConfigureClient(connectionID, p.GetData("PLAYER_TEAMSPEAK_NAME", ""), p.IsAdmin());
                p.SetData("VOICE_ID", connectionID);
                p.SetData("VOICE_TS_ID", teamspeakClientID);
                p.SetData("PLAYER_TEAMSPEAK_IDENT", teamspeakID);
            }
        }


        /* Teamspeak Handling Functions */
        public static void Connect(Client player)
        {
            player.TriggerEvent("GTMPVOICE", GameMode.VoiceServerIP, GameMode.VoiceServerPort, GameMode.VoiceServerSecret,
                 player.GetData("PLAYER_TEAMSPEAK_NAME", ""), GameMode.VoiceServerPluginVersion.ToString(), GameMode.VoiceClientPort);
        }


        public static int GenerateTeamspeakName(Client player)
        {
            int newVal = GameMode.RND.Next(1000000, 8999999);
            while (usedIds.Contains(newVal))
                newVal = GameMode.RND.Next(1000000, 8999999);
            usedIds.Add(newVal);
            player.SetData("PLAYER_TEAMSPEAK_ID", newVal);
            player.SetData("PLAYER_TEAMSPEAK_NAME", newVal.ToString());
            return newVal;
        }

        public void UpdateTeamspeak()
        {
            var players = API.GetAllPlayers().ToList();
            players.ForEach(p => { try { UpdateTeamspeakForUser(p, players); } catch (Exception ex) { logger.Error(ex, $"UpdateTeamspeakForUser {p.SocialClubName} : {ex.Message}"); } });
        }

        public static bool ToggleVoiceDebug(Client player)
        {
            var debugVoice = player.GetData("_DEBUG_VOICE", false);
            player.SetData("_DEBUG_VOICE", !debugVoice);
            player.SendNotification("Debug Voice " + (!debugVoice ? "enabled" : "disabled"));
            if (!debugVoice)
            {
                sharedLogger.Warn($"DEBUGVOICE {player.SocialClubName} enabled");
            }
            var targetId = player.GetData("VOICE_ID", 0L);
            if (targetId != 0)
            {
                _voiceServer.SendCommand(targetId, "DEBUGVOICE", !debugVoice ? "ON" : "OFF");
            }
            return !debugVoice;
        }

        public void UpdateTeamspeakForUser(Client player, List<Client> allPlayers)
        {
            var playerPos = player.GetVoicePosition();
            var playerRot = player.Rotation;
            var rotation = Math.PI / 180 * (playerRot.Z * -1);
            var playerVehicle = player.Vehicle;
            var targetId = player.GetData("VOICE_ID", 0L);
            var cId = player.GetCharacterId();

            if (targetId == 0)
                return;

            if (player.IsDead())
            {
                _voiceServer.SendUpdate(targetId, new List<VoiceLocationInformation>());
                return;
            }


            var playersIHear = new Dictionary<string, VoiceLocationInformation>();
            List<string> mutePlayer = new List<string>();
            var debugVoice = player.GetData("_DEBUG_VOICE", false);
            var preface = $"DEBUGVOICE '{player.SocialClubName}' => ";

            if (debugVoice)
            {
                logger.Debug($"{preface} {playerPos}");
            }

            // Players near me
            var inRangePlayers = allPlayers.Where(cl => (cl != player) && (cl.IsReady()) && (cl.GetVoicePosition().DistanceTo2D(playerPos) <= 50) && (cl.Dimension == player.Dimension)).ToList();

            if (inRangePlayers != null)
            {
                if (debugVoice)
                {
                    logger.Debug($"{preface} {inRangePlayers.Count} in range");
                }
                foreach (var streamedPlayer in inRangePlayers)
                {
                    var n = streamedPlayer.GetTeamspeakID();
                    if (streamedPlayer.IsDead() || (streamedPlayer == player) || !player.IsReady() || !streamedPlayer.IsReady())
                    {
                        continue;
                    }

                    var streamedPlayerPos = streamedPlayer.GetVoicePosition();
                    var distance = playerPos.DistanceTo(streamedPlayerPos);
                    var range = streamedPlayer.GetData<int>("VOICE_RANGE_INT", 5); // VoiceRange of streamed player in meters
                    var volumeModifier = 0f;
                    if (debugVoice)
                    {
                        logger.Debug($"{preface} '{streamedPlayer.GetCharacterName()}' d:{distance} r:{range} {distance < range} {streamedPlayerPos}");
                    }
                    if (distance <= range)
                    {
                        if (Math.Abs(streamedPlayerPos.Z - playerPos.Z) > range)
                        {
                            logger.Debug($"{preface} drop {streamedPlayer.GetCharacterName()} ZRange {Math.Abs(streamedPlayerPos.Z - playerPos.Z)} Speakrange {range} dist {distance}");
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

                        var tData = playersIHear.GetOrAdd(n, new VoiceLocationInformation(n, streamedPlayer.GetTeamspeakClientID()));

                        /* Make sure distance and volume modifier is within TS range -10 ... +10 */
                        var xPos = Math.Max(-10f, Math.Min(10f, (float)(Math.Round(x * 1000) / 1000)));
                        var yPos = Math.Max(-10f, Math.Min(10f, (float)(Math.Round(y * 1000) / 1000)));
                        var volMod = Math.Max(-10f, Math.Min(10f, (float)(Math.Round(volumeModifier * 1000) / 1000)));

                        tData.Update(new TSVector(xPos, yPos, 0), volMod, false);

                    }
                }
            }
            // Player i phone with
            if (player.HasData("CALLING_PLAYER_NAME") && (player.GetData("CALL_IS_STARTED", 0) == 1))
            {
                var callingPlayerName = player.GetData("CALLING_PLAYER_NAME", String.Empty);
                if (!String.IsNullOrEmpty(callingPlayerName))
                {
                    var p = GetPlayerByTeamspeakId(callingPlayerName);
                    if (debugVoice)
                    {
                        logger.Debug($"{preface} phonecall {p.GetCharacterName()}");
                    }
                    var tData = playersIHear.GetOrAdd(callingPlayerName, new VoiceLocationInformation(callingPlayerName, p.GetTeamspeakClientID()));
                    tData.Update(new TSVector(10, 0, 0), 5, true);
                }
            }

            // Radio/Walkie Talkie
            if (player.GetRadioMode() != RadioModes.OFF)
            {
                var radioVolume = player.GetData("RADIO_VOLUME", 4);
                var radioPlayers = RadioController.GetSpeakingPlayersTeamspeak(player, player.GetRadioChannel());
                radioPlayers.ForEach(fpl =>
                {
                    var p = GetPlayerByTeamspeakId(fpl);
                    if (debugVoice)
                    {
                        logger.Debug($"{preface} Radio {p.GetCharacterName()}");
                    }
                    var tData = playersIHear.GetOrAdd(fpl, new VoiceLocationInformation(fpl, p.GetTeamspeakClientID()));
                    tData.Update(new TSVector(10, 0, 0), radioVolume, true);
                });
            }

            if (player.IsInVehicle)
            {
                var others = player.Vehicle?.Occupants.ToList();
                if (others != null)
                {
                    others.Remove(player);
                    others.ForEach(p =>
                    {
                        if (debugVoice)
                        {
                            logger.Debug($"{preface} inSameVehcile {p.GetCharacterName()}");
                        }

                        var tsId = p.GetTeamspeakID();
                        var tData = playersIHear.GetOrAdd(tsId, new VoiceLocationInformation(tsId, p.GetTeamspeakClientID()));
                        tData.Update(new TSVector(0, 0, 0), 4, true);
                    });
                }
            }

            PlayerHears[player.GetCharacterId()] = playersIHear;


            if (!player.IsDead())
            {
                if (debugVoice)
                {
                    logger.Debug($"{preface} {playersIHear.Count} entries");
                    playersIHear.Values.ForEach(v =>
                   {
                       logger.Debug($"{preface} hears {v.ClientID} v:{v.VolumeModifier} p:{v.Position}");
                   });
                }
                _voiceServer.SendUpdate(targetId, playersIHear.Values);
            }
        }

    }
}
