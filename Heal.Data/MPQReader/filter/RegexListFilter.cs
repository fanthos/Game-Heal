using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Heal.Data.MpqReader.filter
{
    public class RegexListFilter : IListFilter
    {
        public MpqArchive.FileInfo[] FilterList(MpqArchive.FileInfo[] List, string FilterPattern)
        {
            Regex regex = new Regex(FilterPattern, RegexOptions.IgnoreCase);
            List<MpqArchive.FileInfo> list = new List<MpqArchive.FileInfo>();
            FilterPattern = FilterPattern.ToLower();
            foreach (MpqArchive.FileInfo info in List)
            {
                string input = info.Name.ToLower();
                if (regex.IsMatch(input))
                {
                    list.Add(info);
                }
            }
            list.Sort(new MpqArchive.FileInfo.Comparer());
            return list.ToArray();
        }
    }
}


