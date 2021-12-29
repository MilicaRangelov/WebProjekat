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
    public class UmetnickoDeloController : ControllerBase
    {

        public GalerijaContext Context { get; set; }

        public UmetnickoDeloController(GalerijaContext context)
        {
            Context = context;
        }

        [Route("PrikaziUmetnickaDela")]
        [HttpGet]
        public async Task<ActionResult> PrikaziUmetnickaDela() => 
        Ok(await Context.UmetnickaDela.Select(p => new
        {
            Id = p.ID,
            Naslov = p.Naslov,
            Godina = p.Godina,
            TipDela = p.TipDela,
            ImeUmetnika = p.Umetnik.Ime,
            UmetnickoImeUmetnika = p.Umetnik.UmetnickoIme,
            PrezimeUmetnika = p.Umetnik.Prezime,
            DrzavaRodjUmetnika = p.Umetnik.DrzavaRodjenja

        }).ToListAsync());


        [Route("DelaUmetnika")]
        [HttpGet]

        public async Task<ActionResult> DelaUmetnika([FromQuery] int idUmetnika)
        {
            if(idUmetnika <= 0)
                return BadRequest("Ne postoji umetnik sa zadatim id-jem");

            try
            {
                return Ok( await Context.UmetnickaDela.Where(q => q.Umetnik.ID == idUmetnika)
                .Select ( p => new{

                    id = p.ID,
                    naslov = p.Naslov,
                    godina = p.Godina,
                    tipDela = p.TipDela,
                    ime = p.Umetnik.Ime,
                    prezime = p.Umetnik.Prezime
                
                }).ToListAsync());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("DodajUmetnickoDelo/{naslov}/{godina}/{tipDela}/{idUmetnika}")]
        [HttpPost]

        public async Task<ActionResult> DodajUmetnickoDelo(string naslov, int godina, string tipDela, int idUmetnika)
        {
            if(string.IsNullOrWhiteSpace(naslov) || naslov.Length > 50)
            {
                return BadRequest("Pogresan naslov");
            }

            if(string.IsNullOrWhiteSpace(tipDela) || tipDela.Length > 15)
            {
                return BadRequest("Pogresan tip dela");
            }

            if(idUmetnika <= 0)
            {
                return BadRequest("Pogresan id umetnika");
            }
        
            if(godina.ToString().Length > 4)
            {
                return BadRequest("Pogresna godina");
            }

            try
            {
                var umetnik = await Context.Umetnici.Where(p => p.ID == idUmetnika).FirstOrDefaultAsync();
                if(umetnik != null)
                {
                    UmetnickoDelo delo = new UmetnickoDelo();
                    delo.Naslov = naslov;
                    delo.Godina = godina;
                    delo.TipDela = tipDela;
                    delo.Umetnik = umetnik;
                    Context.UmetnickaDela.Add(delo);
                    await Context.SaveChangesAsync();
                    return Ok("Uspesno dodato delo zadatog umetnika");
                }

                return BadRequest("Ne postoji trazeni umetnik");
               
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("ObrisiUmetnickoDelo/{id}")]
        [HttpDelete]
        public async Task<ActionResult> ObrisiUmetnickoDelo(int id)
        {
            if(id <= 0)
            return BadRequest("Pogresan id.Mora biti veci od 0");
            
            try
            {
                var delo = await Context.UmetnickaDela.FindAsync(id);
                string naslov = delo.Naslov;
                string tip = delo.TipDela;
                Context.UmetnickaDela.Remove(delo);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno ste uklonili  umetnika: {naslov} {tip}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("IzmeniUmenickoDelo/{id}/{naslov}/{tipDela}/{godina}/{idUmetnika}")]
        [HttpPut]

        public async Task<ActionResult> IzmeniUmenickoDelo(int id, string naslov, string tipDela, int godina, int idUmetnika)
        {
            if(id <= 0)
                return BadRequest("Pogresna vrednost id-ja");

            if(string.IsNullOrWhiteSpace(naslov) || naslov.Length > 50)
            {
                return BadRequest("Pogresnan naslov");
            }

            if(string.IsNullOrWhiteSpace(tipDela) || tipDela.Length > 15)
            {
                return BadRequest("Pogresno prezime");
            }
            
            if(godina.ToString().Length > 4 || godina < 0)
            {
                return BadRequest("Pogresna godina");
            }

            if(idUmetnika <= 0)
            {
                return BadRequest("Pogresna id umetnika");
            }
            try
            {
                var umetnik = await Context.Umetnici.FindAsync(idUmetnika);
                var delo = await Context.UmetnickaDela.Where(p => p.ID == id).FirstOrDefaultAsync();
                if(umetnik != null && delo != null)
                {
                    delo.Naslov = naslov;
                    delo.TipDela = tipDela;
                    delo.Godina = godina;
                    delo.Umetnik = umetnik;

                    await Context.SaveChangesAsync();
                    return Ok("Uspesno je izmenjeno umetnicko delo");
                }

                return BadRequest("Ne postoji umetnik sa trazenik id-jem");
            
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }    
                
        }

        

    }
}
