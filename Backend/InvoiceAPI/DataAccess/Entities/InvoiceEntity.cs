using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceAPI.DataAccess.Entities
{
    [PrimaryKey(nameof(InvoiceId), nameof(LocationId))]
    public class InvoiceEntity
    {
        public int InvoiceId { get; set; }
        public int LocationId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string CustomerName { get; set; }
        public virtual ICollection<InvoiceDetailsEntity> InvoiceDetails { get; set; }
    }
}
