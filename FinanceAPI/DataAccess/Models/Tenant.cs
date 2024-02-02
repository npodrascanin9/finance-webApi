using System;
using System.Collections.Generic;

namespace FinanceAPI.DataAccess.Models
{
    public partial class Tenant
    {
        public Tenant()
        {
            Clients = new HashSet<Client>();
        }

        public Guid Id { get; set; }
        public bool IsWhiteListed { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
}
