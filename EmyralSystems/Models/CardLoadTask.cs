using System;
using System.Collections.Generic;

namespace EmyralSystems.Models
{
    public partial class CardLoadTask
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public int CardLoadId { get; set; }
        public int CoBrandId { get; set; }
        public int Quantity { get; set; }
        public double Amount { get; set; }
        public string Csv { get; set; }
        public string Remarks { get; set; }
        public int Status { get; set; }
        public int CurrencyId { get; set; }
        public double LoadingFee { get; set; }
        public double ResellerProfit { get; set; }
        public int? ResellerTransactionId { get; set; }
        public DateTime FormatFileDate { get; set; }
        public int FormatFileNumber { get; set; }
        public double MonthlyFee { get; set; }

        public virtual CardLoad CardLoad { get; set; }
        public virtual CoBrand CoBrand { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual ResellerTransaction ResellerTransaction { get; set; }
    }
}
