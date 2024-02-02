using FinanceAPI.DataAccess.Models;
using FinanceAPI.Shared.Extensions;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FinanceAPI.Modules.FinanceDocumentModule.Responses
{
    public class FinanceDocumentResponse
    {
        public Guid DocumentId { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        [EnumDataType(typeof(TransactionCurrency))]
        public TransactionCurrency Currency { get; set; }
        public string CurrencyDescription
        {
            get => Currency.GetDescription();
        }

        public virtual IEnumerable<FinanceDocumentTransactionRowResponse> Transactions { get; set; }

        public string Serialize(ProductHashingRule rule)
        {
            return JsonConvert.SerializeObject(MapByRule(rule));
        }

        private FinanceDocumentDataResponse MapByRule(ProductHashingRule rule)
        {
            return rule switch
            {
                ProductHashingRule.All => HashAll(),
                ProductHashingRule.OnlyTransactionId => HashTransactionIdOnly(),
                ProductHashingRule.TransactionIdAndAcoundNumber => HashTransactionIdAndAcountNumber(),
                _ => new FinanceDocumentDataResponse()
            };
        }

        private FinanceDocumentDataResponse HashAll()
        {
            return new FinanceDocumentDataResponse
            {
                DocumentId = DocumentId.HashValue(),
                AccountNumber = AccountNumber.HashValue(),
                Balance = Balance.HashValue(),
                CurrencyDescription = Balance.HashValue(),
                Transactions = Transactions.Select(x => x.HashAll())
            };
        }

        private FinanceDocumentDataResponse HashTransactionIdOnly()
        {
            return new FinanceDocumentDataResponse
            {
                DocumentId = DocumentId,
                AccountNumber = AccountNumber,
                Balance = Balance,
                CurrencyDescription = Balance,
                Transactions = Transactions.Select(x => x.HashTransactionIdOnly())
            };
        }

        private FinanceDocumentDataResponse HashTransactionIdAndAcountNumber()
        {
            return new FinanceDocumentDataResponse
            {
                DocumentId = DocumentId,
                AccountNumber = AccountNumber.HashValue(),
                Balance = Balance,
                CurrencyDescription = Balance,
                Transactions = Transactions.Select(x => x.HashTransactionIdAndAcountNumber())
            };
        }
    }

    public class FinanceDocumentTransactionRowResponse
    {
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        [EnumDataType(typeof(DocumentTransactionCategory))]
        public DocumentTransactionCategory Category { get; set; }
        public string CategoryDescription
        {
            get => Category.GetDescription();
        }

        public FinanceDocumentTransactionRowDataResponse HashAll()
        {
            return new FinanceDocumentTransactionRowDataResponse
            {
                TransactionId = TransactionId.HashValue(),
                Amount = Amount.HashValue(),
                CategoryDescription = CategoryDescription.HashValue(),
                Date = Date.HashValue(),
                Description = Description.HashValue()
            };
        }

        public FinanceDocumentTransactionRowDataResponse HashTransactionIdOnly()
        {
            return new FinanceDocumentTransactionRowDataResponse
            {
                TransactionId = TransactionId.HashValue(),
                Amount = Amount,
                CategoryDescription = CategoryDescription,
                Date = Date,
                Description = Description
            };
        }

        public FinanceDocumentTransactionRowDataResponse HashTransactionIdAndAcountNumber()
        {
            return new FinanceDocumentTransactionRowDataResponse
            {
                TransactionId = TransactionId.HashValue(),
                Amount = Amount,
                CategoryDescription = CategoryDescription,
                Date = Date,
                Description = Description
            };
        }
    }
}
