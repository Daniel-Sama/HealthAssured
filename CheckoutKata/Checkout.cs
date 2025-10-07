using CheckoutKata.Interface;

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

            try
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
            catch (Exception ex)
            {

                Console.WriteLine($"Error: {ex}");

            }
        }

        public int GetTotalPrice()
        {
            int totalPrice = 0;

            try
            {

                foreach (KeyValuePair<string, int> item in scannedItems)
                {
                    string sku = item.Key;
                    int count = item.Value;
                    PricingRule? rule = pricingRules.Where(r => r.SKU == sku).SingleOrDefault();

                    if (rule != null)
                    {

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
                    else
                    {

                        Console.WriteLine($"Cannot find {sku} in the pricing rule");

                    }

                }

            }
            catch(Exception ex)
            {

                Console.WriteLine($"Error: {ex}");

            }

            return totalPrice;
        }


    }
}
