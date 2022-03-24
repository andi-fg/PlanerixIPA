using System;
using System.Collections.Generic;

#nullable disable

namespace PlanerixIPA.Models
{
    public partial class ProgrammMitarbeiter
    {
        public int ProgrammId { get; set; }
        public int MitarbeiterId { get; set; }

        public virtual Mitarbeiter Mitarbeiter { get; set; }
        public virtual Programm Programm { get; set; }
    }
}
