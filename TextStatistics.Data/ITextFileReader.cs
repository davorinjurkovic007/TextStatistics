namespace TextStatistics.Data
{
    public interface ITextFileReader
    {
        string TextOutput { get; }

        void ReadTextFile(string nameOfFile);
    }
}