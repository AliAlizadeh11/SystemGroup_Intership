using System;
using System.Collections.Generic;

namespace LoyaltyClub.Plugin
{
    public interface ILoyaltyPointsPlugin
    {
        int CalculatePoints(CustomerPurchaseData purchaseData);
    }

    public class CustomerPurchaseData
    {
        public int TracksPurchased { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
