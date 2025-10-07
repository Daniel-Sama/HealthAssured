using CheckoutKata.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata.UnitTest
{
    public class CheckoutTest
    {

        private readonly ICheckout checkout;

        public CheckoutTest()
        {

            ServiceCollection services = new ServiceCollection();
            services.AddTransient<ICheckout, Checkout>();

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            checkout = serviceProvider.GetRequiredService<ICheckout>();

        }

        [Fact]
        public void Should_Pass_GetTotalPrice_With_Single_Item_No_Special_Price()
        {

            List<PricingRule> pricingRules = new List<PricingRule>()
            {
                new PricingRule { SKU = "A", UnitPrice = 50, SpecialQuantity = 3, SpecialPrice = 130 },
                new PricingRule { SKU = "B", UnitPrice = 30, SpecialQuantity = 2, SpecialPrice = 45 },
                new PricingRule { SKU = "C", UnitPrice = 20 },
                new PricingRule { SKU = "D", UnitPrice = 15 },
            };

            checkout.SetPricingRule(pricingRules);
            checkout.Scan("C");

            int totalPrice = checkout.GetTotalPrice();

            Assert.Equal(20, totalPrice);

        }

        [Fact]
        public void Should_Pass_GetTotalPrice_With_Multiple_Items_No_Special_Price()
        {

            List<PricingRule> pricingRules = new List<PricingRule>()
            {
                new PricingRule { SKU = "A", UnitPrice = 50, SpecialQuantity = 3, SpecialPrice = 130 },
                new PricingRule { SKU = "B", UnitPrice = 30, SpecialQuantity = 2, SpecialPrice = 45 },
                new PricingRule { SKU = "C", UnitPrice = 20 },
                new PricingRule { SKU = "D", UnitPrice = 15 },
            };

            checkout.SetPricingRule(pricingRules);
            checkout.Scan("C");
            checkout.Scan("D");

            int totalPrice = checkout.GetTotalPrice();

            Assert.Equal(35, totalPrice);

        }

        [Fact]
        public void Should_Pass_GetTotalPrice_With_Special_Price_And_No_Remainders()
        {

            List<PricingRule> pricingRules = new List<PricingRule>()
            {
                new PricingRule { SKU = "A", UnitPrice = 50, SpecialQuantity = 3, SpecialPrice = 130 },
                new PricingRule { SKU = "B", UnitPrice = 30, SpecialQuantity = 2, SpecialPrice = 45 },
                new PricingRule { SKU = "C", UnitPrice = 20 },
                new PricingRule { SKU = "D", UnitPrice = 15 },
            };

            checkout.SetPricingRule(pricingRules);
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");

            int totalPrice = checkout.GetTotalPrice();

            Assert.Equal(130, totalPrice);

        }

        [Fact]
        public void Should_Pass_GetTotalPrice_With_Multiple_Special_Price_And_No_Remainders()
        {

            List<PricingRule> pricingRules = new List<PricingRule>()
            {
                new PricingRule { SKU = "A", UnitPrice = 50, SpecialQuantity = 3, SpecialPrice = 130 },
                new PricingRule { SKU = "B", UnitPrice = 30, SpecialQuantity = 2, SpecialPrice = 45 },
                new PricingRule { SKU = "C", UnitPrice = 20 },
                new PricingRule { SKU = "D", UnitPrice = 15 },
            };

            checkout.SetPricingRule(pricingRules);
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("B");

            int totalPrice = checkout.GetTotalPrice();

            Assert.Equal(175, totalPrice);

        }

        [Fact]
        public void Should_Pass_GetTotalPrice_With_Special_Price_And_Remainders()
        {

            List<PricingRule> pricingRules = new List<PricingRule>()
            {
                new PricingRule { SKU = "A", UnitPrice = 50, SpecialQuantity = 3, SpecialPrice = 130 },
                new PricingRule { SKU = "B", UnitPrice = 30, SpecialQuantity = 2, SpecialPrice = 45 },
                new PricingRule { SKU = "C", UnitPrice = 20 },
                new PricingRule { SKU = "D", UnitPrice = 15 },
            };

            checkout.SetPricingRule(pricingRules);
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");


            int totalPrice = checkout.GetTotalPrice();

            Assert.Equal(180, totalPrice);

        }

        [Fact]
        public void Should_Pass_GetTotalPrice_With_Multiple_Special_Price_And_Remainders()
        {

            List<PricingRule> pricingRules = new List<PricingRule>()
            {
                new PricingRule { SKU = "A", UnitPrice = 50, SpecialQuantity = 3, SpecialPrice = 130 },
                new PricingRule { SKU = "B", UnitPrice = 30, SpecialQuantity = 2, SpecialPrice = 45 },
                new PricingRule { SKU = "C", UnitPrice = 20 },
                new PricingRule { SKU = "D", UnitPrice = 15 },
            };

            checkout.SetPricingRule(pricingRules);
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("B");
            checkout.Scan("B");


            int totalPrice = checkout.GetTotalPrice();

            Assert.Equal(255, totalPrice);

        }

        [Fact]
        public void Should_Return_Zero_GetTotalPrice_With_No_Items()
        {

            List<PricingRule> pricingRules = new List<PricingRule>()
            {
                new PricingRule { SKU = "A", UnitPrice = 50, SpecialQuantity = 3, SpecialPrice = 130 },
                new PricingRule { SKU = "B", UnitPrice = 30, SpecialQuantity = 2, SpecialPrice = 45 },
                new PricingRule { SKU = "C", UnitPrice = 20 },
                new PricingRule { SKU = "D", UnitPrice = 15 },
            };

            checkout.SetPricingRule(pricingRules);
            checkout.Scan("");

            int totalPrice = checkout.GetTotalPrice();

            Assert.Equal(0, totalPrice);

        }

        [Fact]
        public void Should_Fail_GetTotalPrice_With_No_Pricing_Rule()
        {

            List<PricingRule> pricingRules = new List<PricingRule>()
            {
                new PricingRule { SKU = "A", UnitPrice = 50, SpecialQuantity = 3, SpecialPrice = 130 },
                new PricingRule { SKU = "B", UnitPrice = 30, SpecialQuantity = 2, SpecialPrice = 45 },
                new PricingRule { SKU = "C", UnitPrice = 20 },
                new PricingRule { SKU = "D", UnitPrice = 15 },
            };

            checkout.SetPricingRule(pricingRules);
            checkout.Scan("");

            int totalPrice = checkout.GetTotalPrice();

            Assert.Equal(0, totalPrice);

        }

        [Fact]
        public void Should_Fail_GetTotalPrice_With_Unrecognised_Pricing_Rule()
        {

            List<PricingRule> pricingRules = new List<PricingRule>()
            {
                new PricingRule { SKU = "A", UnitPrice = 50, SpecialQuantity = 3, SpecialPrice = 130 },
                new PricingRule { SKU = "B", UnitPrice = 30, SpecialQuantity = 2, SpecialPrice = 45 },
                new PricingRule { SKU = "C", UnitPrice = 20 },
                new PricingRule { SKU = "D", UnitPrice = 15 },
            };

            checkout.SetPricingRule(pricingRules);
            checkout.Scan("");

            int totalPrice = checkout.GetTotalPrice();

            Assert.Equal(0, totalPrice);

        }

    }
}
