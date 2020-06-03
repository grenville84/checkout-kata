using System;
using System.Collections.Generic;
using System.Linq;
using Kata.Checkout.Logic.Models;

namespace Kata.Checkout.Logic
{
    public class Checkout
    {
        private readonly IPlasticBagPolicy _plasticBagPolicy;
        private readonly List<ITransactable> _transactionItems;

        public IList<ITransactable> Transactions => _transactionItems.ToList().AsReadOnly();

        public decimal TotalPrice
        {
            get
            {
                decimal bagPrice = BagsRequired * _plasticBagPolicy.CostPerBag;

                return _transactionItems.Sum(t => t.TransactionValue) + bagPrice;
            }
        }

        public Checkout(IPlasticBagPolicy plasticBagPolicy)
        {
            _plasticBagPolicy = plasticBagPolicy;
            _transactionItems = new List<ITransactable>();
        }

        public void Scan(string sku)
        {
            // lookup item and add to transaction

            if (!TryFindItemBySku(sku, out ITransactable scannedItem))
            {
                throw new ArgumentException($"{sku} is not a valid item SKU.", nameof(sku));
            }

            _transactionItems.Add(scannedItem);

            // check if any discounts are triggered by this purchase and add as transaction if so

            if (TryFindOfferByItemSku(scannedItem.ItemSku, _transactionItems, out ITransactable applicableOffer))
            {
                _transactionItems.Add(applicableOffer);
            }
        }

        public void Scan(string sku, int countOfItem)
        {
            for (int i = 0; i < countOfItem; i++)
            {
                Scan(sku);
            }
        }

        public int BagsRequired
        {
            get
            {
                int numberOfItems = _transactionItems.Count(t => t.TransactionType == TransactionType.ItemPurchased);


                if (numberOfItems == 0 || _plasticBagPolicy.MaxItemsPerBag == 0)
                {
                    return 0;
                }

                decimal bagsRequired = Math.Ceiling(numberOfItems / (decimal)_plasticBagPolicy.MaxItemsPerBag);

                return (int) bagsRequired;
            }
        }

        private static bool TryFindItemBySku(string sku, out ITransactable foundItem)
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

        private static bool TryFindOfferByItemSku(string sku, IEnumerable<ITransactable> transactions, out ITransactable foundOffer)
        {
            var itemOffer = Store.AvailableOffers.FirstOrDefault(i => i.ItemSku == sku);

            if (itemOffer != null)
            {
                int itemTransactionCount =
                    transactions.Count(t => t.TransactionType == TransactionType.ItemPurchased && t.ItemSku == sku);

                bool offerAppliesAtCurrentPurchaseCount = itemTransactionCount % itemOffer.TriggerAtItemCount == 0;

                if (offerAppliesAtCurrentPurchaseCount)
                {
                    foundOffer = itemOffer;
                    return true;
                }
            }

            foundOffer = null;
            return false;
        }
    }
}
