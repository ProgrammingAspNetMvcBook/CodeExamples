using System;

namespace Ebuy
{
    public class UnitsInStockChangedArgs : EventArgs
    {
        public int NewUnitCount { get; private set; }
        public int PreviousUnitCount { get; private set; }

        public UnitsInStockChangedArgs(int previousUnitCount, int newUnitCount)
        {
            PreviousUnitCount = previousUnitCount;
            NewUnitCount = newUnitCount;
        }
    }
}