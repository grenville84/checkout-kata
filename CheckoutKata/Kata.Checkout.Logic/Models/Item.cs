namespace Kata.Checkout.Logic.Models
{
    public class Item : ITransactable, IProduct
    {
        public TransactionType TransactionType => TransactionType.ItemPurchased;
        public string Name { get; }
        public string ItemSku { get; }
        public decimal TransactionValue { get; }

        public Item(string sku, string name, decimal price)
        {
            Name = name;
            ItemSku = sku;
            TransactionValue = price;
        }
    }
}
