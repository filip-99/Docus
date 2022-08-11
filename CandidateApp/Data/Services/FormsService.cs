using CandidateApp.Data.Models;
using CandidateApp.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateApp.Data.Services
{
    public class FormsService
    {
        private AppDbContext _context;

        public FormsService(AppDbContext context)
        {
            _context = context;
        }

        public void AddForm(FormVM form)
        {
            var _form = new Form()
            {
                Name = form.Name,
                Active = form.Active
            };
            _context.Forms.Add(_form);
            _context.SaveChanges();
        }

        public FormWithCandidatesVM GetFormWithCandidates(int formId)
        {
            var _form = _context.Forms.Where(form => form.Id == formId).Select(n => new FormWithCandidatesVM()
            {
                Name = n.Name,
                CandidateName = n.FormAction.Select(n => n.Candidate.Name).ToList()
            }).FirstOrDefault();
            return _form;
        }
    }
}