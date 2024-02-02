using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinanceAPI.DataAccess.Models
{
    public partial class Client
    {
        public Guid Id { get; set; }
        public Guid? TenantId { get; set; }
        public string Vat { get; set; } = null!;
        public string CompanyRegistrationNumber { get; set; } = null!;
        [EnumDataType(typeof(ClientCompanyType))]
        public ClientCompanyType CompanyType { get; set; }

        public virtual Tenant? Tenant { get; set; }
        public virtual Company VatNavigation { get; set; } = null!;

        public bool CanAccessData()
        {
            bool isMedium = CompanyType.Equals(ClientCompanyType.Medium),
                 isLarge = CompanyType.Equals(ClientCompanyType.Large);
            return isMedium || isLarge;
        }
    }

    public enum ClientCompanyType
    {
        [Description("Small")]
        Small = 1,
        [Description("Medium")]
        Medium = 2,
        [Description("Large")]
        Large = 3
    }
}
