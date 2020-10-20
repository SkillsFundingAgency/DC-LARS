namespace ESFA.DC.LARS.API.Interfaces
{
    public interface IMapper<TIn, TOut>
    {
        TOut Map(TIn input);
    }
}
