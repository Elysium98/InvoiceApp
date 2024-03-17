using AutoMapper;
using InvoiceAPI.BusinessLogic.Interfaces;
using InvoiceAPI.BusinessLogic.Models;
using InvoiceAPI.DataAccess.Entities;
using InvoiceAPI.DataAccess.Interfaces;

namespace InvoiceAPI.BusinessLogic.Implementation
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;

        public InvoiceService(IInvoiceRepository invoiceRepository, IMapper mapper)
        {
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
        }

        public async Task<List<Invoice>> GetAll()
        {
            var all = await _invoiceRepository.GetAllWithDetails();
            return _mapper.Map<List<Invoice>>(all);
        }

        public async Task Add(Invoice factura)
        {
            await _invoiceRepository.CreateInvoice(_mapper.Map<Invoice, InvoiceEntity>(factura));
        }

        public async Task<bool> Delete(int id, int locationId)
        {
            var invoice = await _invoiceRepository.GetByInvoiceId(id, locationId);

            if (invoice == null)
            {
                return false;
            }

            await _invoiceRepository.Delete(invoice);

            return true;
        }


        public async Task<bool> Update(int invoiceId, int locationId, Invoice model)
        {
            var invoiceFound = await _invoiceRepository.GetByInvoiceId(invoiceId, locationId);

            if (invoiceFound == null)
            {
                return false;
            }

            invoiceFound.CustomerName = model.CustomerName;
            invoiceFound.InvoiceDate = model.InvoiceDate;
            invoiceFound.InvoiceNumber = model.InvoiceNumber;

            await _invoiceRepository.Update(invoiceFound);

            return true;
        }
    }
}
