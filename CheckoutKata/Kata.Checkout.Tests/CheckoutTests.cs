using System;
using System.Linq;
using Kata.Checkout.Logic;
using Kata.Checkout.Logic.Models;
using NUnit.Framework;

namespace Kata.Checkout.Tests
{
    public class CheckoutTests
    {
        private const string AppleSku = "A99";
        private const string BananaSku = "B15";
        private const string ChocolateSku = "C40";
        private const string OrangeSku = "O10";

        [SetUp]
        public void Setup()
        {

        }

        [TestCase(AppleSku)]
        [TestCase(BananaSku)]
        [TestCase(ChocolateSku)]
        public void CheckoutCanScanValidItem(string sku)
        {
            var checkout = new Logic.Checkout(FreeBagPolicy);

            checkout.Scan(sku);

            Assert.AreEqual(1, checkout.Transactions.Count);
            Assert.AreEqual(sku, checkout.Transactions.First()?.ItemSku);
        }

        [Test]
        public void CheckoutScanInValidItemThrowsArgumentError()
        {
            var checkout = new Logic.Checkout(FreeBagPolicy);

            const string invalidSku = "D32";

            Assert.Throws<ArgumentException>(() => checkout.Scan(invalidSku));
        }

        [Test]
        public void CheckoutReturnsCorrectTotalPriceForNoItems()
        {
            var checkout = new Logic.Checkout(FreeBagPolicy);

            const decimal anticipatedPrice = 0m;

            Assert.AreEqual(anticipatedPrice, checkout.TotalPrice);
        }

        [Test]
        public void CheckoutReturnsCorrectTotalPriceForTwoItems()
        {
            var checkout = new Logic.Checkout(FreeBagPolicy);

            checkout.Scan(AppleSku);
            checkout.Scan(ChocolateSku);

            const decimal anticipatedPrice = 1.1m;

            Assert.AreEqual(anticipatedPrice, checkout.TotalPrice);
        }

        [Test]
        public void ScanningThreeApplesAppliesOffer()
        {
            var checkout = new Logic.Checkout(FreeBagPolicy);

            checkout.Scan(AppleSku);
            checkout.Scan(AppleSku);
            checkout.Scan(AppleSku);

            const decimal anticipatedPrice = 1.3m;

            Assert.AreEqual(anticipatedPrice, checkout.TotalPrice);
        }

        [Test]
        public void ScanningTwoBananasAppliesOffer()
        {
            var checkout = new Logic.Checkout(FreeBagPolicy);

            checkout.Scan(BananaSku);
            checkout.Scan(BananaSku);

            const decimal anticipatedPrice = 0.45m;

            Assert.AreEqual(anticipatedPrice, checkout.TotalPrice);
        }

        [Test]
        public void ScanningFourBananasAppliesOfferTwice()
        {
            var checkout = new Logic.Checkout(FreeBagPolicy);

            checkout.Scan(BananaSku);
            checkout.Scan(BananaSku);
            checkout.Scan(BananaSku);
            checkout.Scan(BananaSku);

            const decimal anticipatedPrice = 0.9m;

            Assert.AreEqual(anticipatedPrice, checkout.TotalPrice);
        }

        [Test]
        public void ScanningFourOrangesAppliesOfferOnce()
        {
            var checkout = new Logic.Checkout(FreeBagPolicy);

            checkout.Scan(OrangeSku);
            checkout.Scan(OrangeSku);
            checkout.Scan(OrangeSku);
            checkout.Scan(OrangeSku);

            const decimal anticipatedPrice = 1.5m;

            Assert.AreEqual(anticipatedPrice, checkout.TotalPrice);
        }

        [Test]
        public void ScanningAssortmentOfItemsGivesCorrectTotalPriceWithOffers()
        {
            var checkout = new Logic.Checkout(FreeBagPolicy);

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

        [TestCase(1)]
        [TestCase(100)]
        public void MultiscanAddsCorrectAmount(int itemQuantity)
        {
            var checkout = new Logic.Checkout(FreeBagPolicy);
            
            checkout.Scan(AppleSku, itemQuantity);

            Assert.AreEqual(itemQuantity, checkout.Transactions.Count(t => t.TransactionType == TransactionType.ItemPurchased));
        }

        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(4, 1)]
        [TestCase(5, 2)]
        public void ScanningFourOrangesRequiresTwoBags(int items, int bagsExpected)
        {
            var checkout = new Logic.Checkout(Store.DefaultBagPolicy);

            checkout.Scan(OrangeSku, items);

            Assert.AreEqual(bagsExpected, checkout.BagsRequired);
        }

        [Test]
        public void ScanningAssortmentOfItemsGivesCorrectTotalPriceWithOffersAndBagPrice()
        {
            var checkout = new Logic.Checkout(Store.DefaultBagPolicy);

            checkout.Scan(AppleSku);
            checkout.Scan(BananaSku);
            checkout.Scan(BananaSku);
            checkout.Scan(ChocolateSku);
            checkout.Scan(AppleSku);
            checkout.Scan(AppleSku);
            checkout.Scan(AppleSku);
            checkout.Scan(BananaSku);

            const decimal anticipatedPrice = 3.25m;

            Assert.AreEqual(anticipatedPrice, checkout.TotalPrice);
        }

        private static IPlasticBagPolicy FreeBagPolicy => new PlasticBagPolicy(0, 0);
    }
}