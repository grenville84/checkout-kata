using System.Linq;
using NUnit.Framework;

namespace Kata.Checkout.Tests
{
    public class CheckoutTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        [TestCase("A99")]
        [TestCase("B15")]
        [TestCase("C40")]
        public void CheckoutCanScanValidItem(string sku)
        {
            var checkout = new Logic.Checkout();

            checkout.Scan(sku);

            Assert.AreEqual(1, checkout.Transactions.Count);
            Assert.AreEqual(sku, checkout.Transactions.First()?.Sku);
        }
    }
}