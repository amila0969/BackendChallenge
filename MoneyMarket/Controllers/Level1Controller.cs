using Microsoft.AspNetCore.Mvc;
using MoneyMarket.Services;
using System.Collections.Generic;
using System.Net.Http;

namespace MoneyMarket.Controllers
{
    [Produces("application/json")]
    [Route("api/bestrate")]
    public class Level1Controller: Controller
    {
        [Route("{units}/getrate")]
        [HttpGet]
        public decimal GetBestRate(int units)
        {
            IDataService dataService = new MoneyMarketDataService();
            var sellerList = dataService.RetrieveSellerDataList();

            IExchangeRateGenarator exchangeRateGenarator = new RatesGenarator();
            var bestRate = exchangeRateGenarator.CalculateBestRate(sellerList, units);
            return bestRate;
        }


        [Route("{units}/getratewithfee")]
        [HttpGet]        
        public decimal GetBestRateWithTransactionFee(int units)
        {
            return 0;
        }

        [HttpPut]
        public HttpResponseMessage UpdateQty(int units)
        {
            IDataService dataService = new MoneyMarketDataService();
            var sellerList = dataService.RetrieveSellerDataList();

            IExchangeRateGenarator exchangeRateGenarator = new RatesGenarator();
            var sellerDataList = exchangeRateGenarator.RetreiveSupplierList(sellerList, units);
            
            if (dataService.UpdateAvailableQty(sellerDataList))
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK); 
            }
            else
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }
    }
}