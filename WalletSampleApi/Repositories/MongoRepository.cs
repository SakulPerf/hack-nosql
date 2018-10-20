using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletSampleApi.Models;

namespace WalletSampleApi.Repositories
{
    public class MongoRepository : IMongoRepository
    {
        private IMongoDatabase db;

        public IMongoCollection<CustomerWallet> CustomerWalletCollection
        {
            get => db.GetCollection<CustomerWallet>(nameof(CustomerWallet));
        }

        public IMongoCollection<BuyRecord> BuyRecordCollection
        {
            get => db.GetCollection<BuyRecord>(nameof(BuyRecord));
        }

        public IMongoCollection<CoinPrice> CoinPriceCollection
        {
            get => db.GetCollection<CoinPrice>(nameof(CoinPrice));
        }

        public MongoRepository()
        {
            var conn = new MongoClient("mongodb://miolynet:passw0rd@ds050077.mlab.com:50077/hackathoncoins");
            db = conn.GetDatabase("hackathoncoins");
        }

        public IEnumerable<CustomerWallet> GetWallets()
            => CustomerWalletCollection.Find(it => true)
                .ToEnumerable();

        public void UpdateCustomerWallet(CustomerWallet wallet)
        {
            var updateDefinition = new UpdateDefinitionBuilder<CustomerWallet>()
                .Set(it => it.Coins, wallet.Coins);
            CustomerWalletCollection.UpdateOne(it => it.Username == wallet.Username, updateDefinition);
        }

        public void CreateNewWallet(CustomerWallet wallet)
            => CustomerWalletCollection.InsertOne(wallet);

        public CustomerWallet GetWallet(string username)
            => CustomerWalletCollection
                .Find(it => it.Username == username)
                .FirstOrDefault();

        public void AddBuyRecord(BuyRecord rec)
            => BuyRecordCollection.InsertOne(rec);

        public IEnumerable<CoinPrice> GetCoinPrices()
            => CoinPriceCollection.Find(it => true)
                .ToEnumerable();

        public CoinPrice GetCoinPrice(string symbol)
            => CoinPriceCollection
                .Find(it => it.Symbol == symbol)
                .FirstOrDefault();

        public IEnumerable<CoinPrice> GetCoinPrices(params string[] symbols)
            => CoinPriceCollection
                .Find(it => symbols.Contains(it.Symbol))
                .ToEnumerable();

        public void UpdateCoinPrice(CoinPrice update)
        {
            var updateDefinition = new UpdateDefinitionBuilder<CoinPrice>()
                .Set(it => it.Buy, update.Buy)
                .Set(it => it.Sell, update.Sell);
            CoinPriceCollection.UpdateOne(it => it.Symbol == update.Symbol, updateDefinition);
        }

        public void CreateNewCoin(CoinPrice data)
            => CoinPriceCollection.InsertOne(data);
    }
}
