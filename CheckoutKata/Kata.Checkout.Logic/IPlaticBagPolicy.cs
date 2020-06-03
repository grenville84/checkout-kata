namespace Kata.Checkout.Logic
{
    public interface IPlasticBagPolicy
    {
        short MaxItemsPerBag { get; }
        decimal CostPerBag { get; }
    }

    public struct PlasticBagPolicy : IPlasticBagPolicy
    {

        public short MaxItemsPerBag { get; }
        public decimal CostPerBag { get; }

        public PlasticBagPolicy(short maxItemsPerBag, decimal costPerBag)
        {
            MaxItemsPerBag = maxItemsPerBag;
            CostPerBag = costPerBag;
        }
    }
}
