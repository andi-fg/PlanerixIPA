using System;
using System.Collections.Generic;

#nullable disable

namespace PlanerixIPA.Models
{
    public partial class Email
    {
        public int EmailId { get; set; }
        public string Adresse { get; set; }
        public bool? It { get; set; }
        public int? AbteilungId { get; set; }
        public int? ProgrammId { get; set; }
        public int? FunktionId { get; set; }
        public bool? Pa { get; set; }

        public virtual Abteilung Abteilung { get; set; }
        public virtual Funktion Funktion { get; set; }
        public virtual Programm Programm { get; set; }
    }
}
