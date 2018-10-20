﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletSampleApi.Models
{
    public class LoginResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get => IsSuccess ? string.Empty : "User not found"; }
    }
}
