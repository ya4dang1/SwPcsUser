using System;
using System.Collections.Generic;

namespace EmyralSystems.Models
{
    public partial class ResellerBalance
    {
        public ResellerBalance()
        {
            ResellerTransaction = new HashSet<ResellerTransaction>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public int CurrencyId { get; set; }
        public double Balance { get; set; }
        public bool Active { get; set; }
        public int? ResellerId { get; set; }

        public virtual Currency Currency { get; set; }
        public virtual Reseller Reseller { get; set; }
        public virtual ICollection<ResellerTransaction> ResellerTransaction { get; set; }
    }
}
