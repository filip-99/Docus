using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateApp.Data.ViewModels
{
    public class FormVM
    {
        public string Name { get; set; }
        public bool Active { get; set; }
    }

    public class FormWithCandidatesVM
    {
        public string Name { get; set; }
        public List<string> CandidateName { get; set; }

    }
}
