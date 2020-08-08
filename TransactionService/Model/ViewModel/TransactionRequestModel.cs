using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionServices.Model.ViewModel
{
    public class TransactionRequestModel
    {
        public TransactionFilter TransactionFilter { get; set; }
        public List<TransactionPayload> TransactionPayloads { get; set; }
    }

    public class TransactionPayload
    {
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Status { get; set; }
    }

    public class TransactionFilter
    {
        public string CurrencyCode { get; set; }
        public string TransactionStatus { get; set; }
        public DateTime TransDateFrom { get; set; }
        public DateTime TransDateTO { get; set; }
    }
}
