using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletSampleApi.Models
{
    public class BuyRequest
    {
        public string Username { get; set; }
        public string Symbol { get; set; }
        public double USDValue { get; set; }
    }
}
