using System;
using System.Collections.Generic;

#nullable disable

namespace PlanerixIPA.Models
{
    public partial class PczugangVorschlag
    {
        public int PczugangVorschlagId { get; set; }
        public int? FunktionId { get; set; }
        public int? Pczugang { get; set; }

        public virtual Funktion Funktion { get; set; }
    }
}
