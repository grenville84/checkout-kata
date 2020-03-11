namespace Kata.Checkout.Logic.Models
{
    public abstract class Transaction
    {
        public TransactionType Type { get; }
        public string Sku { get; }
        public decimal Price { get; }

        protected Transaction(TransactionType type, string sku, decimal price)
        {
            Type = type;
            Sku = sku;
            Price = price;
        }
    }

    public enum TransactionType
    {
        ItemPurchased
    }
}
