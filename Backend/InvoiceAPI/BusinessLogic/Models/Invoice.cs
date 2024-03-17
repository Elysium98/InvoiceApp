using Microsoft.EntityFrameworkCore;

namespace InvoiceAPI.BusinessLogic.Models
{
    [PrimaryKey(nameof(InvoiceId), nameof(LocationId))]
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int LocationId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string CustomerName { get; set; }
        public virtual ICollection<InvoiceDetails>? InvoiceDetails { get; set; }
    }
}
