using System;
using System.Collections.Generic;

namespace EmyralSystems.Models
{
    public partial class ResellerCurrency
    {
        public int ResellerId { get; set; }
        public int CurrencyId { get; set; }
        public double LoadingFeePercent { get; set; }
        public double MaxLoading { get; set; }
        public double MinLoading { get; set; }
        public double MinLoadingFeeAmount { get; set; }
        public double MinResellerProfitAmount { get; set; }
        public double ResellerProfitPercent { get; set; }
        public double MonthlyFee { get; set; }

        public virtual Currency Currency { get; set; }
        public virtual Reseller Reseller { get; set; }
    }
}
