using System;
using System.Collections.Generic;

#nullable disable

namespace PlanerixIPA.Models
{
    public partial class Visum
    {
        public Visum()
        {
            EmailGesendets = new HashSet<EmailGesendet>();
        }

        public int VisumId { get; set; }
        public int? EreignisMitarbeiterId { get; set; }
        public int? AbteilungId { get; set; }
        public int? ProgrammId { get; set; }
        public int? VisumArtId { get; set; }
        public bool Visiert { get; set; }
        public DateTime? VisiertAm { get; set; }
        public int? VisiertVonBenutzerId { get; set; }
        public bool? Aktiv { get; set; }
        public string Info { get; set; }
        public DateTime? Modified { get; set; }
        public Guid? Vguid { get; set; }
        public bool? Email { get; set; }
        public bool? Erforderlich { get; set; }

        public virtual Abteilung Abteilung { get; set; }
        public virtual EreignisMitarbeiter EreignisMitarbeiter { get; set; }
        public virtual Programm Programm { get; set; }
        public virtual Benutzer VisiertVonBenutzer { get; set; }
        public virtual VisumArt VisumArt { get; set; }
        public virtual ICollection<EmailGesendet> EmailGesendets { get; set; }
    }
}
