using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WalletSampleApi.Models;

namespace WalletSampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HackController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "jdoe", "ptparker" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<CustomerWallet> Get(string id)
        {
            return new CustomerWallet
            {
                Username = "jdoe",
                Coins = new List<CustomerCoin>
                {
                    new CustomerCoin
                    {
                        Symbol = "BTC",
                        BuyingRate = 6565.25,
                        BuyingAt = new DateTime(2018, 10, 9, 9, 32, 23),
                        USDValue = 6500
                    },
                    new CustomerCoin
                    {
                        Symbol = "ETH",
                        BuyingRate = 203.47,
                        BuyingAt = new DateTime(2018, 9, 7, 12, 38, 33),
                        USDValue = 200.23
                    },
                },
            };
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] CoinPriceUpdate updateCoin)
        {
        }

        [HttpPost("login")]
        public LoginResponse Login([FromBody]LoginRequest req)
        {
            return new LoginResponse { IsSuccess = true };
        }

        [HttpPost("buy")]
        public BuyResponse Buy([FromBody]BuyRequest req)
        {
            return new BuyResponse { IsSuccess = true };
        }
    }
}
