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
    public class CandidatesController : ControllerBase
    {

        // Implementiraćemo servis koji smo kreirali, a potom konfigurisali za ubacivanje podataka u bazu
        public CandidatesService _candidatesService;

        public CandidatesController(CandidatesService candidatesService)
        {
            _candidatesService = candidatesService;
        }

        // Sada će mo implementirati EndPoint tj. krajnje tačke
        // Prva će biti HttpPost jer šaljemo podatke u bazu
        // Da bi osigurali da se radi baš o ovoj krajnjoj tački dodelićemo naziv
        [HttpPost("add-candidate")]
        // Tip metode IActionResult || AddBook - naziv metode || [FromBody] uzima se kontekst odnosno telo klase BookVM
        // Prosleđeni parametar [FromBody]BookVM book znači da se očekuje pri unosu da se popune sva polja u klasi BookVM
        public IActionResult AddCandidate([FromBody] CandidateVM candidate)
        {
            _candidatesService.AddBook(candidate);
            // Ok() - ovo je samo jedan od tipova koje metoda može da vrati
            return Ok();
        }
    }
}