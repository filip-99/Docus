using CandidateApp.Data.Services;
using CandidateApp.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormsController : ControllerBase
    {

        private FormsService _formsService;

        public FormsController(FormsService formsService)
        {
            _formsService = formsService;
        }


        [HttpPost("add-form")]
        public IActionResult AddForm([FromBody] FormVM form)
        {
            _formsService.AddForm(form);
            return Ok();
        }

        [HttpGet("get-form-with-candidates-by-id/{id}")]
        public IActionResult GetFormWithCandidatesById(int id)
        {
            var response = _formsService.GetFormWithCandidates(id);
            return Ok(response);
        }
    }
}
