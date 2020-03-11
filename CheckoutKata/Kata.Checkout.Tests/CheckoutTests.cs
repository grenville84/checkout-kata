using System.Linq;
using NUnit.Framework;

namespace Kata.Checkout.Tests
{
    public class CheckoutTests
    {
        private const string AppleSku = "A99";
        private const string BananaSku = "B15";
        private const string ChocolateSku = "C40";

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        [TestCase(AppleSku)]
        [TestCase(BananaSku)]
        [TestCase(ChocolateSku)]
        public void CheckoutCanScanValidItem(string sku)
        {
            var checkout = new Logic.Checkout();

            checkout.Scan(sku);

            Assert.AreEqual(1, checkout.Transactions.Count);
            Assert.AreEqual(sku, checkout.Transactions.First()?.Sku);
        }

        [Test]
        public void CheckoutReturnsCorrectTotalPriceForNoItems()
        {
            var checkout = new Logic.Checkout();

            const decimal anticipatedPrice = 0m;

            Assert.AreEqual(anticipatedPrice, checkout.TotalPrice);
        }

        [Test]
        public void CheckoutReturnsCorrectTotalPriceForTwoItems()
        {
            var checkout = new Logic.Checkout();

            checkout.Scan(AppleSku);
            checkout.Scan(ChocolateSku);

            const decimal anticipatedPrice = 1.1m;

            Assert.AreEqual(anticipatedPrice, checkout.TotalPrice);
        }
    }
}