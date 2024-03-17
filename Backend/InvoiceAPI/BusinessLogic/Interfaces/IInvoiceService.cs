using InvoiceAPI.BusinessLogic.Models;

namespace InvoiceAPI.BusinessLogic.Interfaces
{
    public interface IInvoiceService
    {
        Task<List<Invoice>> GetAll();
        Task Add(Invoice invoice);

        Task<bool> Delete(int invoiceId, int locationId);

        Task<bool> Update(int invoiceId, int locationId, Invoice invoice);
    }
}
