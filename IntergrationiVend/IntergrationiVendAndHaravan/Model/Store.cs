using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntergrationHaravan.Model
{
    public class Store
    {
        public string StoreId { get; set; }
        public string StoreName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public decimal PrurchaseTaxCode { get; set; }
        public decimal SaleTaxCode { get; set; }
        public string PriceList { get; set; }
    }
}
