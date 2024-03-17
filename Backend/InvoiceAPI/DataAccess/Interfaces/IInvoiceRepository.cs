using InvoiceAPI.DataAccess.Entities;

namespace InvoiceAPI.DataAccess.Interfaces
{
    public interface IInvoiceRepository : IBaseRepository<InvoiceEntity>
    {
        Task<IEnumerable<InvoiceEntity>> GetAllWithDetails();

        Task CreateInvoice(InvoiceEntity invoice);

        Task<InvoiceEntity> GetByInvoiceId(int invoiceId, int locationId);

    }

}
