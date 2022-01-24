using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntergrationHaravan.Model
{
    public class Customer
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string BillingAddressKey { get; set; }
        public string ShippingAddressKey { get; set; }
        public string Email { get; set; }
        public string LoyalityId { get; set; }
    }
}
