namespace ESFA.DC.LARS.Web.Models
{
    public class TLevelSearchModel : AbstractSearchModel
    {
        public TLevelSearchModel()
        {
            SearchType = LearningType.TLevels;
        }

        public override LearningType? SearchType { get; set; }
    }
}
