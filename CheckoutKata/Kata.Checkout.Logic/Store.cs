using System.Collections.Generic;
using Kata.Checkout.Logic.Models;

namespace Kata.Checkout.Logic
{
    public static class Store
    {
        public static IList<Item> AvailableItems = new List<Item>
        {
            new Item("A99", "Apple", 0.5m),
            new Item("B15", "Banana", 0.3m),
            new Item("C40", "Chocolate", 0.6m)
        };

        public static IList<Offer> AvailableOffers = new List<Offer>
        {
            new Offer("A99", -0.2m, 3),
            new Offer("B15", -0.15m, 2)
        };
    }
}
