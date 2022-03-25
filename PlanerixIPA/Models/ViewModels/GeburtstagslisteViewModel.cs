using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanerixIPA.Models.ViewModels
{
    public class GeburtstagslisteViewModel
    {
        public string Name { get; set; }
        public string Vorname { get; set; }
        public List<string> Abteilungen { get; set; }
        public int Alter { get; set; }
        public string Datum { get; set; }
    }
}
