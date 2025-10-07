using CheckoutKata.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata
{
    public sealed class Checkout : ICheckout
    {
        private readonly List<PricingRule> pricingRules;
        private readonly Dictionary<string, int> scannedItems = new Dictionary<string, int>();

        public Checkout(List<PricingRule> pricingRules)
        {

            this.pricingRules = pricingRules;

        }

        public void Scan(string item)
        {

            if (scannedItems.ContainsKey(item))
            {
                scannedItems[item] = scannedItems[item] + 1;
            }
            else
            {
                scannedItems[item] = 1;
            }

        }

        public int GetTotalPrice()
        {

            return 0;

        }


    }
}
