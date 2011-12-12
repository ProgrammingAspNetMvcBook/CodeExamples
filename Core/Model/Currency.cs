using System.ComponentModel.DataAnnotations;

namespace Ebuy
{
    [ComplexType]
    public class Currency
    {
        public string Code { get; set; }
        public decimal Amount { get; set; }
    }
}