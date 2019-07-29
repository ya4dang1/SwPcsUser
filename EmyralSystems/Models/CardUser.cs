using System;
using System.Collections.Generic;

namespace EmyralSystems.Models
{
    public partial class CardUser
    {
        public CardUser()
        {
            Card = new HashSet<Card>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public int ResellerId { get; set; }
        public string UserIdentification { get; set; }
        public int? AddressProofId { get; set; }
        public string Comments { get; set; }
        public int Status { get; set; }
        public DateTime? UserIdentificationExpiryDate { get; set; }
        public int? UserIdentificationProofId { get; set; }

        public virtual FileLibrary AddressProof { get; set; }
        public virtual Reseller Reseller { get; set; }
        public virtual FileLibrary UserIdentificationProof { get; set; }
        public virtual ICollection<Card> Card { get; set; }
    }
}
