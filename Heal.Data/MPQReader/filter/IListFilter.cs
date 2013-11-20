namespace Heal.Data.MpqReader.filter
{
    public interface IListFilter
    {
        MpqArchive.FileInfo[] FilterList(MpqArchive.FileInfo[] List, string FilterPattern);
    }
}


