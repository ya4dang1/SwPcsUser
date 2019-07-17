using System;
using System.Collections.Generic;

namespace EmyralSystems.Models
{
    public partial class Reseller
    {
        public Reseller()
        {
            AspNetRoles = new HashSet<AspNetRoles>();
            Card = new HashSet<Card>();
            CardAssignmentHistory = new HashSet<CardAssignmentHistory>();
            CardBox = new HashSet<CardBox>();
            CardLoad = new HashSet<CardLoad>();
            CardUser = new HashSet<CardUser>();
            ResellerBalance = new HashSet<ResellerBalance>();
            ResellerCoBrand = new HashSet<ResellerCoBrand>();
            ResellerCurrency = new HashSet<ResellerCurrency>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string HostName { get; set; }
        public string Name { get; set; }
        public int ChargeTo { get; set; }
        public int Type { get; set; }
        public int MonthlyFeeChargeTo { get; set; }

        public virtual ICollection<AspNetRoles> AspNetRoles { get; set; }
        public virtual ICollection<Card> Card { get; set; }
        public virtual ICollection<CardAssignmentHistory> CardAssignmentHistory { get; set; }
        public virtual ICollection<CardBox> CardBox { get; set; }
        public virtual ICollection<CardLoad> CardLoad { get; set; }
        public virtual ICollection<CardUser> CardUser { get; set; }
        public virtual ICollection<ResellerBalance> ResellerBalance { get; set; }
        public virtual ICollection<ResellerCoBrand> ResellerCoBrand { get; set; }
        public virtual ICollection<ResellerCurrency> ResellerCurrency { get; set; }
    }
}
