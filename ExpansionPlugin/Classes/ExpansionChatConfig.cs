using Day2eEditor;
using System.ComponentModel;

namespace ExpansionPlugin
{
    public class ExpansionChatConfig : ExpansionBaseIConfigLoader<ExpansionChatSettings>
    {
        public const int CurrentVersion = 4;

        public ExpansionChatConfig(string path) : base(path)
        {
        }

        protected override ExpansionChatSettings CreateDefaultData()
        {
            return new ExpansionChatSettings(CurrentVersion);
        }

        protected override IEnumerable<string> ValidateData()
        {
            return Data.FixMissingOrInvalidFields();
        }

    }
    public class ExpansionChatSettings : IEquatable<ExpansionChatSettings>, IDeepCloneable<ExpansionChatSettings>
    {
        public int m_Version { get; set; }
        public int? EnableGlobalChat { get; set; }
        public int? EnablePartyChat { get; set; }
        public int? EnableTransportChat { get; set; }
        public int? EnableExpansionChat { get; set; }
        public ExpansionChatColors ChatColors { get; set; }
        public BindingList<string> BlacklistedWords { get; set; }
 
        public ExpansionChatSettings()
        { }
        public ExpansionChatSettings(int CurrentVersion)
        {
            m_Version = CurrentVersion;
            EnableGlobalChat = 1;
            EnablePartyChat = 1;
            EnableTransportChat = 1;
            EnableExpansionChat = 1;
            DefaultColors();
            BlacklistedWords = new BindingList<string>();
        }
        private void DefaultColors()
        {
            ChatColors.SystemChatColor = "BA45BAFF";
            ChatColors.AdminChatColor = "C0392BFF";
            ChatColors.GlobalChatColor = "58C3F7FF";
            ChatColors.DirectChatColor = "FFFFFFFF";
            ChatColors.TransportChatColor = "FFCE09FF";
            ChatColors.PartyChatColor = "FFCE09FF";
            ChatColors.TransmitterChatColor = "F9FF49FF";
            ChatColors.StatusMessageColor = "4B77BEFF";
            ChatColors.ActionMessageColor = "F7CA18FF";
            ChatColors.FriendlyMessageColor = "2ECC71FF";
            ChatColors.ImportantMessageColor = "F22613FF";
            ChatColors.DefaultMessageColor = "FFFFFFFF";
        }
        public bool Equals(ExpansionChatSettings other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return m_Version == other.m_Version &&
                   EnableGlobalChat == other.EnableGlobalChat &&
                   EnablePartyChat == other.EnablePartyChat &&
                   EnableTransportChat == other.EnableTransportChat &&
                   EnableExpansionChat == other.EnableExpansionChat &&
                   ChatColors.Equals(other.ChatColors) &&
                   BlacklistedWords.SequenceEqual(other.BlacklistedWords);
        }
        public override bool Equals(object? obj) => Equals(obj as ExpansionCoreSettings);
        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();
            if (m_Version != ExpansionChatConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {m_Version} to {ExpansionChatConfig.CurrentVersion}");
                m_Version = ExpansionChatConfig.CurrentVersion;
            }

            if (EnableGlobalChat == null ||(EnableGlobalChat != 0 && EnableGlobalChat != 1))
            {
                EnableGlobalChat = 1;
                fixes.Add("Corrected EnableGlobalChat");
            }

            if (EnablePartyChat == null || (EnablePartyChat != 0 && EnablePartyChat != 1))
            {
                EnablePartyChat = 1;
                fixes.Add("Corrected EnablePartyChat");
            }

            if (EnableTransportChat == null || (EnableTransportChat != 0 && EnableTransportChat != 1))
            {
                EnableTransportChat = 1;
                fixes.Add("Corrected EnableTransportChat");
            }

            if (EnableExpansionChat == null || (EnableExpansionChat != 0 && EnableExpansionChat != 1))
            {
                EnableExpansionChat = 1;
                fixes.Add("Corrected EnableExpansionChat");
            }

            if (ChatColors == null)
            {
                ChatColors = new ExpansionChatColors();
                fixes.Add("Initialized ChatColors");
            }

            // Helper function to set default color if null or whitespace
            string SetDefaultColor(string currentValue, string defaultValue, string name)
            {
                if (string.IsNullOrWhiteSpace(currentValue))
                {
                    fixes.Add($"Set default {name}");
                    return defaultValue;
                }
                return currentValue;
            }

            ChatColors.SystemChatColor = SetDefaultColor(ChatColors.SystemChatColor, "BA45BAFF", "SystemChatColor");
            ChatColors.AdminChatColor = SetDefaultColor(ChatColors.AdminChatColor, "C0392BFF", "AdminChatColor");
            ChatColors.GlobalChatColor = SetDefaultColor(ChatColors.GlobalChatColor, "58C3F7FF", "GlobalChatColor");
            ChatColors.DirectChatColor = SetDefaultColor(ChatColors.DirectChatColor, "FFFFFFFF", "DirectChatColor");
            ChatColors.TransportChatColor = SetDefaultColor(ChatColors.TransportChatColor, "FFCE09FF", "TransportChatColor");
            ChatColors.PartyChatColor = SetDefaultColor(ChatColors.PartyChatColor, "FFCE09FF", "PartyChatColor");
            ChatColors.TransmitterChatColor = SetDefaultColor(ChatColors.TransmitterChatColor, "F9FF49FF", "TransmitterChatColor");
            ChatColors.StatusMessageColor = SetDefaultColor(ChatColors.StatusMessageColor, "4B77BEFF", "StatusMessageColor");
            ChatColors.ActionMessageColor = SetDefaultColor(ChatColors.ActionMessageColor, "F7CA18FF", "ActionMessageColor");
            ChatColors.FriendlyMessageColor = SetDefaultColor(ChatColors.FriendlyMessageColor, "2ECC71FF", "FriendlyMessageColor");
            ChatColors.ImportantMessageColor = SetDefaultColor(ChatColors.ImportantMessageColor, "F22613FF", "ImportantMessageColor");
            ChatColors.DefaultMessageColor = SetDefaultColor(ChatColors.DefaultMessageColor, "FFFFFFFF", "DefaultMessageColor");


            if (BlacklistedWords == null)
            {
                BlacklistedWords = new BindingList<string>();
                fixes.Add("Initialized BlacklistedWords");
            }


            return fixes;
        }
        public ExpansionChatSettings Clone()
        {
            return new ExpansionChatSettings()
            {
                m_Version = this.m_Version,
                EnableExpansionChat = this.EnableExpansionChat,
                EnableGlobalChat = this.EnableGlobalChat,
                EnablePartyChat = this.EnablePartyChat,
                EnableTransportChat = this.EnableTransportChat,
                ChatColors = this.ChatColors.Clone(),
                BlacklistedWords = new BindingList<string>(this.BlacklistedWords.ToList())
            };
        }
    }
    public class ExpansionChatColors
    {
        public string SystemChatColor { get; set; }
        public string AdminChatColor { get; set; }
        public string GlobalChatColor { get; set; }
        public string DirectChatColor { get; set; }
        public string TransportChatColor { get; set; }
        public string PartyChatColor { get; set; }
        public string TransmitterChatColor { get; set; }
        public string StatusMessageColor { get; set; }
        public string ActionMessageColor { get; set; }
        public string FriendlyMessageColor { get; set; }
        public string ImportantMessageColor { get; set; }
        public string DefaultMessageColor { get; set; }

        public ExpansionChatColors()
        {
        }
        public override bool Equals(object obj)
        {
            if (obj is not ExpansionChatColors other)
                return false;

            return SystemChatColor == other.SystemChatColor &&
                   AdminChatColor == other.AdminChatColor &&
                   GlobalChatColor == other.GlobalChatColor &&
                   DirectChatColor == other.DirectChatColor &&
                   TransportChatColor == other.TransportChatColor &&
                   PartyChatColor == other.PartyChatColor &&
                   TransmitterChatColor == other.TransmitterChatColor &&
                   StatusMessageColor == other.StatusMessageColor &&
                   ActionMessageColor == other.ActionMessageColor &&
                   FriendlyMessageColor == other.FriendlyMessageColor &&
                   ImportantMessageColor == other.ImportantMessageColor &&
                   DefaultMessageColor == other.DefaultMessageColor;
        }
        public ExpansionChatColors Clone()
        {
            return new ExpansionChatColors()
            {
                SystemChatColor = this.SystemChatColor,
                AdminChatColor = this.AdminChatColor,
                GlobalChatColor = this.GlobalChatColor,
                DirectChatColor = this.DirectChatColor,
                TransportChatColor = this.TransportChatColor,
                PartyChatColor = this.PartyChatColor,
                TransmitterChatColor = this.TransmitterChatColor,
                StatusMessageColor = this.StatusMessageColor,
                ActionMessageColor = this.ActionMessageColor,
                FriendlyMessageColor = this.FriendlyMessageColor,
                ImportantMessageColor = this.ImportantMessageColor,
                DefaultMessageColor = this.DefaultMessageColor
            };
        }

    }
}
