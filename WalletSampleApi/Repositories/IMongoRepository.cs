using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletSampleApi.Models;

namespace WalletSampleApi.Repositories
{
    public interface IMongoRepository
    {
        void CreateNewWallet(CustomerWallet wallet);
        CustomerWallet GetWallet(string username);
        IEnumerable<CustomerWallet> GetWallets();
        void UpdateCustomerWallet(CustomerWallet wallet);
        void AddBuyRecord(BuyRecord rec);
        IEnumerable<CoinPrice> GetCoinPrices();
        CoinPrice GetCoinPrice(string symbol);
        IEnumerable<CoinPrice> GetCoinPrices(params string[] symbols);
        void UpdateCoinPrice(CoinPrice update);
        void CreateNewCoin(CoinPrice data);
    }
}
