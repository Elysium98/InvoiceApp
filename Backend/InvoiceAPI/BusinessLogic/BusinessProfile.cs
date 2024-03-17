using AutoMapper;
using InvoiceAPI.BusinessLogic.Models;
using InvoiceAPI.DataAccess.Entities;

namespace InvoiceAPI.BusinessLogic
{
    public class BusinessProfile : Profile
    {
        public BusinessProfile()
        {
            CreateMap<Invoice, InvoiceEntity>();
            CreateMap<InvoiceEntity, Invoice>();
            CreateMap<InvoiceDetails, InvoiceDetailsEntity>();
            CreateMap<InvoiceDetailsEntity, InvoiceDetails>();
        }
    }
}
