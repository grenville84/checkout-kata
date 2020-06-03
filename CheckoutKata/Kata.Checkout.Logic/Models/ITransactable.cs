namespace Kata.Checkout.Logic.Models
{
    public interface ITransactable : ISkuIdentifier
    {
        TransactionType TransactionType { get; }
        decimal TransactionValue { get; }
    }

    public interface IProduct : ISkuIdentifier
    {
        string Name { get; }
    }

    public interface ISkuIdentifier
    {
        string ItemSku { get; }
    }

    public enum TransactionType
    {
        ItemPurchased,
        DiscountApplied
    }
}
