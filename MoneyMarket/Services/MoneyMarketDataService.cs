using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyMarket.Services
{
    public class MoneyMarketDataService : IDataService
    {
        private readonly MoneyMarketDbContext _dbContext;
        public MoneyMarketDataService()
        {
            if(_dbContext == null)
            {
                MoneyMarketDbContext dbContext = new MoneyMarketDbContext();
                _dbContext = dbContext;
            }
        }
        public bool CreateSeller()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrive Suppliers available to supply amount greater than 0
        /// </summary>
        /// <returns></returns>
        public List<Seller> RetrieveSellerDataList()
        {
            var sellerList = _dbContext.Seller.Select(a => a).Where(a => a.AvailableToSupply > 0).ToList();
            return sellerList;
        }

        public bool UpdateAvailableQty(Dictionary<int, int> sellerList)
        {
            try
            {
                if (sellerList != null)
                {
                    foreach (var seller in sellerList)
                    {
                        var sellerData = _dbContext.Seller.Where(a => a.Id == seller.Key).SingleOrDefault();
                        sellerData.AvailableToSupply = sellerData.AvailableToSupply - seller.Value;
                        _dbContext.Update(sellerData);
                    }
                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }
    }
}
