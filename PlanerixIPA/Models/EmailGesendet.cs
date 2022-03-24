using System;
using System.Collections.Generic;

#nullable disable

namespace PlanerixIPA.Models
{
    public partial class EmailGesendet
    {
        public int MailitemId { get; set; }
        public int? VisumId { get; set; }
        public bool? IsReminder { get; set; }
        public DateTime? Datum { get; set; }

        public virtual Visum Visum { get; set; }
    }
}
