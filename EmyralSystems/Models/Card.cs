using System;
using System.Collections.Generic;

namespace EmyralSystems.Models
{
    public partial class Card
    {
        public Card()
        {
            CardTransaction = new HashSet<CardTransaction>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsAssigned { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public int CoBrandId { get; set; }
        public int? ResellerId { get; set; }
        public int? CardUserId { get; set; }
        public string Balance { get; set; }
        public int? CardBoxId { get; set; }
        public DateTime LastSync { get; set; }
        public int LoadCount { get; set; }

        public virtual CardBox CardBox { get; set; }
        public virtual CardUser CardUser { get; set; }
        public virtual CoBrand CoBrand { get; set; }
        public virtual Reseller Reseller { get; set; }
        public virtual ICollection<CardTransaction> CardTransaction { get; set; }
    }
}
