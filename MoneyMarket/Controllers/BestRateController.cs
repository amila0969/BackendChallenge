using Microsoft.AspNetCore.Mvc;
using MoneyMarket.Services;
using System.Collections.Generic;
using System.Net.Http;

namespace MoneyMarket.Controllers
{
    [Produces("application/json")]
    [Route("api/bestrate")]
    public class BestRateController : Controller
    {
        /// <summary>
        /// This Endpoint answer for the Assignment Level 1
        /// </summary>
        /// <param name="units"></param>
        /// <returns></returns>
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

        /// <summary>
        /// This Endpoint answer for the Assignment Level 3
        /// </summary>
        /// <param name="units"></param>
        /// <returns></returns>
        [Route("{units}/getratewithfee")]
        [HttpGet]        
        public decimal GetBestRateWithTransactionFee(int units)
        {
            IDataService dataService = new MoneyMarketDataService();
            var sellerList = dataService.RetrieveSellerDataList();

            IExchangeRateGenarator exchangeRateGenarator = new RatesGenarator();
            var selectedSellerList = exchangeRateGenarator.RetreiveSupplierList(sellerList, units);
            var bestRate = exchangeRateGenarator.CalculateBestRateWithServiceFee(sellerList, units, selectedSellerList);
            return bestRate;
        }

        /// <summary>
        /// This Endpoint answer for the Assignment Level 2
        /// </summary>
        /// <param name="units"></param>
        /// <returns></returns>
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