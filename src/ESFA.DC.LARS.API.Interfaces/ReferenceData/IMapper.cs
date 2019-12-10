namespace ESFA.DC.LARS.API.Interfaces.ReferenceData
{
    public interface IMapper<TIn, TOut>
    {
        TOut Map(TIn input);
    }
}
