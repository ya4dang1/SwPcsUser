﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmyralSystems.Models
{
    public partial class CardTransaction
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public double Amount { get; set; }
        public int CardId { get; set; }
        public string Currency { get; set; }
        public string MerchantId { get; set; }
        public string MerchantName { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm:ss tt}")]
        public DateTime TranDate { get; set; }
        public string TranType { get; set; }

        public virtual Card Card { get; set; }
    }
}
