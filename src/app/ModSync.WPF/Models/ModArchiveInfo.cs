using System;
using System.Linq;

namespace ModSync.WPF.Models
{
    public class ModArchiveInfo
    {
        public string FullPath { get; set; }

        public string Version { get; set; }

        public McModInfoRoot McModInfoRoot { get; set; }

        public string FileName => !string.IsNullOrEmpty(FullPath) ? FullPath.Substring(FullPath.LastIndexOf(@"\", StringComparison.Ordinal)).TrimStart('\\') : "--";

        public ModArchiveInfo()
        {
            FullPath = "";
            Version = "0.0.0.0";
            McModInfoRoot = new McModInfoRoot();
        }

        public ModArchiveInfo(string fullPath, McModInfoRoot mcModInfoRoot)
        {
            FullPath = fullPath;
            McModInfoRoot = mcModInfoRoot;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", FileName, McModInfoRoot);
        }
    }
}