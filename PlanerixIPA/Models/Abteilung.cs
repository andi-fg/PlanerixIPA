using System;
using System.Collections.Generic;

#nullable disable

namespace PlanerixIPA.Models
{
    public partial class Abteilung
    {
        public Abteilung()
        {
            AbteilungFunktions = new HashSet<AbteilungFunktion>();
            AbteilungMitarbeiters = new HashSet<AbteilungMitarbeiter>();
            Emails = new HashSet<Email>();
            ProgrammVorschlags = new HashSet<ProgrammVorschlag>();
            VisumRights = new HashSet<VisumRight>();
            Visums = new HashSet<Visum>();
        }

        public int AbteilungId { get; set; }
        public string Bezeichnung { get; set; }

        public virtual ICollection<AbteilungFunktion> AbteilungFunktions { get; set; }
        public virtual ICollection<AbteilungMitarbeiter> AbteilungMitarbeiters { get; set; }
        public virtual ICollection<Email> Emails { get; set; }
        public virtual ICollection<ProgrammVorschlag> ProgrammVorschlags { get; set; }
        public virtual ICollection<VisumRight> VisumRights { get; set; }
        public virtual ICollection<Visum> Visums { get; set; }
    }
}
