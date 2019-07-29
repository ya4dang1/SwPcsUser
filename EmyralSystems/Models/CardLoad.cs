using System;
using System.Collections.Generic;

namespace EmyralSystems.Models
{
    public partial class CardLoad
    {
        public CardLoad()
        {
            CardLoadTask = new HashSet<CardLoadTask>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string BatchId { get; set; }
        public int? ResellerId { get; set; }
        public int ImportType { get; set; }
        public string Data { get; set; }
        public int? FileId { get; set; }
        public int CardLoadStatus { get; set; }
        public string TransactionId { get; set; }

        public virtual FileLibrary File { get; set; }
        public virtual Reseller Reseller { get; set; }
        public virtual ICollection<CardLoadTask> CardLoadTask { get; set; }
    }
}
