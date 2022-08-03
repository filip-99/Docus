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

        // Sada je potrebno da kreiramo metodu koja će prikazati sve podatke iz baze
        // Metoda je tipa List<> je vraća listu kandidata iz baze
        public List<Candidate> GetAllCandidates() => _context.Candidates.ToList();

        // Sada kreiramo metodu za odabir jednog kandidata iz baze na osnovu njegove aktivnost
        public Candidate GetCandidateById (int candidateId) => _context.Candidates.FirstOrDefault(n => n.Id == candidateId);

        // Kreiramo metodu za prikazivanje svih aktivnih kandidata
        public List<Candidate> GetActiveCandidates(bool candidateIsActive) => _context.Candidates.ToList().FindAll(n => n.Active == candidateIsActive);

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