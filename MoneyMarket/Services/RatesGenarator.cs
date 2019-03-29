using System.Collections.Generic;
using System.Linq;

namespace MoneyMarket.Services
{
    public class RatesGenarator : IExchangeRateGenarator
    {
        /// <summary>
        /// This is the business logic for Backend Chalenge level 1
        /// </summary>
        /// <param name="sellerList"></param>
        /// <param name="units"></param>
        /// <returns></returns>
        public decimal CalculateBestRate(List<Seller> sellerList, int units)
        {
            decimal totalAmount = 0.0m;
            decimal bestRate = 0.0m;
            int tempUnits = units;
            List<Seller> sortedSellerList = null;            

            if (sellerList != null)
            {
                //Sort list descending order to get highest value
                sortedSellerList = sellerList.OrderByDescending(o => o.Rate).ToList();
            }

            if(sortedSellerList != null)
            {
                foreach (var seller in sortedSellerList)
                {
                    if (tempUnits > 0)
                    {
                        if (seller.AvailableToSupply <= tempUnits)
                        {
                            tempUnits = tempUnits - seller.AvailableToSupply;
                            totalAmount = totalAmount + (seller.Rate * seller.AvailableToSupply);
                        }
                        else
                        {
                            totalAmount = totalAmount + (seller.Rate * tempUnits);
                            tempUnits = 0;
                        }
                    }
                    else
                    {
                        break;
                    }

                    bestRate = totalAmount / units;
                }
            }
            return bestRate;
        }

        /// <summary>
        /// This is the business logic for Backend Chalenge level 3
        /// </summary>
        /// <param name="sellerList"></param>
        /// <param name="units"></param>
        /// <returns></returns>
        public decimal CalculateBestRateWithServiceFee(List<Seller> sellerList, int units, Dictionary<int, int> selectedList)
        {
            decimal bestRate = 0.0m;
            decimal tatalAmount = 0.0m;

            var selectedSellerList = RetreiveSupplierList(sellerList, units);

            foreach (var sellerData in selectedSellerList)
            {
                decimal rateAfterFee = 0.0m;
                var seller = sellerList.Where(a => a.Id == sellerData.Key).SingleOrDefault();
                rateAfterFee = seller.Rate - ((seller.TransactionFee * seller.Rate) / sellerData.Value);
                tatalAmount = tatalAmount + (sellerData.Value * rateAfterFee);
            }

            bestRate = tatalAmount / units;

            return bestRate;
        }

        /// <summary>
        /// Returning US doller consumption list with each seller
        /// </summary>
        /// <param name="sellerList"></param>
        /// <param name="units"></param>
        /// <returns></returns>
        public Dictionary<int, int> RetreiveSupplierList(List<Seller> sellerList, int units)
        {
            List<Seller> sortedSellerList = null;
            int tempUnits = units;
            Dictionary<int, int> selectedSellerList = null;

            if (sellerList != null)
            {
                sortedSellerList = sellerList.OrderByDescending(o => o.Rate).ToList();

                if (sortedSellerList != null)
                {
                    foreach (var seller in sortedSellerList)
                    {
                        if (tempUnits > 0)
                        {
                            if (seller.AvailableToSupply <= tempUnits)
                            {
                                selectedSellerList.Add(seller.Id, seller.AvailableToSupply);
                                tempUnits = tempUnits - seller.AvailableToSupply;
                            }
                            else
                            {
                                selectedSellerList.Add(seller.Id, tempUnits);
                                tempUnits = 0;
                            }
                        }
                        else
                        {
                            break;
                        }
                        
                    }
                }
            }

            return selectedSellerList;
        }
    }
}
