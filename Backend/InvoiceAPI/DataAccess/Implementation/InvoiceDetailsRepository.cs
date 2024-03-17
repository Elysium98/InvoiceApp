using InvoiceAPI.DataAccess.Entities;
using InvoiceAPI.DataAccess.Interfaces;

namespace InvoiceAPI.DataAccess.Implementation
{
    public class InvoiceDetailsRepository : BaseRepository<InvoiceDetailsEntity>, IInvoiceDetailsRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public InvoiceDetailsRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        private int GetNextId()
        {

            int lastDetailId = _dbContext.InvoiceDetails.Max(e => (int?)e.DetailId) ?? 0;

            return lastDetailId + 1;
        }

        public virtual async Task CreateInvoiceDetails(InvoiceDetailsEntity entity)
        {
            var newLastId = GetNextId();

            entity.DetailId = newLastId;

            _dbContext.Set<InvoiceDetailsEntity>().Add(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
