using System;
using System.Collections.Generic;

#nullable disable

namespace PlanerixIPA.Models
{
    public partial class FunktionMitarbeiter
    {
        public int FunktionId { get; set; }
        public int MitarbeiterId { get; set; }

        public virtual Funktion Funktion { get; set; }
        public virtual Mitarbeiter Mitarbeiter { get; set; }
    }
}
