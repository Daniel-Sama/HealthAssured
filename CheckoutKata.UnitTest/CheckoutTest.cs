using CheckoutKata.Interface;
using Microsoft.Extensions.DependencyInjection;

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

            List<PricingRule> pricingRules = new List<PricingRule>()
            {
                new PricingRule { SKU = "A", UnitPrice = 50, SpecialQuantity = 3, SpecialPrice = 130 },
                new PricingRule { SKU = "B", UnitPrice = 30, SpecialQuantity = 2, SpecialPrice = 45 },
                new PricingRule { SKU = "C", UnitPrice = 20 },
                new PricingRule { SKU = "D", UnitPrice = 15 },
            };

            checkout.SetPricingRule(pricingRules);

        }

        [Fact]
        public void Should_Pass_GetTotalPrice_With_Single_Item_No_Special_Price()
        {

            checkout.Scan("C");

            int totalPrice = checkout.GetTotalPrice();

            Assert.Equal(20, totalPrice);

        }

        [Fact]
        public void Should_Pass_GetTotalPrice_With_Multiple_Items_No_Special_Price()
        {

            checkout.Scan("C");
            checkout.Scan("D");

            int totalPrice = checkout.GetTotalPrice();

            Assert.Equal(35, totalPrice);

        }

        [Fact]
        public void Should_Pass_GetTotalPrice_With_Special_Price_And_No_Remainders()
        {

            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");

            int totalPrice = checkout.GetTotalPrice();

            Assert.Equal(130, totalPrice);

        }

        [Fact]
        public void Should_Pass_GetTotalPrice_With_Multiple_Special_Price_And_No_Remainders()
        {

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
        public void Should_Pass_GetTotalPrice_With_Multiple_Special_Price_And_Remainders_In_Any_Order()
        {

            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("C");
            checkout.Scan("D");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("B");

            int totalPrice = checkout.GetTotalPrice();

            Assert.Equal(340, totalPrice);

        }

        [Fact]
        public void Should_Pass_GetTotalPrice_With_Unrecognised_And_Recognised_Price()
        {

            checkout.Scan("A");
            checkout.Scan("Z");
            checkout.Scan("D");

            int totalPrice = checkout.GetTotalPrice();

            Assert.Equal(65, totalPrice);

        }


        [Fact]
        public void Should_Return_Zero_GetTotalPrice_With_No_Items()
        {

            checkout.Scan("E");

            int totalPrice = checkout.GetTotalPrice();

            Assert.Equal(0, totalPrice);

        }


    }
}
