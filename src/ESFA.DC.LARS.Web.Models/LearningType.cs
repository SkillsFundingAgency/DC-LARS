using System.ComponentModel.DataAnnotations;

namespace ESFA.DC.LARS.Web.Models
{
    public enum LearningType
    {
        Qualifications = 0,
        Frameworks = 1,
        Units = 2,
        Standards = 3,
        [Display(Name = "T Levels")]
        TLevels = 4
    }
}
