using AutoMapper;
using InvoiceAPI.BusinessLogic.Interfaces;
using InvoiceAPI.BusinessLogic.Models;
using InvoiceAPI.DataAccess.Entities;
using InvoiceAPI.DataAccess.Implementation;
using InvoiceAPI.DataAccess.Interfaces;

namespace InvoiceAPI.BusinessLogic.Implementation
{
    public class InvoiceDetailsService : IInvoiceDetailsService
    {
        private readonly IInvoiceDetailsRepository _invoiceDetailsRepository;
        private readonly IMapper _mapper;

        public InvoiceDetailsService(IInvoiceDetailsRepository invoiceDetailsRepository, IMapper mapper)
        {
            _invoiceDetailsRepository = invoiceDetailsRepository;
            _mapper = mapper;
        }

        public async Task<List<InvoiceDetails>> GetAll()
        {
            var all = await _invoiceDetailsRepository.FindAll();
            return _mapper.Map<List<InvoiceDetails>>(all);
        }


        public async Task Add(InvoiceDetails invoiceDetails)
        {
            await _invoiceDetailsRepository.CreateInvoiceDetails(_mapper.Map<InvoiceDetails, InvoiceDetailsEntity>(invoiceDetails));
        }

        public async Task<bool> Delete(int id)
        {
            var invoice = await _invoiceDetailsRepository.GetById(id);

            if (invoice == null)
            {
                return false;
            }

            await _invoiceDetailsRepository.Delete(invoice);

            return true;
        }

        public async Task<bool> Update(int id, InvoiceDetails model)
        {
            var invoiceFound = await _invoiceDetailsRepository.GetById(id);

            if (invoiceFound == null)
            {
                return false;
            }

            invoiceFound.ProductName = model.ProductName;
            invoiceFound.Quantity = model.Quantity;
            invoiceFound.UnitPrice = model.UnitPrice;
            invoiceFound.Value = model.Value;

            await _invoiceDetailsRepository.Update(invoiceFound);

            return true;
        }
    }
}
