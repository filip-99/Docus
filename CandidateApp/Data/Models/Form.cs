using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateApp.Data.Models
{
    public class Form
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        // Dodajemo odnos između klase FormAction
        public List<FormAction> FormAction { get; set; }
    }
}
