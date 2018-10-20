using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletSampleApi.Models
{
    public class CreateWalletResponse
    {
        public string Username { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get => IsSuccess ? string.Empty : "Username is already existring"; }
    }
}
