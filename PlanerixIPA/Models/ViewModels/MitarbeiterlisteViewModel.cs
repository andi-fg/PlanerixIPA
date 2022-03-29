using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanerixIPA.Models.ViewModels
{
    public class MitarbeiterlisteViewModel
    {
        public string Name { get; set; }
        public string Vorname { get; set; }
        public string Benutzername { get; set; }
        public string Email { get; set; }
        public List<string> Funktionen { get; set; }
        public List<string> Abteilungen { get; set; }
        public List<string> Programme { get; set; }
        public string Eintritt { get; set; }
        public string Austritt { get; set; }
    }
}
