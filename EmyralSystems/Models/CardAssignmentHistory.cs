using System;
using System.Collections.Generic;

namespace EmyralSystems.Models
{
    public partial class CardAssignmentHistory
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public int ResellerId { get; set; }
        public string Description { get; set; }
        public int TotalCards { get; set; }

        public virtual Reseller Reseller { get; set; }
    }
}
