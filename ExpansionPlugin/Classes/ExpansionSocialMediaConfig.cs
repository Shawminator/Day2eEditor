using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class ExpansionSocialMediaConfig : ExpansionBaseIConfigLoader<ExpansionSocialMediaSettings>
    {
        public const int CurrentVersion = 2;

        public ExpansionSocialMediaConfig(string path) : base(path)
        {
        }
        protected override ExpansionSocialMediaSettings CreateDefaultData()
        {
            return new ExpansionSocialMediaSettings(CurrentVersion);
        }
        protected override IEnumerable<string> ValidateData()
        {
            return Data.FixMissingOrInvalidFields();
        }
    }
    public class ExpansionSocialMediaSettings : IEquatable<ExpansionSocialMediaSettings>, IDeepCloneable<ExpansionSocialMediaSettings>
    {
        public int m_Version { get; set; }
        public BindingList<ExpansionNewsFeedTextSetting> NewsFeedTexts { get; set; }
        public BindingList<ExpansionNewsFeedLinkSetting> NewsFeedLinks { get; set; }


        [JsonIgnore]
        public string Filename { get; set; }
        [JsonIgnore]
        public bool isDirty { get; set; }

        public ExpansionSocialMediaSettings()
        {
        }
        public ExpansionSocialMediaSettings(int CurrentVersion)
        {
            m_Version = CurrentVersion;
            DefaultNewsFeedTexts();
            DefaultNewsFeedLinks();
        }
        public void DefaultNewsFeedTexts()
        {
            NewsFeedTexts = new BindingList<ExpansionNewsFeedTextSetting>()
            {
                new ExpansionNewsFeedTextSetting()
                {
                    m_Title = "CHANGE ME",
                    m_Text = "THIS IS A PLACEHOLDER TEXT"
                }
            };
        }
        public void DefaultNewsFeedLinks()
        {
            NewsFeedLinks = new BindingList<ExpansionNewsFeedLinkSetting>()
            {
                new ExpansionNewsFeedLinkSetting()
                {
                    m_Label = "Discord",
                    m_Icon =  "set:expansion_iconset image:icon_discord",
                    m_URL = "https://www.google.com/"
                },
                new ExpansionNewsFeedLinkSetting()
                {
                    m_Label = "Twitter",
                    m_Icon =  "set:expansion_iconset image:icon_twitter",
                    m_URL = "https://www.google.com/"
                }
            };
        }
        public void SetStringValue(string mytype, string myString)
        {
            GetType().GetProperty(mytype).SetValue(this, myString, null);
        }
        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            if (m_Version != ExpansionSocialMediaConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {m_Version} to {ExpansionSocialMediaConfig.CurrentVersion}");
                m_Version = ExpansionSocialMediaConfig.CurrentVersion;
            }
            if (NewsFeedTexts == null)
            {
                DefaultNewsFeedTexts();
                fixes.Add("Initilised Default NewsFeedTexts");
            }
            foreach(ExpansionNewsFeedTextSetting ts in NewsFeedTexts)
            {
                if(string.IsNullOrWhiteSpace(ts.m_Title))
                {
                    ts.m_Title = "CHANGE ME";
                    fixes.Add("Corrected m_Title");
                }
                if(string.IsNullOrWhiteSpace(ts.m_Text))
                {
                    ts.m_Text = "THIS IS A PLACEHOLDER TEXT";
                    fixes.Add("Corrected m_Text");
                }
            }
            foreach (ExpansionNewsFeedLinkSetting ts in NewsFeedLinks)
            {
                if (string.IsNullOrWhiteSpace(ts.m_Label))
                {
                    ts.m_Label = "CHANGE ME";
                    fixes.Add("Corrected m_Label");
                }
                if (string.IsNullOrWhiteSpace(ts.m_Icon))
                {
                    ts.m_Icon = "THIS IS A PLACEHOLDER TEXT";
                    fixes.Add("Corrected m_Icon");
                }
                if (string.IsNullOrWhiteSpace(ts.m_URL))
                {
                    ts.m_URL = "https://www.google.com/";
                    fixes.Add("Corrected m_URL");
                }
            }
            if (NewsFeedLinks == null)
            {
                DefaultNewsFeedLinks();
                fixes.Add("Initilised Default NewsFeedLinks");
            }
            return fixes;
        }
        public bool Equals(ExpansionSocialMediaSettings other)
        {

            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            if (m_Version != other.m_Version) return false;
            
            if (NewsFeedTexts == null && other.NewsFeedTexts == null)
                return true;

            if (NewsFeedTexts == null || other.NewsFeedTexts == null)
                return false;

            if (NewsFeedTexts.Count != other.NewsFeedTexts.Count)
                return false;

            for (int i = 0; i < NewsFeedTexts.Count; i++)
            {
                if (!NewsFeedTexts[i].Equals(other.NewsFeedTexts[i]))
                    return false;
            }
            if (NewsFeedLinks == null && other.NewsFeedLinks == null)
                return true;

            if (NewsFeedLinks == null || other.NewsFeedLinks == null)
                return false;

            if (NewsFeedLinks.Count != other.NewsFeedLinks.Count)
                return false;

            for (int i = 0; i < NewsFeedLinks.Count; i++)
            {
                if (!NewsFeedLinks[i].Equals(other.NewsFeedLinks[i]))
                    return false;
            }

            return true;
        }
        public override bool Equals(object? obj) => Equals(obj as ExpansionSocialMediaSettings);
        public ExpansionSocialMediaSettings Clone()
        {

            return new ExpansionSocialMediaSettings
            {
                m_Version = this.m_Version,
                NewsFeedTexts = new BindingList<ExpansionNewsFeedTextSetting>(this.NewsFeedTexts.Select(x => x.Clone()).ToList()),
                NewsFeedLinks = new BindingList<ExpansionNewsFeedLinkSetting>(this.NewsFeedLinks.Select(x => x.Clone()).ToList())
            };
        }
    }
    public class ExpansionNewsFeedTextSetting
    {
        public string? m_Title { get; set; }
        public string? m_Text { get; set; }

        public override string ToString()
        {
            return m_Title;
        }
        public override bool Equals(object obj)
        {

            if (obj is not ExpansionNewsFeedTextSetting other)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (m_Title != other.m_Title) return false;
            if (m_Text != other.m_Text) return false;

            return true;
        }
        public ExpansionNewsFeedTextSetting Clone()
        {

            return new ExpansionNewsFeedTextSetting
            {
                m_Title = this.m_Title,
                m_Text = this.m_Text
            };
        }
    }

    public class ExpansionNewsFeedLinkSetting
    {
        public string? m_Label { get; set; }
        public string? m_Icon { get; set; }
        public string? m_URL { get; set; }

        public override string ToString()
        {
            return m_Label;
        }
        public override bool Equals(object obj)
        {

            if (obj is not ExpansionNewsFeedLinkSetting other)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (m_Label != other.m_Label) return false;
            if (m_Icon != other.m_Icon) return false;
            if (m_URL != other.m_URL) return false;

            return true;
        }
        public ExpansionNewsFeedLinkSetting Clone()
        {

            return new ExpansionNewsFeedLinkSetting
            {
                m_Label = this.m_Label,
                m_Icon = this.m_Icon,
                m_URL = this.m_URL
            };
        }
    }
}
