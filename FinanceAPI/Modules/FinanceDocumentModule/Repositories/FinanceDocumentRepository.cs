using FinanceAPI.DataAccess;
using FinanceAPI.DataAccess.Models;
using FinanceAPI.Modules.FinanceDocumentModule.Responses;
using FinanceAPI.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FinanceAPI.Modules.FinanceDocumentModule.Repositories
{
    public interface IFinanceDocumentRepository :
        IBaseRepository<FinanceDocument>
    {
        Task<string> GetById(
            Guid id,
            ProductHashingRule hashingRule);
    }

    public class FinanceDocumentRepository :
        BaseRepository<FinanceDocument>,
        IFinanceDocumentRepository
    {
        private readonly DataContext _context;

        public FinanceDocumentRepository(
            DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<string> GetById(
            Guid id,
            ProductHashingRule hashingRule)
        {
            var response = await InitializeDocumentById(id);
            return response is not null
                ? response.Serialize(hashingRule)
                : "{}";
        }

        private async Task<FinanceDocumentResponse?> InitializeDocumentById(Guid id)
        {
            return await _context
                .Set<FinanceDocument>()
                .Include(x => x.DocumentTransactions)
                .Select(x => x.Map())
                .FirstOrDefaultAsync(x => x.DocumentId.Equals(id));
        }
    }
}
