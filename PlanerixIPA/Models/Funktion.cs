using System;
using System.Collections.Generic;

#nullable disable

namespace PlanerixIPA.Models
{
    public partial class Funktion
    {
        public Funktion()
        {
            AbteilungFunktions = new HashSet<AbteilungFunktion>();
            Emails = new HashSet<Email>();
            FunktionMitarbeiters = new HashSet<FunktionMitarbeiter>();
            PczugangVorschlags = new HashSet<PczugangVorschlag>();
            ProgrammVorschlags = new HashSet<ProgrammVorschlag>();
        }

        public int FunktionId { get; set; }
        public string Bezeichnung { get; set; }

        public virtual ICollection<AbteilungFunktion> AbteilungFunktions { get; set; }
        public virtual ICollection<Email> Emails { get; set; }
        public virtual ICollection<FunktionMitarbeiter> FunktionMitarbeiters { get; set; }
        public virtual ICollection<PczugangVorschlag> PczugangVorschlags { get; set; }
        public virtual ICollection<ProgrammVorschlag> ProgrammVorschlags { get; set; }
    }
}
