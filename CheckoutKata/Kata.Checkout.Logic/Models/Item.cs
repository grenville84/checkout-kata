namespace Kata.Checkout.Logic.Models
{
    public class Item : Transaction
    {
        public string Name { get; }

        public Item(string sku, string name, decimal price) : base(TransactionType.ItemPurchased, sku, price)
        {
            Name = name;
        }
    }
}
