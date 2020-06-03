namespace Kata.Checkout.Logic.Models
{
    public class Offer : ITransactable
    {
        public int TriggerAtItemCount { get; }

        public Offer(string itemSku, decimal price, int triggerAtItemCount)
        {
            ItemSku = itemSku;
            TransactionValue = price;
            TriggerAtItemCount = triggerAtItemCount;
        }

        public TransactionType TransactionType => TransactionType.DiscountApplied;
        public string ItemSku { get; }
        public decimal TransactionValue { get; }
    }
}
