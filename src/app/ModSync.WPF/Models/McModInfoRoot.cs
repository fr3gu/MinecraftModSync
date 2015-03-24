using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModSync.WPF.Models
{
    public class McModInfoRoot
    {
        public McModInfoRoot()
        {
        }

        public McModInfoRoot(McModInfo[] mcModInfos)
        {
            McModInfos = mcModInfos;
        }

        public McModInfo[] McModInfos { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            foreach (var mcModInfo in McModInfos)
            {
                stringBuilder.AppendLine(mcModInfo.ToString());
            }
            return stringBuilder.ToString().TrimEnd(Environment.NewLine.ToCharArray());
        }
    }
}
