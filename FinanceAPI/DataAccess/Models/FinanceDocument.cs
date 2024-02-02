using FinanceAPI.Modules.FinanceDocumentModule.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinanceAPI.DataAccess.Models
{
    public partial class FinanceDocument
    {
        public FinanceDocument()
        {
            Companies = new HashSet<Company>();
            DocumentTransactions = new HashSet<DocumentTransaction>();
        }

        public Guid Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        [EnumDataType(typeof(TransactionCurrency))]
        public TransactionCurrency Currency { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<DocumentTransaction> DocumentTransactions { get; set; }

        public FinanceDocumentResponse Map()
        {
            return new FinanceDocumentResponse
            {
                DocumentId = Id,
                AccountNumber = AccountNumber,
                Balance = Balance,
                Currency = Currency,
                Transactions = DocumentTransactions.Select(x => x.Map())
            };
        }
    }

    public enum TransactionCurrency
    {
        [Description("Euro")]
        EUR = 1,
        [Description("American dollar")]
        UsaDollar = 2
    }
}
