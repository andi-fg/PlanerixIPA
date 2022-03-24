using System;
using System.Collections.Generic;

#nullable disable

namespace PlanerixIPA.Models
{
    public partial class ProgrammVorschlag
    {
        public int ProgrammVorschlagId { get; set; }
        public int? ProgrammId { get; set; }
        public int? FunktionId { get; set; }
        public int? AbteilungId { get; set; }
        public bool? Checked { get; set; }

        public virtual Abteilung Abteilung { get; set; }
        public virtual Funktion Funktion { get; set; }
        public virtual Programm Programm { get; set; }
    }
}
