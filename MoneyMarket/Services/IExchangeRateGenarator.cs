using System.Collections.Generic;

namespace MoneyMarket.Services
{
    interface IExchangeRateGenarator
    {
        decimal CalculateBestRate(List<Seller> sellerList, int units);
        Dictionary<int, int> RetreiveSupplierList(List<Seller> sellerList, int units);
        decimal CalculateBestRateWithServiceFee(List<Seller> sellerList, int units, Dictionary<int, int> selectedList);
    }
}
