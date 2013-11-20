using System.Collections.Generic;

namespace Heal.Data.MpqReader.filter
{
    public class DefaultListFilter : IListFilter
    {
        public MpqArchive.FileInfo[] FilterList(MpqArchive.FileInfo[] List, string FilterPattern)
        {
            List<MpqArchive.FileInfo> list = new List<MpqArchive.FileInfo>();
            FilterPattern = FilterPattern.ToLower();
            foreach (MpqArchive.FileInfo info in List)
            {
                string str = info.Name.ToLower();
                if ((FilterPattern.StartsWith("*") && str.EndsWith(FilterPattern.Substring(1))) || str.StartsWith(FilterPattern))
                {
                    list.Add(info);
                }
            }
            list.Sort(new MpqArchive.FileInfo.Comparer());
            return list.ToArray();
        }
    }
}


