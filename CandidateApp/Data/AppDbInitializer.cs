using CandidateApp.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateApp.Data
{
    // Ovu klasu će mo koristiti da preko nje ubacujemo podatke u bazu
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            // Osiguravamo da će se konekcija sa bazom zatvoriti van ovih zagrada vitičastih
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                // Sada kreiramo promenjivu i deklarišemo je kao kontekst. Što nam omogućava rad sa bazom podataka
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                // Sada uz pomoć konteksta proveravamo da li u bazi ima podataka tj. knjiga
                // Tj. Da li nema knjiga. Ako nema dodaćemo
                if (!context.Candidates.Any())
                {
                    // Pošto želimo da unesemo više od jedne knjige u bazu, potrebno je da dodamo opseg tj. ovaj AddRange metod
                    context.Candidates.AddRange(new Candidate()
                    {
                        // Id se ne dodaje jer je identity, pa će samo da krene od 1 kako dodajemo knjige u tabelu
                        Name = "Marko",
                        EmailId = "1",
                        PhoneNumber = "064153759",
                        AdressLine1 = "Rajićeva bb",
                        AdressLine2 = "",
                        Active = true,

                    },
                    new Candidate()
                    {
                        Name = "Petar",
                        EmailId = "2",
                        PhoneNumber = "064751359",
                        AdressLine1 = "Rajićeva 16",
                        AdressLine2 = "10 Oktobar",
                        Active = false,
                    });
                    // Takođe da bi smo sačuvali ovaj niz moramo sačuvati ovaj kontekst
                    context.SaveChanges();
                }

            }
        }
    }
}