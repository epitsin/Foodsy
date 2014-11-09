using System.ComponentModel.DataAnnotations;
namespace Foodsy.Data.Models
{
    public enum ChallengeType
    {
        [Display(Name="Weight loss")]
        WeightLoss,
        [Display(Name = "Weight gain")]
        WeightGain,
        [Display(Name = "Better health")]
        BetterHealth,
        [Display(Name = "Detoxification")]
        Detox
    }
}
