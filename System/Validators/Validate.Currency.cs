using System;

namespace Glitch9
{
    public partial class Validate
    {
        public static class Currency
        {
            private const string k_DefaultCurrencyName = "Funds";
            public static void HasEnough(float balance, float amount, string currencyName = k_DefaultCurrencyName)
            {
                if (balance < amount)
                {
                    string message = $"Insufficient {currencyName.ToLower()}.";
                    throw new InvalidOperationException(message);
                }
            }
        }
    }
}