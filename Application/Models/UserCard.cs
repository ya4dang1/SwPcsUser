using Core.Libraries;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Models
{
    public class UserCard: BaseModel
    {
        [Required]
        public string CardNumber { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public int CVV { get; set; }

        [Required]
        public int PIN { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM'/'yyyy}")]
        [JsonConverter(typeof(IsoDateTimeConverter), "MM'/'yyyy")]
        public DateTime ExpiredDate { get; set; }

    }
}
