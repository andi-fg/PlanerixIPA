using System;
using System.Collections.Generic;

#nullable disable

namespace PlanerixIPA.Models
{
    public partial class AbteilungFunktion
    {
        public int AbteilungId { get; set; }
        public int FunktionId { get; set; }

        public virtual Abteilung Abteilung { get; set; }
        public virtual Funktion Funktion { get; set; }
    }
}
