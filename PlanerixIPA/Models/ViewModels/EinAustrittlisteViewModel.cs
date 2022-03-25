using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanerixIPA.Models.ViewModels
{
    public class EinAustrittlisteViewModel
    {
        public string Name { get; set; }
        public string Vorname { get; set; }
        public string Eintritt { get; set; }
        public string Austritt { get; set; }
        public List<string> Abteilungen { get; set; }
    }
}
