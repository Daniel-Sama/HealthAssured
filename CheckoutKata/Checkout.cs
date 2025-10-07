using CheckoutKata.Exceptions;
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
        private List<PricingRule> pricingRules;
        private readonly Dictionary<string, int> scannedItems = new Dictionary<string, int>();

        public Checkout()
        {

        }

        public void SetPricingRule(List<PricingRule> pricingRules)
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

            int totalPrice = 0;

            foreach (KeyValuePair<string, int> item in scannedItems)
            {

                int count = item.Value;
                PricingRule? rule = pricingRules.Where(r => r.SKU == item.Key).SingleOrDefault();

                if (rule == null)
                {

                    throw new PricingRuleNotFoundException();

                }

                if (rule.SpecialQuantity.HasValue && rule.SpecialPrice.HasValue)
                {

                    int specialQuantity = count / rule.SpecialQuantity.Value;
                    int specialRemainder = count % rule.SpecialQuantity.Value;
                    totalPrice = totalPrice + (specialQuantity * rule.SpecialPrice.Value) + (specialRemainder * rule.UnitPrice);

                }
                else
                {

                    totalPrice = totalPrice + (count * rule.UnitPrice);

                }



            }

            return totalPrice;

        }


    }
}
