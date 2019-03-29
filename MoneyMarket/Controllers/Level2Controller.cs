using Microsoft.AspNetCore.Mvc;

namespace MoneyMarket.Controllers
{
    [Produces("application/json")]
    [Route("api/Level2")]
    public class Level2Controller : Controller
    {

        [HttpPost]
        public void CreateSeller(Seller seller)
        {

        }

        [HttpPut]
        public void UpdateQty()
        {

        }

    }
}