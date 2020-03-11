using System;
using System.Collections.Generic;
using System.Linq;
using Kata.Checkout.Logic.Models;

namespace Kata.Checkout.Logic
{
    public class Checkout
    {
        private readonly List<Item> _transactionItems;

        public IList<Item> Transactions => _transactionItems.ToList().AsReadOnly();

        public decimal TotalPrice => _transactionItems.Sum(t => t.Price);

        public Checkout()
        {
            _transactionItems = new List<Item>();
        }

        public void Scan(string sku)
        {
            if (!TryFindItemBySku(sku, out Item scannedItem))
                throw new ArgumentException($"{sku} is not a valid item SKU.", nameof(sku));

            _transactionItems.Add(scannedItem);
        }

        private static bool TryFindItemBySku(string sku, out Item foundItem)
        {
            var searchItem = Store.AvailableItems.FirstOrDefault(i => i.ItemSku == sku);

            if (searchItem == null)
            {
                foundItem = null;
                return false;
            }

            foundItem = searchItem;
            return true;
        }
    }
}
