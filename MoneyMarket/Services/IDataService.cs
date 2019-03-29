using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyMarket.Services
{
    interface IDataService
    {
        List<Seller> RetrieveSellerDataList();
        bool CreateSeller();
        bool UpdateAvailableQty(Dictionary<int, int> sellerList);
    }
}
