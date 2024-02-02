using FinanceAPI.Modules.FinanceDocumentModule.Responses;
using FinanceAPI.Shared.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinanceAPI.DataAccess.Models
{
    public partial class DocumentTransaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        [EnumDataType(typeof(DocumentTransactionCategory))]
        public DocumentTransactionCategory Category { get; set; }
        public Guid? DocumentId { get; set; }

        public virtual FinanceDocument? Document { get; set; }


        public FinanceDocumentTransactionRowResponse Map()
        {
            return new FinanceDocumentTransactionRowResponse
            {
                TransactionId = Id,
                Amount = Amount,
                Category = Category,
                Date = Date,
                Description = Description
            };
        }
    }

    public enum DocumentTransactionCategory
    {
        [Description("Victuals")]
        FoodAndDrins = 1,
        [Description("N/A")]
        Unknown = 2
    }
}
