using System;
using System.Collections.Generic;

#nullable disable

namespace PlanerixIPA.Models
{
    public partial class Objekt
    {
        public Objekt()
        {
            Verleihs = new HashSet<Verleih>();
        }

        public int ObjektId { get; set; }
        public int KategorieId { get; set; }
        public string Name { get; set; }
        public bool? Aktiv { get; set; }
        public string Begruendung { get; set; }

        public virtual Kategorie Kategorie { get; set; }
        public virtual ICollection<Verleih> Verleihs { get; set; }
    }
}
