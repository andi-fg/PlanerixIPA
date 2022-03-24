using System;
using System.Collections.Generic;

#nullable disable

namespace PlanerixIPA.Models
{
    public partial class EreignisMitarbeiter
    {
        public EreignisMitarbeiter()
        {
            Visums = new HashSet<Visum>();
        }

        public int EreignisMitarbeiterId { get; set; }
        public int? EreignisId { get; set; }
        public int? MitarbeiterId { get; set; }
        public DateTime? Datum { get; set; }
        public bool Erledigt { get; set; }
        public bool Aktiv { get; set; }
        public string Info { get; set; }
        public DateTime? Modified { get; set; }
        public int BenutzerId { get; set; }
        public Guid? Eguid { get; set; }
        public string InfoErledigt { get; set; }

        public virtual Benutzer Benutzer { get; set; }
        public virtual Ereigni Ereignis { get; set; }
        public virtual Mitarbeiter Mitarbeiter { get; set; }
        public virtual ICollection<Visum> Visums { get; set; }
    }
}
