namespace Change_Machine
{
    internal class CalculateChangeUSD : CalculateChange
    {
        public CalculateChangeUSD() : base()
        {
            _currencySymbol = "$";
            //Only the most frequently used denominations
            _denominations.Add(new Denomination(new decimal(1)));
            _denominations.Add(new Denomination(new decimal(5)));
            _denominations.Add(new Denomination(new decimal(10)));
            _denominations.Add(new Denomination(new decimal(20)));
            _denominations.Add(new Denomination(new decimal(50)));
            _denominations.Add(new Denomination(new decimal(100)));
            _denominations.Add(new Denomination(new decimal(0.05)));
            _denominations.Add(new Denomination(new decimal(0.01)));
            _denominations.Add(new Denomination(new decimal(0.1)));
            _denominations.Add(new Denomination(new decimal(0.25)));
        }
    }
}