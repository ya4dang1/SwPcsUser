using System;
using System.Collections.Generic;

namespace EmyralSystems.Models
{
    public partial class CoBrand
    {
        public CoBrand()
        {
            Card = new HashSet<Card>();
            CardBox = new HashSet<CardBox>();
            CardLoadTask = new HashSet<CardLoadTask>();
            ResellerCoBrand = new HashSet<ResellerCoBrand>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string EmailPassword { get; set; }
        public string CmsHostname { get; set; }
        public string CmsPassword { get; set; }
        public string CmsUsername { get; set; }

        public virtual ICollection<Card> Card { get; set; }
        public virtual ICollection<CardBox> CardBox { get; set; }
        public virtual ICollection<CardLoadTask> CardLoadTask { get; set; }
        public virtual ICollection<ResellerCoBrand> ResellerCoBrand { get; set; }
    }
}
