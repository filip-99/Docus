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

        // Kreiramo HTTP metodu za prikaz svih kandidata u bazi
        // Nazvaćemo API krajnju tačku get-all-candidates
        [HttpGet("get-all-candidates")]
        public IActionResult GetAllCandidates()
        {
            var allCandidates = _candidatesService.GetAllCandidates();
            return Ok(allCandidates);
        }

        // Kreiramo sada API End-Point tj. krajnju tačku za vraćanje samo jednog kandidata iz baze
        // Kroz HttpGet zahtev prosleđujemo id da bi dobili podatke o kandidatu sa tim id-em
        // Bitno je da bude tačan naziv jer će API End-Point morati da mapira parametar sa parametrom u metodi
        [HttpGet("get-candidate-by-id/{id}")]
        public IActionResult GetCandidateById(int id)
        {
            var candidate = _candidatesService.GetCandidateById(id);
            return Ok(candidate);
        }

        // Kreiranje API End-Pointa koji vraća aktivne kandidate
        [HttpGet("get-candidate-is-active")]
        public IActionResult GetActiveCandidates()
        {
            var candidate = _candidatesService.GetActiveCandidates(true);
            return Ok(candidate);
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

        [HttpPut("update-book-by-id/{id}")]
        public IActionResult UpdateCandidateById(int id, [FromBody] CandidateVM candidate){
            var updateCandidate = _candidatesService.UpdateCandidateById(id, candidate);
            return Ok(updateCandidate);
        }
    }
}