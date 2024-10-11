using System;

namespace LoyaltyClub.Plugin
{
    public class SeasonalBonusPointsPlugin : ILoyaltyPointsPlugin
    {
        public int CalculatePoints(CustomerPurchaseData purchaseData)
        {
            var springMonths = new[] { 3, 4, 5 };
            
            return springMonths.Contains(purchaseData.PurchaseDate.Month) ? 50 : 0;
        }
    }
}