namespace ESFA.DC.LARS.Web.Interfaces
{
    public interface IClientValidationService
    {
        string SearchTermLengthValid(string searchTerm);

        string FilterLengthValid(string filterTerm);
    }
}