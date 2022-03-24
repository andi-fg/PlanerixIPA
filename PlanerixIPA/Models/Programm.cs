using System;
using System.Collections.Generic;

#nullable disable

namespace PlanerixIPA.Models
{
    public partial class Programm
    {
        public Programm()
        {
            Emails = new HashSet<Email>();
            ProgrammMitarbeiters = new HashSet<ProgrammMitarbeiter>();
            ProgrammVorschlags = new HashSet<ProgrammVorschlag>();
            VisumRights = new HashSet<VisumRight>();
            Visums = new HashSet<Visum>();
        }

        public int ProgrammId { get; set; }
        public string Bezeichnung { get; set; }
        public int? Gruppe { get; set; }
        public int? Reihenfolge { get; set; }
        public bool Aktiv { get; set; }

        public virtual ICollection<Email> Emails { get; set; }
        public virtual ICollection<ProgrammMitarbeiter> ProgrammMitarbeiters { get; set; }
        public virtual ICollection<ProgrammVorschlag> ProgrammVorschlags { get; set; }
        public virtual ICollection<VisumRight> VisumRights { get; set; }
        public virtual ICollection<Visum> Visums { get; set; }
    }
}
