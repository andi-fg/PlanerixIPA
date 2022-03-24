using System;
using System.Collections.Generic;

#nullable disable

namespace PlanerixIPA.Models
{
    public partial class ImpMitarbeiter
    {
        public string Personalnummer { get; set; }
        public string Name { get; set; }
        public string Vorname { get; set; }
        public string Mail { get; set; }
        public string Kurzzeichen { get; set; }
        public DateTime? Geburtsdatum { get; set; }
        public DateTime? Eintritt { get; set; }
        public DateTime? Austritt { get; set; }
        public string Gruppe { get; set; }
        public string Code { get; set; }
        public string Abteilung { get; set; }
        public string Funktion { get; set; }
        public string Itzugang { get; set; }
        public string Email { get; set; }
        public string Leitung { get; set; }
    }
}
