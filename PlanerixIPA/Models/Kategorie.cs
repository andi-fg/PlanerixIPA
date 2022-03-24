using System;
using System.Collections.Generic;

#nullable disable

namespace PlanerixIPA.Models
{
    public partial class Kategorie
    {
        public Kategorie()
        {
            Objekts = new HashSet<Objekt>();
        }

        public int KategorieId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Objekt> Objekts { get; set; }
    }
}
