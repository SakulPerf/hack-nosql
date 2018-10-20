using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletSampleApi.Models
{
    public class CoinPriceUpdate
    {
        public DateTime At { get; set; }
        public List<CoinPrice> PriceList { get; set; }
    }

    public class CoinPrice
    {
        [BsonId]
        public string Symbol { get; set; }
        public double Buy { get; set; }
        public double Sell { get; set; }
    }
}
