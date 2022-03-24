using System;
using System.Collections.Generic;

#nullable disable

namespace PlanerixIPA.Models
{
    public partial class AbteilungMitarbeiter
    {
        public int AbteilungId { get; set; }
        public int MitarbeiterId { get; set; }

        public virtual Abteilung Abteilung { get; set; }
        public virtual Mitarbeiter Mitarbeiter { get; set; }
    }
}
