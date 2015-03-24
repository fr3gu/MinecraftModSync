using System;
using System.Linq;
using System.Runtime.Remoting;
using ModSync.WPF.Models;

namespace ModSync.WPF.Models
{
    public class McModInfo
    {
        public string ModId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public string McVersion { get; set; }
        public string Url { get; set; }
        public string UpdateUrl { get; set; }
        public string[] AuthorList { get; set; }
        public string Credits { get; set; }
        public string LogoFile { get; set; }
        public string[] ScreenShots { get; set; }
        public string Parent { get; set; }
        public string[] RequiredMods { get; set; }
        public string[] Dependencies { get; set; }
        public object[] Dependants { get; set; }
        public bool UseDependencyInformation { get; set; }

        public override string ToString()
        {
            return string.Format("{0} ({1}_{2}) by {3}", ModId, Version, McVersion, AuthorList != null ? string.Join(", ", AuthorList) : "");
        }
    }
}