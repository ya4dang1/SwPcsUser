using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text;

namespace Core.Models
{
    public class AuditLog
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string EntityName { get; set; }

        [Required]
        public Guid EntityId { get; set; }

        [Required]
        public EntityState Action { get; set; }

        [Required]
        public DateTime ActionOn { get; set; }

        [Required]
        public string ActionBy { get; set; }
        
        public string OldValue { get; set; }

        public string NewValue { get; set; }

        [NotMapped]
        public object OldObject
        {
            get
            {
                return JsonConvert.DeserializeObject(OldValue);
            }
        }

        [NotMapped]
        public object NewObject
        {
            get
            {
                return JsonConvert.DeserializeObject(NewValue);
            }
        }

        public List<Variance> Changes()
        {
            return OldObject.TrackChanges<object>(NewObject);
        }
    }

    static class Extensions
    {
        public static List<Variance> TrackChanges<T>(this T val1, T val2)
        {
            List<Variance> variances = new List<Variance>();
            FieldInfo[] fi = val1.GetType().GetFields();
            foreach (FieldInfo f in fi)
            {
                Variance v = new Variance();
                v.Prop = f.Name;
                v.Val1 = f.GetValue(val1);
                v.Val2 = f.GetValue(val2);
                if (!v.Val1.Equals(v.Val2))
                    variances.Add(v);
            }
            return variances;
        }
    }

    public class Variance
    {
        public string Prop { get; set; }
        public object Val1 { get; set; }
        public object Val2 { get; set; }
    }
}
