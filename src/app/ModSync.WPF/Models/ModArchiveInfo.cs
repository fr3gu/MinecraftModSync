using System;
using System.Linq;

namespace ModSync.WPF.Models
{
    public class ModArchiveInfo
    {
        private readonly string _version;
        public string FullPath { get; set; }
        public McModInfoRoot McModInfoRoot { get; set; }

        public string Version => string.Format("Version: {0}", _version);

        public string FileName => !string.IsNullOrEmpty(FullPath) ? FullPath.Substring(FullPath.LastIndexOf(@"\", StringComparison.Ordinal)).TrimStart('\\') : "--";

        public ModArchiveInfo()
        {
            FullPath = "";
            McModInfoRoot = new McModInfoRoot();
            _version = "0.0.0.0";
        }

        public ModArchiveInfo(string fullPath, McModInfoRoot mcModInfoRoot)
        {
            FullPath = fullPath;
            McModInfoRoot = mcModInfoRoot;
            _version = McModInfoRoot.McModInfos[0].Version;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", FileName, McModInfoRoot);
        }
    }
}