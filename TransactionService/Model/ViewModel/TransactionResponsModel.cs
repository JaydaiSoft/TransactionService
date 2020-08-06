using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionService.Model.ViewModel
{
    public class TransactionResponsModel
    {
        public List<TransactionModel> Results { get; set; }
        public string Message { get; set; }
        public DateTime ResponseDate { get; set; }
    }
}
