using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sessions.Models
{
    public class AuditLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Actions Action { get; set; }
        public string Log { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Date { get; set; }

        public static AuditLog GetLog(Actions action, string log)
        {
            return new AuditLog
            {
                Action = action,
                Log = log,
                Date = DateTime.Now
            };
        }
    }
}
