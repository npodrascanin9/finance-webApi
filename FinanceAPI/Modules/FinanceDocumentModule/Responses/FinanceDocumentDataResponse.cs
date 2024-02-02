using FinanceAPI.DataAccess.Models;
using System.ComponentModel.DataAnnotations;

namespace FinanceAPI.Modules.FinanceDocumentModule.Responses
{
    public class FinanceDocumentDataResponse
    {
        public object DocumentId { get; set; }
        public object AccountNumber { get; set; }
        public object Balance { get; set; }
        public object CurrencyDescription { get; set; }

        public IEnumerable<FinanceDocumentTransactionRowDataResponse> Transactions { get; set; }
    }

    public class FinanceDocumentTransactionRowDataResponse
    {
        public object TransactionId { get; set; }
        public object Amount { get; set; }
        public object Date { get; set; }
        public object Description { get; set; }
        public object CategoryDescription { get; set; }
    }
}
