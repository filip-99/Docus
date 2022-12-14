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
        public CandidateWithFormsVM GetCandidateWithFormById(int candidateId)
        {
            var _candidateWithForm = _context.Candidates.Where(n => n.Id == candidateId).Select(candidate => new CandidateWithFormsVM()
            {
                Name = candidate.Name,
                EmailId = candidate.EmailId,
                PhoneNumber = candidate.PhoneNumber,
                AdressLine1 = candidate.AdressLine1,
                AdressLine2 = candidate.AdressLine2,
                Active = candidate.Active,
                FormsName = candidate.FormAction.Select(n => n.Form.Name).ToList()
            }).FirstOrDefault();
            return _candidateWithForm;
        }

        // Kreiramo metodu za prikazivanje svih aktivnih kandidata
        public List<Candidate> GetActiveCandidates(bool candidateIsActive) => _context.Candidates.ToList().FindAll(n => n.Active == candidateIsActive);

        public Candidate UpdateCandidateById(int id, CandidateVM candidate)
        {
            var _candidate = _context.Candidates.FirstOrDefault(n => n.Id == id);
            if (_candidate != null)
            {
                _candidate.Name = candidate.Name;
                _candidate.EmailId = candidate.EmailId;
                _candidate.PhoneNumber = candidate.PhoneNumber;
                _candidate.AdressLine1 = candidate.AdressLine1;
                _candidate.AdressLine2 = candidate.AdressLine2;
                _candidate.Active = candidate.Active;

                _context.SaveChanges();
            }
            // Kao rezultat vraćamo apdejtovan red u tabeli
            return _candidate;
        }

        // Pošto želimo da ubacimo podatke u bazu kreiramo metodu:
        public void AddCandidateWithForms(CandidateVM candidate)
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

            foreach (var id in candidate.FormsId)
            {
                var _form_action = new FormAction()
                {
                    CandidateId = _candidate.Id,
                    FormId = id
                };
                _context.FormsActions.Add(_form_action);
                _context.SaveChanges();
            }
        }

        // Kreiramo metodu za brisanje podataka iz baze
        // Metoda će biti void jer ne želimo da nam vraća izbrisanog kandidata
        public void DeleteCandidateById(int candidateId)
        {
            var _candidate = _context.Candidates.FirstOrDefault(n => n.Id == candidateId);
            if (_candidate != null)
            {
                // Sada kada promenjiva _candidate referencira na red u tabeli sa zadatim ID-em, obrisaćemo taj red iz baze
                _context.Candidates.Remove(_candidate);
                _context.SaveChanges();
            }
        }
    }
}