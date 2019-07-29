using System;
using System.Collections.Generic;

namespace EmyralSystems.Models
{
    public partial class ResellerTransaction
    {
        public ResellerTransaction()
        {
            CardLoadTask = new HashSet<CardLoadTask>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public int Type { get; set; }
        public double OldBalance { get; set; }
        public double AdjustedAmount { get; set; }
        public double NewBalance { get; set; }
        public string Remark { get; set; }
        public int Status { get; set; }
        public int ResellerBalanceId { get; set; }

        public virtual ResellerBalance ResellerBalance { get; set; }
        public virtual ICollection<CardLoadTask> CardLoadTask { get; set; }
    }
}
