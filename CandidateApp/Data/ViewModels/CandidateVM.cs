using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateApp.Data.ViewModels
{
    // CandidateVM je ViewModel klasa koja sadrži deo podataka iz model klase Book.cs
    // Podaci u ovoj klasi su oni koje unosi korisnik, zato je izostavljen Id jer je identity i recimo datum unosa knjige u bazu, jer se uzima trenutni datum i korisnik ga ne unosi
    public class CandidateVM
    {
        public String Name { get; set; }
        public string EmailId { get; set; }
#nullable enable
        public string? PhoneNumber { get; set; }
        public string? AdressLine1 { get; set; }
        public string? AdressLine2 { get; set; }
        public bool? Active { get; set; }
#nullable disable
    }
}
