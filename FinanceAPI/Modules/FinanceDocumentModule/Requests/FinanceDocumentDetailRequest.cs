using FinanceAPI.DataAccess.Models;
using FinanceAPI.Shared.Extensions;
using System.Linq.Expressions;

namespace FinanceAPI.Modules.FinanceDocumentModule.Requests
{
    public class FinanceDocumentDetailRequest
    {
        public Guid TenantId { get; set; }
        public Guid DocumentId { get; set; }
        public string ProductCode { get; set; }

        public Expression<Func<Product, bool>> ExistsProduct()
        {
            return x => x.Code.Equals(ProductCode);
        }

        public Expression<Func<Tenant, bool>> IsWhiteListedTenant()
        {
            return x => x.Id.Equals(TenantId) && x.IsWhiteListed;
        }

        public bool IsValid()
        {
            //WARNING: This can be solved with FluentValidation as well
            bool hasTenantId = !TenantId.Equals(default),
                 hasDocumentId = !DocumentId.Equals(default),
                 hasProductCode = ProductCode.HasText();
            return hasTenantId && hasDocumentId && hasProductCode;
        }
    }
}
