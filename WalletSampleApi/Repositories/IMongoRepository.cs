﻿using System;
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
    }
}