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
            => this.repo = repo;

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
            => new ActionResult<IEnumerable<string>>(repo.GetWallets().Select(it => it.Username));

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<CustomerWallet> Get(string id)
        {
            var selectedAccount = repo.GetWallet(id);
            var isAccountExistring = selectedAccount != null;
            if (!isAccountExistring) return null;

            var symbolQry = selectedAccount.Coins.Select(it => it.Symbol).Distinct();
            var coinPriceQry = repo.GetCoinPrices(symbolQry.ToArray()).ToList();
            selectedAccount.Coins = selectedAccount.Coins.Select(it =>
            {
                var selectedCoinPrice = coinPriceQry.FirstOrDefault(c => c.Symbol == it.Symbol);
                it.USDValue = selectedCoinPrice == null ? 0 : it.Coins * selectedCoinPrice.Sell;
                return it;
            }).ToList();

            return new ActionResult<CustomerWallet>(selectedAccount);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] CoinPriceUpdate updateCoin)
            => updateCoin?.PriceList?.ForEach(it => repo.UpdateCoinPrice(it));

        [HttpPost("newcoin")]
        public void CreateNewCoin([FromBody] CoinPrice coin)
            => repo.CreateNewCoin(coin);

        [HttpGet("coinprices")]
        public IEnumerable<CoinPrice> GetCoinPrices()
            => repo.GetCoinPrices();

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
            var selectedAccount = repo.GetWallet(req.Username);
            var isAccountExistring = selectedAccount != null;
            if (!isAccountExistring) return null;

            var now = DateTime.Now;
            var selectedCoin = repo.GetCoinPrice(req.Symbol);
            var isSymbolExistring = selectedCoin != null;
            if (isSymbolExistring)
            {
                var receivedCoins = req.USDValue / selectedCoin.Buy;
                repo.AddBuyRecord(new BuyRecord
                {
                    Id = Guid.NewGuid().ToString(),
                    Username = req.Username,
                    BuyingAt = now,
                    BuyingRate = selectedCoin.Buy,
                    Symbol = req.Symbol,
                    ReceivedCoins = receivedCoins
                });

                var selectedAccountCoin = selectedAccount.Coins.FirstOrDefault(it => it.Symbol == req.Symbol);
                if (selectedAccountCoin == null)
                {
                    selectedAccount.Coins.Add(new CustomerCoin
                    {
                        BuyingAt = now,
                        BuyingRate = selectedCoin.Buy,
                        Symbol = req.Symbol,
                        Coins = receivedCoins
                    });
                }
                else
                {
                    selectedAccountCoin.BuyingAt = now;
                    selectedAccountCoin.BuyingRate = selectedCoin.Buy;
                    selectedAccountCoin.Coins += receivedCoins;
                }
                repo.UpdateCustomerWallet(selectedAccount);
            }

            return new BuyResponse
            {
                IsSuccess = isSymbolExistring,
                BuyingAt = now,
                Symbol = req.Symbol,
                ReceivedCoins = selectedCoin == null ? 0 : req.USDValue / selectedCoin.Buy,
                BuyingRate = selectedCoin?.Buy ?? 0
            };
        }
    }
}
