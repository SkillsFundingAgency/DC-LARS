namespace ESFA.DC.LARS.Web.Models
{
    public abstract class BaseSearchModel
    {
        public string SearchTerm { get; set; }

        public abstract LearningType? SearchType { get; set; }
    }
}
