using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace Projekat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UmetnikController : ControllerBase
    {

        public GalerijaContext Context { get; set; }

        public UmetnikController(GalerijaContext context)
        {
            Context = context;
        }

        [Route("PrikaziUmetnike")]
        [HttpGet]
        public async Task<ActionResult> PrikaziUmetnike() => Ok(await Context.Umetnici.Select(p => new
        {
            id = p.ID,
            ime = p.Ime,
            umetnickoIme = p.UmetnickoIme,
            prezime = p.Prezime,
            drzavaRodjenja = p.DrzavaRodjenja

        }).ToListAsync());

        [Route("Umetnik/{idUmetnika}")]
        [HttpGet]

        public async Task<ActionResult> Izlozba(int idUmetnika){

             if(idUmetnika < 0){
                 return BadRequest("Pogresan id izlozbe");
            }

            try{

                return Ok( await Context.Umetnici.Where(q => q.ID == idUmetnika)
                .Select ( p => new{
                    id = p.ID,
                    ime = p.Ime,
                    umetnickoIme = p.UmetnickoIme,
                    prezime = p.Prezime,
                    drzavaRodj = p.DrzavaRodjenja
                
                }).ToListAsync());

            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }

        }


        [Route("DodajUmetnika/{ime}/{umetnickoIme}/{prezime}/{drzavaRodjenja}")]
        [HttpPost]

        public async Task<ActionResult> DodajUmetnika(string ime, string umetnickoIme, string prezime, string drzavaRodjenja)
        {
            if(string.IsNullOrWhiteSpace(ime) ||  ime.Length > 20)
            {
                return BadRequest("Pogresno ime");
            }

            if(string.IsNullOrWhiteSpace(prezime) || prezime.Length > 20)
            {
                return BadRequest("Pogresno prezime");
            }
            
            if(string.IsNullOrWhiteSpace(umetnickoIme) || umetnickoIme.Length > 20)
            {
                return BadRequest("Pogresno umetnicko ime");
            }

            if(string.IsNullOrWhiteSpace(drzavaRodjenja) || drzavaRodjenja.Length > 30)
            {
                return BadRequest("Pogresna drzava rodjenja");
            }
            try
            {
                Umetnik u = new Umetnik();
                u.Ime = ime;
                u.Prezime = prezime;
                u.UmetnickoIme = umetnickoIme;
                u.DrzavaRodjenja = drzavaRodjenja;
                Context.Umetnici.Add(u);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno je dodat novi umetnik sa id-jem: {u.ID}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("ObrisiUmetnika/{id}")]
        [HttpDelete]
        public async Task<ActionResult> ObrisiUmetnika(int id)
        {
            if(id <= 0)
            return BadRequest("Pogresan id.Mora biti veci od 0");
            
            try
            {
                var umetnik = await Context.Umetnici.FindAsync(id);
                string ime = umetnik.Ime;
                string prezime = umetnik.Prezime;
                Context.Umetnici.Remove(umetnik);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno ste uklonili  umetnika: {ime} {prezime}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("IzmeniUmetnika/{id}/{ime}/{umetnickoIme}/{prezime}/{drzavaRodjenja}")]
        [HttpPut]

        public async Task<ActionResult> IzmeniUmetnika(int id, string ime, string umetnickoIme, string prezime, string drzavaRodjenja)
        {
            if(id <= 0)
                return BadRequest("Pogresna vrednost id-ja");

            if(string.IsNullOrWhiteSpace(ime) || ime.Length > 20)
            {
                return BadRequest("Pogresno ime");
            }

            if(string.IsNullOrWhiteSpace(prezime) || prezime.Length > 20)
            {
                return BadRequest("Pogresno prezime");
            }
            
            if(string.IsNullOrWhiteSpace(umetnickoIme) || umetnickoIme.Length > 20)
            {
                return BadRequest("Pogresno umetnicko ime");
            }

            if(string.IsNullOrWhiteSpace(drzavaRodjenja) || drzavaRodjenja.Length > 30)
            {
                return BadRequest("Pogresna drzava rodjenja");
            }
            try
            {
                var umetnik = await Context.Umetnici.FindAsync(id);
                if(umetnik != null)
                {
                    umetnik.Ime = ime;
                    umetnik.Prezime = prezime;
                    umetnik.DrzavaRodjenja = drzavaRodjenja;

                    await Context.SaveChangesAsync();

                    return Ok($"Uspesno ste izmenili umetnika: {id}. {ime} {umetnickoIme} {prezime}, {drzavaRodjenja}");
                }

                return BadRequest("Ne postoji umetnik sa trazenik id-jem");
            
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }    
                
        }


        [Route("DodajUmetnikaGalerije/{idGalerije}/{idUmetnika}")]
        [HttpPost]
        public async Task<ActionResult> DodajUmetnikaGalerije(int idGalerije, int idUmetnika)
        {
            if (idGalerije <= 0)
                return BadRequest("Pogresan id umetnickog dela");
            if (idUmetnika <= 0)
                return BadRequest("Pogresan id izlozbe");
            try
            {
                var galerija = await Context.Galerije.Where(p => p.ID == idGalerije).FirstOrDefaultAsync();
                var umetnik = await Context.Umetnici.Where(p => p.ID == idUmetnika).FirstOrDefaultAsync();

                if (galerija == null)
                    return BadRequest("Trazeno umetnicko delo ne postoji");
                if (umetnik == null)
                    return BadRequest("Trazena izlozba ne postoji");

                Dostupno dostupno = new Dostupno();
                dostupno.Umetnik = umetnik;
                dostupno.Galerija = galerija;
                Context.GalerijaUmetnici.Add(dostupno);

                await Context.SaveChangesAsync();

             
                return Ok("Uspesno");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }


        

    }
}
