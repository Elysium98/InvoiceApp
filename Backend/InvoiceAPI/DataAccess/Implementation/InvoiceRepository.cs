using InvoiceAPI.BusinessLogic.Models;
using InvoiceAPI.DataAccess.Entities;
using InvoiceAPI.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvoiceAPI.DataAccess.Implementation
{
    public class InvoiceRepository : BaseRepository<InvoiceEntity>, IInvoiceRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public InvoiceRepository(ApplicationDbContext applicationDbContext)
             : base(applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }
        private (int, int, int) GetNextId()
        {

            int lastInvoiceId = _dbContext.Invoices.Max(e => (int?)e.InvoiceId) ?? 0;
            int lastLocationId = _dbContext.Invoices.Max(e => (int?)e.LocationId) ?? 0;
            int lastDetailId = _dbContext.InvoiceDetails.Max(e => (int?)e.DetailId) ?? 0;

            return (lastInvoiceId + 1, lastLocationId + 1, lastDetailId + 1);
        }
        public virtual async Task CreateInvoice(InvoiceEntity entity)
        {
            var newlastIds = GetNextId();
            entity.InvoiceId = newlastIds.Item1;
            entity.LocationId = newlastIds.Item2;

            foreach (var detail in entity.InvoiceDetails)
            {
                if (detail.DetailId == 0)
                {
                    detail.DetailId = newlastIds.Item3;
                }
            }

            _dbContext.Set<InvoiceEntity>().Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<InvoiceEntity>> GetAllWithDetails()
        {
            return await _dbContext.Invoices
            .Include(x => x.InvoiceDetails)
            .ToListAsync();
        }

        public async Task<InvoiceEntity> GetByInvoiceId(int invoiceId, int locationId)
        {
            return await _dbContext.Invoices
                .Include(x => x.InvoiceDetails)
                .FirstOrDefaultAsync(e => e.InvoiceId == invoiceId && e.LocationId == locationId);
        }
    }
}
