using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Libraries
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }

        public string ActionBy { get; set; }
        public DateTime ActionOn { get; set; }
    }
}
