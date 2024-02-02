using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceAPI.DataAccess.Models
{
    public partial class Product
    {
        public string Code { get; set; } = null!;
        [EnumDataType(typeof(ProductHashingRule))]
        public ProductHashingRule Rule { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime AssignedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsSupported { get; set; }

        public bool IsUnsupported()
        {
            return !IsSupported;
        }
    }

    public enum ProductHashingRule
    {
        [Description("Only transaction Id")]
        OnlyTransactionId = 1,
        [Description("Hash transactionId and amount number")]
        TransactionIdAndAcoundNumber = 2,
        [Description("All")]
        All = 3
    }
}
