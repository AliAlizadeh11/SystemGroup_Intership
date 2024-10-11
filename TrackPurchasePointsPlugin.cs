using System;

namespace LoyaltyClub.Plugin
{
    public class TrackPurchasePointsPlugin : ILoyaltyPointsPlugin
    {
        public int CalculatePoints(CustomerPurchaseData purchaseData)
        {
            return purchaseData.TracksPurchased switch
            {
                1 => 5,
                2 => 15,
                _ when purchaseData.TracksPurchased >= 3 => 30,
                _ => 0
            };
        }
    }
}