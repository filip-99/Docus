using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateApp.Data.Models
{
    public class Candidate
    {
        public int Id { get; set; }
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
