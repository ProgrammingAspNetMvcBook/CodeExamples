using System.Collections.Generic;
using System.Linq;

namespace Ebuy.Website.Models
{
    public class CategoryViewModel
    {
        public int AuctionsCount { get; set; }

        public bool HasSubCategories
        {
            get
            {
                return SubCategories != null
                    && SubCategories.Any();
            }
        }

        public string Key { get; set; }

        public string Name { get; set; }

        public bool IsTopLevelCategory { get; set; }

        public IEnumerable<CategoryViewModel> SubCategories { get; set; }
    }

}