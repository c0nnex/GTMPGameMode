using ConcurrentCollections;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Managers;
using GTMPGameMode;
using GTMPGameMode.Base;
using Newtonsoft.Json;
using NLog;
using Server.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTMPGameMode.VoiceSupport
{
    public class RadioController : GameModeScript
    {
        private static Dictionary<int, RadioInformation> _radioChannels = new Dictionary<int, RadioInformation>();

        public RadioController()
        {

            ClientEventManager.RegisterClientEvent("RADIO_PTT", OnRadioSpeakingKey);
            ClientEventManager.RegisterClientEvent("RADIO_TOGGLE_SPEAK", OnRadioToggleSpeaking);
            ClientEventManager.RegisterClientEvent("RADIO_MUTE", OnRadioMute);
            ClientEventManager.RegisterClientEvent("RADIO_NEXT", OnRadioNextChannel);

            API.onPlayerDisconnected += API_onPlayerDisconnected;
        }

        private void API_onPlayerDisconnected(Client player, string reason)
        {
            _radioChannels.Values.ForEach(rc =>
            {
                rc.Leave(player);
            });
        }

        private void World_OnWorldReloadConfig()
        {

        }


        private void OnRadioNextChannel(Client player, string eventName, params object[] args)
        {
            if (!player.CanUseRadio())
                return;
            var c = GetPlayerRadioChannels(player).Select(f => f.ChannelFrequency).ToList();
            if (c.Count > 1)
            {
                int curFreq = player.GetRadioChannel();
                PlayerLeaveChannel(player, curFreq, false);
                int nextChannel = (c.IndexOf(curFreq) + 1) % c.Count;
                PlayerJoinChannel(player, c[nextChannel]);
            }
            else
            {
                if (c.Count > 0)
                    PlayerJoinChannel(player, c[0], true);
            }
        }

        private void OnRadioSpeakingKey(Client player, string eventName, params object[] args)
        {
            if (!player.CanUseRadio())
                return;
            if (GetArg(args, 0, true))
                OnRadioStartSpeaking(player);
            else
                OnRadioStopSpeaking(player);
        }

        private void OnRadioStartSpeaking(Client player)
        {
            if (!player.CanUseRadio())
                return;
            if (player.IsDead())
                return;
            var channelFreq = player.GetRadioChannel();
            if (channelFreq > 0)
            {
                var channel = GetRadioChannel(channelFreq);
                if (channel == null)
                    return;
                if (!channel.IsMember(player))
                {
                    PlayerLeaveChannel(player, channelFreq, true, false);
                    return;
                }

                channel.Unmute(player);
                if (channel.Speaking.Add(player))
                {
                    channel.Listening.ForEach(pl => pl.PlaySound("mic_click_on"));
                }
                player.setData("RADIO_MODE", "send");
                player.UpdateHUD();
            }
        }

        private void OnRadioStopSpeaking(Client player)
        {
            if (!player.CanUseRadio())
                return;
            var channelFreq = player.GetRadioChannel();
            if (channelFreq > 0)
            {
                var channel = GetRadioChannel(channelFreq);
                if (channel == null)
                    return;
                if (!channel.IsMember(player))
                {
                    PlayerLeaveChannel(player, channelFreq, true, false);
                    return;
                }
                if (channel.Speaking.TryRemove(player))
                    channel.Listening.ForEach(pl => pl.PlaySound("mic_click_off"));

                player.setData("RADIO_MODE", "on");
                player.UpdateHUD();
            }
        }

        private void OnRadioToggleSpeaking(Client player, string eventName, params object[] args)
        {
            if (!player.CanUseRadio())
                return;
            var channelFreq = player.GetRadioChannel();
            if (channelFreq > 0)
            {
                var channel = GetRadioChannel(channelFreq);
                if (!channel.IsMember(player))
                    return;
                if (player.IsDead())
                    return;
                channel.Unmute(player);

                if (channel.Speaking.Contains(player))
                {
                    channel.Listening.ForEach(pl => pl.PlaySound("mic_click_off"));
                    channel.Speaking.TryRemove(player);
                    player.SetRadioMode(RadioModes.LISTENING);
                    player.UpdateHUD();
                }
                else
                {
                    channel.Listening.ForEach(pl => pl.PlaySound("mic_click_on"));
                    channel.Speaking.Add(player);
                    player.SetRadioMode(RadioModes.SPEAKING);
                    player.UpdateHUD();
                }

            }
        }

        private void OnRadioMute(Client player, string eventName, params object[] args)
        {
            if (!player.CanUseRadio())
                return;
            var channelFreq = player.GetRadioChannel();
            if (channelFreq > 0)
            {
                var channel = GetRadioChannel(channelFreq);
                if (!channel.IsMember(player))
                    return;
                if (player.GetRadioMode() != RadioModes.OFF)
                {
                    channel.Mute(player);
                }
                else
                {
                    channel.Unmute(player);
                }
            }
        }

        public static List<string> GetSpeakingPlayersTeamspeak(Client player, int frequency)
        {
            var channel = GetRadioChannel(frequency);
            var myName = player.GetTeamspeakID();
            var playerPos = player.GetRadioVoicePosition();
            var rList = new List<string>();
            if (channel != null)
                channel.Speaking.ForEach(cl =>
                {
                    if (channel.IsDigital || (cl.GetRadioVoicePosition().DistanceTo2D(playerPos) <= GameMode.RadioDistanceMax))
                    {
                        var n = cl.GetTeamspeakID();
                        if (!cl.IsDead() && (n != myName))
                            rList.Add(n);
                    }
                });
            return rList;
        }

        public static void MuteRadio(Client player, bool silent = false)
        {
            int frequency = player.GetRadioChannel();
            if (frequency > 0)
            {
                var channel = GetRadioChannel(frequency);
                channel.Mute(player, silent);
            }
        }
        public static void UnmuteRadio(Client player)
        {
            if (!player.CanUseRadio())
                return;
            int frequency = player.GetRadioChannel();
            if (frequency > 0)
            {
                var channel = GetRadioChannel(frequency);
                channel.Unmute(player);
            }
        }

        public static RadioInformation GetRadioChannel(int frequency)
        {
            if (_radioChannels.ContainsKey(frequency))
                return _radioChannels[frequency];
            return null;
        }

        public static RadioInformation FindRadioChannel(string name)
        {
            return _radioChannels.Values.FirstOrDefault(fc => String.Compare(fc.ChannelName, name, true) == 0);
        }

        public static List<RadioInformation> GetPlayerRadioChannels(Client player)
        {
            return _radioChannels.Values.Where(f => f.IsMember(player) || player.IsAdmin()).ToList();
        }

        public static void PlayerJoinChannel(Client player, int frequency, bool notify = true)
        {
            var channel = GetRadioChannel(frequency);
            if (channel != null && channel.CanJoin(player))
            {
                sharedLogger.Debug($"PlayerJoinChannel {player.GetCharacterName()} {channel.ChannelName}");
                if (channel.Join(player, -2003))
                {
                    player.setData("PLAYER_RADIO_CHANNEL", channel.ChannelName);
                    player.UpdateHUD();
                    if (notify)
                        player.Message($"You joind radiochannel '{channel.ChannelName}'");
                }
            }
        }

        public static void PlayerLeaveChannel(Client player, int frequency, bool removeMember = true, bool notify = true)
        {
            var channel = GetRadioChannel(frequency);
            if (channel != null)
            {
                if (notify)
                    sharedLogger.Debug($"PlayerLeaveChannel {player.GetCharacterName()} {channel.ChannelName} {removeMember}");
                if (channel.Leave(player, removeMember))
                {
                    player.resetData("RADIO_MODE");
                    player.resetData("PLAYER_RADIO_CHANNEL");
                    player.UpdateHUD();
                    if (notify)
                        player.Message($"You left radiochannel '{channel.ChannelName}'");
                }
            }
        }

        internal static void RegisterRadioChannel(int frequency, string name, Func<Client, RadioInformation, bool> canJoinPredicate = null, bool isDigital = false, bool isEncrypted = false)
        {
            var channel = GetRadioChannel(frequency);
            if (channel == null)
            {
                sharedLogger.Debug($"RegisterRadioChannel {frequency} {name} {isDigital} {isEncrypted}");
                channel = new RadioInformation(canJoinPredicate)
                {
                    ChannelFrequency = frequency,
                    ChannelName = name,
                    IsEncrypted = isEncrypted,
                    IsDigital = isDigital
                };
            }
            _radioChannels[frequency] = channel;
        }
    }

    public class RadioInformation
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public int ChannelFrequency;
        public string ChannelName;
        public bool IsEncrypted = false;
        public bool IsDigital = false;

        private Func<Client, RadioInformation, bool> CanJoinChannel = (p, c) => true;

        public HashSet<int> ChannelMembers = new HashSet<int>();

        public RadioInformation(Func<Client, RadioInformation, bool> predicate = null)
        {
            if (predicate != null)
                CanJoinChannel = predicate;
        }

        public bool CanJoin(Client player)
        {
            return CanJoinChannel(player, this);
        }

        public void Mute(Client player, bool silent = false)
        {
            if (Listening.TryRemove(player))
            {
                if (!silent)
                    player.Message("Radio off.");
            }
            Speaking.TryRemove(player);
            player.setData("RADIO_MODE", "off");
            player.UpdateHUD();
        }

        public void Unmute(Client player, bool silent = false)
        {
            if (Listening.Add(player))
            {
                if (!silent)
                    player.Message("Radio on.");
            }
            player.setData("RADIO_MODE", "on");
            player.UpdateHUD();
        }

        public bool Join(Client player, int pin)
        {
            bool isNew = !ChannelMembers.Contains(player.GetCharacterId());
            ChannelMembers.Add(player.GetCharacterId());
            Listening.Add(player);
            player.SetRadioChannel(ChannelFrequency);
            Unmute(player, true);
            return true;
        }

        public bool Leave(Client player, bool removeMember = true)
        {
            bool wasRemoved = false;
            if (removeMember)
                wasRemoved = ChannelMembers.Remove(player.GetCharacterId());
            wasRemoved |= Listening.TryRemove(player);
            Speaking.TryRemove(player);
            if (wasRemoved)
            {
                player.SetRadioChannel(0);
                player.resetData("RADIO_MODE");
                player.UpdateHUD();
            }
            return wasRemoved;
        }

        public bool IsMember(Client player)
        {
            return ChannelMembers.Contains(player.GetCharacterId());
        }

        internal void UpdateMembers()
        {
            Listening.ToList().ForEach(cl =>
           {
               if (!IsMember(cl))
               {
                   Leave(cl);
               }
           });
        }

        [JsonIgnore]
        public ConcurrentHashSet<Client> Speaking = new ConcurrentHashSet<Client>();
        [JsonIgnore]
        public ConcurrentHashSet<Client> Listening = new ConcurrentHashSet<Client>();
    }

}
