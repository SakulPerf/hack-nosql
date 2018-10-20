using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletSampleApi.Models
{
    public class BuyResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get => IsSuccess ? string.Empty : "Symbol or User not found."; }
        public string Symbol { get; set; }
        /// <summary>
        /// ราคาตอนซื้อ
        /// </summary>
        public double BuyingRate { get; set; }
        /// <summary>
        /// ซื้อเมื่อไหร่
        /// </summary>
        public DateTime BuyingAt { get; set; }
        public double ReceivedCoins { get; set; }
    }
}
