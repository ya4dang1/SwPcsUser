using System;
using System.Collections.Generic;

namespace EmyralSystems.Models
{
    public partial class ResellerCoBrand
    {
        public int ResellerId { get; set; }
        public int CoBrandId { get; set; }

        public virtual CoBrand CoBrand { get; set; }
        public virtual Reseller Reseller { get; set; }
    }
}
