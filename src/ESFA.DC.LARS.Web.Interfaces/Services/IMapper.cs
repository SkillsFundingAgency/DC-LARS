namespace ESFA.DC.LARS.Web.Interfaces.Services
{
    public interface IMapper<TIn, TOut>
    {
        TOut Map(TIn input);
    }
}
