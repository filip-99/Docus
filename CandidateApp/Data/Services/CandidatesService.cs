using CandidateApp.Data.Models;
using CandidateApp.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateApp.Data.Services
{
    // Servis klasa je posrednik i ona komunicira sa kontekst klasom, koja komunicira sa bazom, a njene metode će okidati kontroler klasa BookControler
    public class CandidatesService
    {
        // Prvo će mo imati metodu za ubacivanje knjiga u bazi
        // Ovo će mo uraditi znači preko kontekst klase, jer ona komunicira sa bazom. Pa kreiramo najpre objekat, pa konstruktor sa ovim poljem
        private AppDbContext _context;

        public CandidatesService(AppDbContext context)
        {
            _context = context;
        }

        // Pošto želimo da ubacimo podatke u bazu kreiramo metodu:
        public void AddBook(CandidateVM candidate)
        {
            // Kreiraćemo objekat modela knjige tj. Model Book
            // Možemo pisati Book ili var kao što je navedeno
            var _candidate = new Candidate()
            {
                Name = candidate.Name,
                EmailId = candidate.EmailId,
                PhoneNumber = candidate.PhoneNumber,
                AdressLine1 = candidate.AdressLine1,
                AdressLine2 = candidate.AdressLine2,
                Active = candidate.Active
            };
            // Sada kada smo uzeli prosleđene podatke funkciji, dodelili im vrednosti, potrebno je da ih prosledimo kontekst klasi
            _context.Candidates.Add(_candidate);
            _context.SaveChanges();
        }
    }
}