using System;
using System.Collections.Generic;

#nullable disable

namespace PlanerixIPA.Models
{
    public partial class Menupunkt
    {
        public Menupunkt()
        {
            MenupunktRights = new HashSet<MenupunktRight>();
        }

        public int MenupunktId { get; set; }
        public string Bezeichnung { get; set; }

        public virtual ICollection<MenupunktRight> MenupunktRights { get; set; }
    }
}
