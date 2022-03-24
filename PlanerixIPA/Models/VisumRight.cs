using System;
using System.Collections.Generic;

#nullable disable

namespace PlanerixIPA.Models
{
    public partial class VisumRight
    {
        public int VisumRightId { get; set; }
        public int? BenutzerId { get; set; }
        public int? AbteilungId { get; set; }
        public int? ProgrammId { get; set; }
        public int? VisumArtId { get; set; }
        public bool? ReadOnly { get; set; }
        public bool? Aktiv { get; set; }

        public virtual Abteilung Abteilung { get; set; }
        public virtual Benutzer Benutzer { get; set; }
        public virtual Programm Programm { get; set; }
        public virtual VisumArt VisumArt { get; set; }
    }
}
