using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Ebuy
{
    [ComplexType]
    public class Currency
    {
        public static IDictionary<char, string> CurrencyCodesBySymbol = new Dictionary<char, string>() {
                    { '€', "EUR" },
                    { '£', "GBP" },
                    { '¥', "JPY" },
                    { '$', "USD" },
                };

        public string Code { get; private set; }
        public decimal Amount { get; private set; }


        private Currency()
        {
        }

        public Currency(string code, decimal amount)
        {
            Code = code;
            Amount = amount;
        }

        public Currency(string currency)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(currency));
            Contract.Requires(currency.Length > 1);

            Code = CurrencyCodesBySymbol[currency[0]];
            Amount = decimal.Parse(currency.Substring(1));
        }


        public override string ToString()
        {
            var symbol = CurrencyCodesBySymbol.Single(x => x.Value == Code).Key;
            return string.Format("{0}{1}", symbol, Amount);
        }

        public static implicit operator Currency(string currency)
        {
            return new Currency(currency);
        }
    }
}