using CandidateApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateApp.Data
{
    // Nasledili smo klasu DbContext što znači da možemo koristiti njene metode i ostalo za komunikaciju sa bazom
    public class AppDbContext : DbContext
    {
        // Kreiramo konstruktor i da bi uključio potrebne elemente u njega, treba da izgleda ovako:
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        // Sada vršimo referenciranje model klase sa tabelom u bazi podataka
        // Promenjiva će biti tipa DbSet i nazvaćemo je kao tabelu u bazi Books. Sada možemo manipulisati tabelom uz pomoć ovog naziva "Books"
        public DbSet<Candidate> Candidates { get; set; }
    }
}