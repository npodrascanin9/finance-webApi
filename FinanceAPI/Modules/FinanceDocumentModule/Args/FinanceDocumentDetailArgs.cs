using FinanceAPI.DataAccess.Models;

namespace FinanceAPI.Modules.FinanceDocumentModule.Args
{
    public class FinanceDocumentDetailArgs
    {
        public Guid DocumentId { get; set; }
        public string CompanyRegistrationNumber { get; set; }
        public Product Product { get; set; }
    }
}
