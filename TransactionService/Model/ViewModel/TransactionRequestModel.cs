﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionService.Model.ViewModel
{
    public class TransactionRequestModel
    {
        public DateTime FromTransactionDate { get; set; }
        public DateTime ToTransactionDate { get; set; }
        public string CurrencyCode { get; set; }
        public string Status { get; set; }
    }
}