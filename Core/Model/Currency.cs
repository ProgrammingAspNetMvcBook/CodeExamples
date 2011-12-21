using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Ebuy
{
    [ComplexType]
    public class Currency : IEquatable<Currency>
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


        public bool Equals(Currency other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Code, Code) && other.Amount == Amount;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Currency)) return false;
            return Equals((Currency) obj);
        }

        public override string ToString()
        {
            var symbol = CurrencyCodesBySymbol.Single(x => x.Value == Code).Key;
            return string.Format("{0}{1}", symbol, Amount);
        }

        public static bool operator ==(Currency x, Currency y)
        {
            if(x == null || y == null) return false;
            return x.Equals(y);
        }

        public static bool operator !=(Currency x, Currency y)
        {
            if (x == null || y == null) return true;
            return !x.Equals(y);
        }

        public static implicit operator Currency(string currency)
        {
            return new Currency(currency);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Code != null ? Code.GetHashCode() : 0)*397) ^ Amount.GetHashCode();
            }
        }
    }
}