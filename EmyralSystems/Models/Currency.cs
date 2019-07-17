using System;
using System.Collections.Generic;

namespace EmyralSystems.Models
{
    public partial class Currency
    {
        public Currency()
        {
            CardLoadTask = new HashSet<CardLoadTask>();
            ResellerBalance = new HashSet<ResellerBalance>();
            ResellerCurrency = new HashSet<ResellerCurrency>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Country { get; set; }

        public virtual ICollection<CardLoadTask> CardLoadTask { get; set; }
        public virtual ICollection<ResellerBalance> ResellerBalance { get; set; }
        public virtual ICollection<ResellerCurrency> ResellerCurrency { get; set; }
    }
}
