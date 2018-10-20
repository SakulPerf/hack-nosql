using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletSampleApi.Models
{
    public class BuyResponse
    {
        public bool IsSuccess { get; set; }
        public string Symbol { get; set; }
        public double USDValue { get; set; }
        public double Coins { get; set; }
        public string Message { get; set; }
    }
}
