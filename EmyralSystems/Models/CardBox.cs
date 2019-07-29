using System;
using System.Collections.Generic;

namespace EmyralSystems.Models
{
    public partial class CardBox
    {
        public CardBox()
        {
            Card = new HashSet<Card>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string BoxId { get; set; }
        public int Type { get; set; }
        public bool IsAssigned { get; set; }
        public int? CobrandId { get; set; }
        public int? ResellerId { get; set; }
        public int? FileId { get; set; }

        public virtual CoBrand Cobrand { get; set; }
        public virtual FileLibrary File { get; set; }
        public virtual Reseller Reseller { get; set; }
        public virtual ICollection<Card> Card { get; set; }
    }
}
