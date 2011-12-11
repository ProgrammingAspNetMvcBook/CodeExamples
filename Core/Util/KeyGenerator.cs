using System;
using System.Diagnostics.Contracts;

namespace Ebuy
{
    public static class KeyGenerator
    {
        public static string Generate()
        {
            return Guid.NewGuid().ToString("D");
        }

        public static string Generate(string input)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(input));

            return input.Replace(" ", "_");
        }
    }
}
