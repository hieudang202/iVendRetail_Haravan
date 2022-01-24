using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntergrationHaravan.Model
{
    public class InventoryItem
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal BasePrice { get; set; }
        public string Warehouse { get; set; }
        public int Quantity { get; set; }
    }
}
