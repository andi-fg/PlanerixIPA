using System;
using System.Collections.Generic;

#nullable disable

namespace PlanerixIPA.Models
{
    public partial class MenupunktRight
    {
        public int MenupunktRightId { get; set; }
        public int? BenutzerId { get; set; }
        public int? MenupunktId { get; set; }
        public bool? ReadOnly { get; set; }
        public bool? Aktiv { get; set; }

        public virtual Benutzer Benutzer { get; set; }
        public virtual Menupunkt Menupunkt { get; set; }
    }
}
