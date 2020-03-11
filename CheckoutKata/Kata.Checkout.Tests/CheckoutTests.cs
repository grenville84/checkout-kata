using System;
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

        [TestCase(AppleSku)]
        [TestCase(BananaSku)]
        [TestCase(ChocolateSku)]
        public void CheckoutCanScanValidItem(string sku)
        {
            var checkout = new Logic.Checkout();

            checkout.Scan(sku);

            Assert.AreEqual(1, checkout.Transactions.Count);
            Assert.AreEqual(sku, checkout.Transactions.First()?.ItemSku);
        }

        [TestCase(AppleSku)]
        [TestCase(BananaSku)]
        [TestCase(ChocolateSku)]
        public void CheckoutScanInValidItemThrowsArgumentError(string sku)
        {
            var checkout = new Logic.Checkout();

            const string invalidSku = "D32";

            Assert.Throws<ArgumentException>(() => checkout.Scan(invalidSku));
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

        [Test]
        public void ScanningThreeApplesAppliesOffer()
        {
            var checkout = new Logic.Checkout();

            checkout.Scan(AppleSku);
            checkout.Scan(AppleSku);
            checkout.Scan(AppleSku);

            const decimal anticipatedPrice = 1.3m;

            Assert.AreEqual(anticipatedPrice, checkout.TotalPrice);
        }

        [Test]
        public void ScanningTwoBananasAppliesOffer()
        {
            var checkout = new Logic.Checkout();

            checkout.Scan(BananaSku);
            checkout.Scan(BananaSku);

            const decimal anticipatedPrice = 0.45m;

            Assert.AreEqual(anticipatedPrice, checkout.TotalPrice);
        }

        [Test]
        public void ScanningFourBananasAppliesOfferTwice()
        {
            var checkout = new Logic.Checkout();

            checkout.Scan(BananaSku);
            checkout.Scan(BananaSku);
            checkout.Scan(BananaSku);
            checkout.Scan(BananaSku);

            const decimal anticipatedPrice = 0.9m;

            Assert.AreEqual(anticipatedPrice, checkout.TotalPrice);
        }

        [Test]
        public void ScanningAssortmentOfItemsGivesCorrectTotalPriceWithOffers()
        {
            var checkout = new Logic.Checkout();

            checkout.Scan(AppleSku);
            checkout.Scan(BananaSku);
            checkout.Scan(BananaSku);
            checkout.Scan(ChocolateSku);
            checkout.Scan(AppleSku);
            checkout.Scan(AppleSku);
            checkout.Scan(AppleSku);
            checkout.Scan(BananaSku);

            const decimal anticipatedPrice = 3.15m;

            Assert.AreEqual(anticipatedPrice, checkout.TotalPrice);
        }
    }
}