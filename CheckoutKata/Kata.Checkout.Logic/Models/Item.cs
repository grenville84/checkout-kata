namespace Kata.Checkout.Logic.Models
{
    public class Item
    {
        public string Sku { get; }
        public string Name { get; }
        public decimal Price { get; }

        public Item(string sku, string name, decimal price)
        {
            Sku = sku;
            Name = name;
            Price = price;
        }
    }
}
