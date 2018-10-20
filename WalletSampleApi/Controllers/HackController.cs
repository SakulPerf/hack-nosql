using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WalletSampleApi.Models;
using WalletSampleApi.Repositories;

namespace WalletSampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HackController : ControllerBase
    {
        private readonly IMongoRepository repo;

        public HackController(IMongoRepository repo)
        {
            this.repo = repo;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
            => new ActionResult<IEnumerable<string>>(repo.GetWallets().Select(it => it.Username));

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<CustomerWallet> Get(string id)
        {
            //return new ActionResult<CustomerWallet>(repo.GetWallet(id));
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

        [HttpGet("coinprices")]
        public IEnumerable<CoinPrice> GetCoinPrices()
        {
            return new List<CoinPrice>
            {
                new CoinPrice
                {
                    Symbol = "BTC",
                    Buy =  6565.25,
                    Sell = 6000,
                },
                new CoinPrice
                {
                    Symbol = "ETH",
                    Buy =  203.47,
                    Sell = 200,
                },
            };
        }

        [HttpPost("register")]
        public CreateWalletResponse Create([FromBody]CreateWalletRequest req)
        {
            var isUsernameAlreadyExisting = repo.GetWallets().Any(it => it.Username == req.Username);
            if (!isUsernameAlreadyExisting)
            {
                repo.CreateNewWallet(new CustomerWallet
                {
                    Username = req.Username,
                    Coins = new List<CustomerCoin>()
                });
            }

            return new CreateWalletResponse { Username = req.Username, IsSuccess = !isUsernameAlreadyExisting };
        }

        [HttpPost("login")]
        public LoginResponse Login([FromBody]LoginRequest req)
        {
            var isSuccess = repo.GetWallets().Any(it => it.Username == req.Username);
            return new LoginResponse { IsSuccess = isSuccess };
        }

        [HttpPost("buy")]
        public BuyResponse Buy([FromBody]BuyRequest req)
        {
            return new BuyResponse { IsSuccess = true };
        }
    }
}
