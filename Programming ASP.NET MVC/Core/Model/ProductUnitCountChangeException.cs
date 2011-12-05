using System;

namespace Ebuy
{
    public class ProductUnitCountChangeException : Exception
    {
        public int CurrentUnitCount { get; private set; }
        public int NewUnitCount { get; private set; }

        public ProductUnitCountChangeException(int currentUnitCount, int newUnitCount)
        {
            CurrentUnitCount = currentUnitCount;
            NewUnitCount = newUnitCount;
        }
    }
}