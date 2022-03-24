using System;
using System.Collections.Generic;

#nullable disable

namespace PlanerixIPA.Models
{
    public partial class Dokument
    {
        public int DokumentId { get; set; }
        public string Bezeichnung { get; set; }
        public string Pfad { get; set; }
    }
}
