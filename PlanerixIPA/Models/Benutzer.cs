using System;
using System.Collections.Generic;

#nullable disable

namespace PlanerixIPA.Models
{
    public partial class Benutzer
    {
        public Benutzer()
        {
            EreignisMitarbeiters = new HashSet<EreignisMitarbeiter>();
            MenupunktRights = new HashSet<MenupunktRight>();
            VisumRights = new HashSet<VisumRight>();
            Visums = new HashSet<Visum>();
        }

        public int BenutzerId { get; set; }
        public string Benutzername { get; set; }
        public string Passwort { get; set; }
        public string Email { get; set; }
        public bool Aktiv { get; set; }
        public string Name { get; set; }
        public string Vorname { get; set; }
        public Guid? LoginSession { get; set; }
        public byte[] Salt { get; set; }
        public string Kuerzel { get; set; }

        public virtual ICollection<EreignisMitarbeiter> EreignisMitarbeiters { get; set; }
        public virtual ICollection<MenupunktRight> MenupunktRights { get; set; }
        public virtual ICollection<VisumRight> VisumRights { get; set; }
        public virtual ICollection<Visum> Visums { get; set; }
    }
}
