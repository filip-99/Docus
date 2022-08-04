using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateApp.Data.Models
{
    public class FormAction
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public int FormId { get; set; }
        public string Action { get; set; }
        public DateTime ActionOn { get; set; }

        // Definišemo odnos između tabela
        public Candidate Candidate { get; set; }
        public Form Form { get; set; }
    }
}
