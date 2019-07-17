using System;
using System.Collections.Generic;

namespace EmyralSystems.Models
{
    public partial class FileLibrary
    {
        public FileLibrary()
        {
            CardBox = new HashSet<CardBox>();
            CardLoad = new HashSet<CardLoad>();
            CardUserAddressProof = new HashSet<CardUser>();
            CardUserUserIdentificationProof = new HashSet<CardUser>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
        public string FileName { get; set; }
        public byte[] File { get; set; }

        public virtual ICollection<CardBox> CardBox { get; set; }
        public virtual ICollection<CardLoad> CardLoad { get; set; }
        public virtual ICollection<CardUser> CardUserAddressProof { get; set; }
        public virtual ICollection<CardUser> CardUserUserIdentificationProof { get; set; }
    }
}
