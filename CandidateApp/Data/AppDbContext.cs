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
        // Sada u konstruktoru definišemo da pomoću ove klase AppDbContext možemo koristiti sve članove osnovne klase u ovoj izvedenoj klasi pa zato ovo base(options)
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        // Potrebno je sada da konfigurišemo ova 3 modela (Author.cs, Book.cs i Book_Author.cs) kako bi uspešno apdejtovali bazu
        // Ovo se konfiguriše korišćenjem Fluent API-ja za Entity Framework Core
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Sada definišemo odnos između Candidate i FormAction
            modelBuilder.Entity<FormAction>()
                // HasOne - jedan kandidat
                .HasOne(c => c.Candidate)
                // WithMany - Može imati više FormAction (više je na strani FormAction)
                .WithMany(fa => fa.FormAction)
                // Sada definišemo javni ključ
                .HasForeignKey(fa => fa.CandidateId);

            // Sada isto ovako definišemo odnos između Form i FormAction
            modelBuilder.Entity<FormAction>()
                .HasOne(a => a.Form)
                .WithMany(ba => ba.FormAction)
                .HasForeignKey(ai => ai.FormId);
        }

        // Sada vršimo referenciranje model klase sa tabelom u bazi podataka
        // Promenjiva će biti tipa DbSet i nazvaćemo je kao tabelu u bazi Books. Sada možemo manipulisati tabelom uz pomoć ovog naziva "Books"
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<FormAction> FormsActions { get; set; }
    }
}