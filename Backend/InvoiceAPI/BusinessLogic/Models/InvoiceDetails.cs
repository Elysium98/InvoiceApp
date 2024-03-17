using System.ComponentModel.DataAnnotations;

namespace InvoiceAPI.BusinessLogic.Models
{
    public class InvoiceDetails
    {
        public int DetailId { get; set; }
        public int InvoiceId { get; set; }
        public int LocationId { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Value { get; set; }
    }
}
