using System;
using System.Collections.Generic;

#nullable disable

namespace PlanerixIPA.Models
{
    public partial class Ereigni
    {
        public Ereigni()
        {
            EmailVorlages = new HashSet<EmailVorlage>();
            EreignisMitarbeiters = new HashSet<EreignisMitarbeiter>();
        }

        public int EreignisId { get; set; }
        public string Bezeichnung { get; set; }

        public virtual ICollection<EmailVorlage> EmailVorlages { get; set; }
        public virtual ICollection<EreignisMitarbeiter> EreignisMitarbeiters { get; set; }
    }
}
