namespace ESFA.DC.LARS.API.Interfaces
{
    public interface ISearchCleaningService
    {
        string EscapeSearchSpecialCharacters(string term);

        string EscapeFilterSpecialCharacters(string term);
    }
}