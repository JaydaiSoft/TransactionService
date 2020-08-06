using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionServices.Model.ViewModel
{
    public class TransactionResponsModel
    {
        public List<TransactionModel> Results { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public DateTime ResponseDate { get; set; }
    }
}
