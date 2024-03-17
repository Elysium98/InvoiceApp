using InvoiceAPI.BusinessLogic.Models;

namespace InvoiceAPI.BusinessLogic.Interfaces
{
    public interface IInvoiceDetailsService
    {

        Task<List<InvoiceDetails>> GetAll();
        Task Add(InvoiceDetails detailsInvoice);
        Task<bool> Delete(int id);
        Task<bool> Update(int id, InvoiceDetails detailsInvoice);

    }
}
