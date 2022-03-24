using System;
using System.Collections.Generic;

#nullable disable

namespace PlanerixIPA.Models
{
    public partial class EmailVorlage
    {
        public int EmailVorlageId { get; set; }
        public string Vorlage { get; set; }
        public string Betreff { get; set; }
        public int? VisumArtId { get; set; }
        public int? EreignisId { get; set; }

        public virtual Ereigni Ereignis { get; set; }
        public virtual VisumArt VisumArt { get; set; }
    }
}
