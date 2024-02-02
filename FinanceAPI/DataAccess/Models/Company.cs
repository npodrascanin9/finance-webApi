using System;
using System.Collections.Generic;

namespace FinanceAPI.DataAccess.Models
{
    public partial class Company
    {
        public Company()
        {
            Clients = new HashSet<Client>();
        }

        public string Vat { get; set; } = null!;
        public Guid? DocumentId { get; set; }

        public virtual FinanceDocument? Document { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
    }
}
