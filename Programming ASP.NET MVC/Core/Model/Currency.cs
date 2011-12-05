namespace Ebuy
{
    public class Currency
    {
        public float Amount { get; private set; }
        public string Code { get; private set; }

        public Currency(float amount, string code)
        {
            Amount = amount;
            Code = code;
        }
    }
}