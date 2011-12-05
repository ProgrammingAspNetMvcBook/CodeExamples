using System;

namespace Ebuy
{
    public class Product
    {
        public event EventHandler<EventArgs> UnitsInStockChanged;

        public DateTime AvailabilityDate { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public ProductKind Kind { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }

        public int UnitsInStock
        {
            get { return _unitsInStock; }
            private set
            {
                var previous = _unitsInStock;
                _unitsInStock = value;

                if (UnitsInStockChanged != null)
                {
                    var args = new UnitsInStockChangedArgs(previous, _unitsInStock);
                    UnitsInStockChanged(this, args);
                }
            }
        }
        private int _unitsInStock;

        public void ReduceUnitsInStock(int unitCount)
        {
            if (unitCount == 0)
                return;

            int newCount = UnitsInStock - unitCount;

            if (unitCount < 0)
                throw new ProductUnitCountChangeException(UnitsInStock, newCount);

            UnitsInStock -= unitCount;
        }

        public void Restock(int totalUnitsInStock)
        {
            if (totalUnitsInStock < 0)
                throw new ProductUnitCountChangeException(UnitsInStock, totalUnitsInStock);

            UnitsInStock = totalUnitsInStock;
        }
    }
}
