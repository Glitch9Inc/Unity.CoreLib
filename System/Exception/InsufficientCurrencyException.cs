using System;

namespace Glitch9
{
    public class InsufficientCurrencyException : Exception
    {
        public string CurrencyName { get; }
        
        public InsufficientCurrencyException(string currencyName) : base($"Insufficient {currencyName}")
        {
            CurrencyName = currencyName;
        }
    }
}