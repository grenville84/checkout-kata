namespace Kata.Checkout.Logic.Models
{
    public class Offer : Transaction
    {
        public int TriggerAtItemCount { get; }

        public Offer(string itemSku, decimal price, int triggerAtItemCount): base(TransactionType.DiscountApplied, itemSku, price)
        {
            TriggerAtItemCount = triggerAtItemCount;
        }
    }
}
