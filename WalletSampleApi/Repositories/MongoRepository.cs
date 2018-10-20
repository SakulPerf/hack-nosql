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

        public MongoRepository()
        {
            var conn = new MongoClient("mongodb://miolynet:passw0rd@ds050077.mlab.com:50077/hackathoncoins");
            db = conn.GetDatabase("hackathoncoins");
        }

        public IEnumerable<CustomerWallet> GetWallets()
            => CustomerWalletCollection.Find(it => true).ToList();

        public void CreateNewWallet(CustomerWallet wallet)
            => CustomerWalletCollection.InsertOne(wallet);

        public CustomerWallet GetWallet(string username)
            => CustomerWalletCollection
                .Find(it => it.Username.Equals(username, StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefault();
    }
}
