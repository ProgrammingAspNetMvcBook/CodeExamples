using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

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

        public string Code { get; set; }
        public decimal Amount { get; set; }


        public Currency()
        {
        }

        public Currency(string currency)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(currency));
            Contract.Requires(currency.Length > 1);

            Code = CurrencyCodesBySymbol[currency[0]];
            Amount = decimal.Parse(currency.Substring(1));
        }


        public static implicit operator Currency(string currency)
        {
            return new Currency(currency);
        }
    }
}