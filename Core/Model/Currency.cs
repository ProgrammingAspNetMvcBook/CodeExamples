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
        public decimal Value { get; private set; }


        public Currency(string code, decimal value)
        {
            Code = code;
            Value = value;
        }

        public Currency(string currency)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(currency));
            Contract.Requires(currency.Length > 1);

            Code = CurrencyCodesBySymbol[currency[0]];
            Value = decimal.Parse(currency.Substring(1));
        }

        private Currency()
        {
        }


        public bool Equals(Currency other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Code, Code) && other.Value == Value;
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
            return string.Format("{0}{1}", symbol, Value);
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

        public static implicit operator string(Currency currency)
        {
            return currency.ToString();
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Code != null ? Code.GetHashCode() : 0)*397) ^ Value.GetHashCode();
            }
        }
    }
}