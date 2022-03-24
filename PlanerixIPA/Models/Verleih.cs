using System;
using System.Collections.Generic;

#nullable disable

namespace PlanerixIPA.Models
{
    public partial class Verleih
    {
        public int VerleihId { get; set; }
        public int MitarbeiterId { get; set; }
        public int ObjektId { get; set; }
        public bool? Erhalt { get; set; }
        public DateTime? ErhaltDatum { get; set; }
        public bool? AbgegebenMitarbeiter { get; set; }
        public bool? Abgegeben { get; set; }
        public bool? Abgeschlossen { get; set; }
        public DateTime? AbgeschlossenDatum { get; set; }
        public DateTime? Eingetragen { get; set; }
        public string Unterschrift { get; set; }

        public virtual Mitarbeiter Mitarbeiter { get; set; }
        public virtual Objekt Objekt { get; set; }
    }
}
