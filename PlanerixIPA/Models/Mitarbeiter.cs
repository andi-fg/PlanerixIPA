using System;
using System.Collections.Generic;

#nullable disable

namespace PlanerixIPA.Models
{
    public partial class Mitarbeiter
    {
        public Mitarbeiter()
        {
            AbteilungMitarbeiters = new HashSet<AbteilungMitarbeiter>();
            EreignisMitarbeiters = new HashSet<EreignisMitarbeiter>();
            FunktionMitarbeiters = new HashSet<FunktionMitarbeiter>();
            ProgrammMitarbeiters = new HashSet<ProgrammMitarbeiter>();
            Verleihs = new HashSet<Verleih>();
        }

        public int MitarbeiterId { get; set; }
        public string Name { get; set; }
        public string Vorname { get; set; }
        public string Email { get; set; }
        public string Kurzzeichen { get; set; }
        public string InitialPasswort { get; set; }
        public string Personalnummer { get; set; }
        public DateTime? Geburtsdatum { get; set; }
        public DateTime? Eintritt { get; set; }
        public DateTime? Austritt { get; set; }
        public bool? KeinEdvzugriff { get; set; }
        public DateTime? LetzterArbeitstag { get; set; }
        public string Bemerkung { get; set; }
        public Guid? Mguid { get; set; }
        public int? Pczugang { get; set; }
        public string EmailPrivat { get; set; }
        public string Stellenbeschreibung { get; set; }

        public virtual ICollection<AbteilungMitarbeiter> AbteilungMitarbeiters { get; set; }
        public virtual ICollection<EreignisMitarbeiter> EreignisMitarbeiters { get; set; }
        public virtual ICollection<FunktionMitarbeiter> FunktionMitarbeiters { get; set; }
        public virtual ICollection<ProgrammMitarbeiter> ProgrammMitarbeiters { get; set; }
        public virtual ICollection<Verleih> Verleihs { get; set; }
    }
}
