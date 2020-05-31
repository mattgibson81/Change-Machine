namespace Change_Machine
{
    internal class Denomination
    {
        internal readonly decimal Amount;
        internal int Quantity;

        internal Denomination(decimal amount)
        {
            Amount = amount;
        }

        internal decimal Total
        {
            get { return (Amount * Quantity); }
        }
    }
}