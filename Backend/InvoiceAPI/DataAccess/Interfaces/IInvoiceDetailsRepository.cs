using InvoiceAPI.DataAccess.Entities;

namespace InvoiceAPI.DataAccess.Interfaces
{
    public interface IInvoiceDetailsRepository: IBaseRepository<InvoiceDetailsEntity>
    {
        Task CreateInvoiceDetails(InvoiceDetailsEntity invoice);
    }
}
