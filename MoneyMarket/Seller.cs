using System;
using System.Collections.Generic;

namespace MoneyMarket
{
    public partial class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AvailableToSupply { get; set; }
        public decimal Rate { get; set; }
        public int TransactionFee { get; set; }
    }
}
