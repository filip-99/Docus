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
    }
}