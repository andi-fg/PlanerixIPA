using System;
using System.Collections.Generic;

#nullable disable

namespace PlanerixIPA.Models
{
    public partial class VisumArt
    {
        public VisumArt()
        {
            EmailVorlages = new HashSet<EmailVorlage>();
            VisumRights = new HashSet<VisumRight>();
            Visums = new HashSet<Visum>();
        }

        public int VisumArtId { get; set; }
        public string Bezeichnung { get; set; }

        public virtual ICollection<EmailVorlage> EmailVorlages { get; set; }
        public virtual ICollection<VisumRight> VisumRights { get; set; }
        public virtual ICollection<Visum> Visums { get; set; }
    }
}
