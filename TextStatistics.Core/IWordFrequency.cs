namespace TextStatistics.Core
{
    public interface IWordFrequency
    {
        long Frequency { get; set; }
        string Word { get; set; }
    }
}