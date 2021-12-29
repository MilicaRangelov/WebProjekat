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
    public class KarteController : ControllerBase
    {

        public GalerijaContext Context { get; set; }

        public KarteController(GalerijaContext context)
        {
            Context = context;
        }

        [Route("PrikaziKarte")]
        [HttpGet]
        public async Task<ActionResult> PrikaziKarte() => 
        Ok(await Context.Karte.Select(p => new
        {
           Id = p.ID,
           imePosetioca = p.ImePosetioca,
           prezimePosetioca = p.PrezimePosetioca,
           nazivIzlozbe = p.Izlozba.NazivIzlozbe,
           datumPoc = p.Izlozba.DatumPocetka.ToShortDateString(),
           datumKraja = p.Izlozba.DatumKraja.ToShortDateString()

        }).ToListAsync());


        [Route("KarteJedeneIzlozbe")]
        [HttpGet]

        public async Task<ActionResult> KarteJedeneIzlozbe([FromQuery] int idIzlozbe)
        {
            if(idIzlozbe <= 0)
                return BadRequest("Ne postoji trazena izlozba");

            try
            {
                return Ok( await Context.Karte.Where(q => q.Izlozba.ID == idIzlozbe)
                .Select ( p => new{

                    id = p.ID,
                    imePosetica = p.ImePosetioca,
                    prezimePosetioca = p.PrezimePosetioca,
                    naziv = p.Izlozba.NazivIzlozbe,
                    datumPoc = p.Izlozba.DatumPocetka.ToShortDateString(),
                    datumKraja = p.Izlozba.DatumKraja.ToShortDateString()
                
                }).ToListAsync());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("KartaPosetioca/{imePosetioca}/{prezimePosetioca}/{idIzlozbe}")]
        [HttpGet]

        public async Task<ActionResult> KartaPosetioca(string imePosetioca, string prezimePosetioca, int idIzlozbe)
        {
            if(idIzlozbe <= 0)
                return BadRequest("Ne postoji trazena izlozba");

            if(imePosetioca == null){
                return BadRequest("Morate uneti ime posetioca");
            }    

            if(prezimePosetioca == null){

                return BadRequest("Morate uneti ime posetioca");
            }   

            try
            {
                var izlozba = await Context.Izlozbe.Where(p=>p.ID == idIzlozbe).FirstOrDefaultAsync();
                var karta = await Context.Karte.Where(p=> p.Izlozba.ID == idIzlozbe && p.ImePosetioca.Equals(imePosetioca) 
                 && p.PrezimePosetioca.Equals(prezimePosetioca)).ToListAsync();
                
                return Ok(
                    karta.Select(p=> new{
                        Ime = p.ImePosetioca,
                        Prezime = p.PrezimePosetioca,
                        Izlozba = p.Izlozba.NazivIzlozbe,
                        Broj = karta.Count


                    }).ToList()
                );
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("DodajKartu/{imePosetioca}/{prezimePosetioca}/{idIzlozbe}")]
        [HttpPost]

        public async Task<ActionResult> DodajKartu(string imePosetioca, string prezimePosetioca, int idIzlozbe)
        {
            if(string.IsNullOrWhiteSpace(imePosetioca) || imePosetioca.Length > 20)
            {
                return BadRequest("Pogresan ime posetioca");
            }

            if(string.IsNullOrWhiteSpace(prezimePosetioca) || prezimePosetioca.Length > 20)
            {
                return BadRequest("Pogresan prezime posetioca");
            }

            if(idIzlozbe <= 0)
            {
                return BadRequest("Pogresan id izlozbe");
            }
        
    
            try
            {
                var izlozba = await Context.Izlozbe.Where(p => p.ID == idIzlozbe).FirstOrDefaultAsync();
                if(izlozba != null)
                {
                    Karta karta = new Karta();
                    karta.ImePosetioca = imePosetioca;
                    karta.PrezimePosetioca =prezimePosetioca;
                    karta.Izlozba = izlozba;
                    Context.Karte.Add(karta);
                    await Context.SaveChangesAsync();

                var karte  = await Context.Karte.Where(p=> p.Izlozba.ID == idIzlozbe && p.ImePosetioca.Equals(imePosetioca) 
                 && p.PrezimePosetioca.Equals(prezimePosetioca)).ToListAsync();

                    return Ok(
                    karte.Select(p=> new{
                        Ime = p.ImePosetioca,
                        Prezime = p.PrezimePosetioca,
                        Izlozba = p.Izlozba.NazivIzlozbe,
                        Broj = karte.Count


                    }).ToList());
                }

                return BadRequest("Ne postoji trazena izlozba");
               
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("ObrisiKartu/{id}")]
        [HttpDelete]
        public async Task<ActionResult> ObrisiKartu(int id)
        {
            if(id <= 0)
                return BadRequest("Pogresan id.Mora biti veci od 0");
            
            try
            {
                var karta = await Context.Karte.FindAsync(id);
                string imePosetioca = karta.ImePosetioca;
                string prezimePosetioca = karta.PrezimePosetioca;
                string izlozba = karta.Izlozba.NazivIzlozbe;
                Context.Karte.Remove(karta);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno ste otkazali kartu: {imePosetioca} {prezimePosetioca}  za izlozbu {izlozba}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("ObrisiKartuPosetioca/{ime}/{prezime}/{idIzlozbe}")]
        [HttpDelete]
        public async Task<ActionResult> ObrisiKartuPosetioca(string ime, string prezime, int idIzlozbe)
        {
            if(idIzlozbe <= 0)
                return BadRequest("Pogresan id.Mora biti veci od 0");
            
            try
            {
                var izlozba = await Context.Izlozbe.Where(p=>p.ID == idIzlozbe).FirstOrDefaultAsync();
                var karta = await Context.Karte.Where(p=> p.Izlozba.ID == idIzlozbe && p.ImePosetioca.Equals(ime) 
                 && p.PrezimePosetioca.Equals(prezime)).FirstOrDefaultAsync();
                
                Context.Karte.Remove(karta);
                await Context.SaveChangesAsync();
                var karte = await Context.Karte.Where(p=> p.Izlozba.ID == idIzlozbe && p.ImePosetioca.Equals(ime) 
                 && p.PrezimePosetioca.Equals(prezime)).ToListAsync();
                
                return Ok(
                    karte.Select(p=> new{
                        Ime = p.ImePosetioca,
                        Prezime = p.PrezimePosetioca,
                        Izlozba = p.Izlozba.NazivIzlozbe,
                        Broj = karte.Count


                    }).ToList()
                );
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("IzmeniKartu/{id}/{imePosetioca}/{prezimePosetioca}/{idIzlozbe}")]
        [HttpPut]

        public async Task<ActionResult> IzmeniKartu(int id, string imePosetica, string prezimePosetioca, int idIzlozbe)
        {
            if(id <= 0)
                return BadRequest("Pogresna vrednost id-ja");

            if(string.IsNullOrWhiteSpace(imePosetica) || imePosetica.Length > 20)
            {
                return BadRequest("Pogresnan ime posetioca");
            }

            if(string.IsNullOrWhiteSpace(prezimePosetioca) || prezimePosetioca.Length > 20)
            {
                return BadRequest("Pogresno prezime posetioca");
            }

            if(idIzlozbe <= 0)
            {
                return BadRequest("Pogresna id izlozbe");
            }
            try
            {
                var karta = await Context.Karte.FindAsync(id);
                var izlozba = await Context.Izlozbe.Where(p => p.ID == idIzlozbe).FirstOrDefaultAsync();
                if(karta != null && izlozba != null)
                {
                   karta.ImePosetioca = imePosetica;
                   karta.PrezimePosetioca = prezimePosetioca;
                   karta.Izlozba = izlozba;

                    await Context.SaveChangesAsync();
                    return Ok("Uspesno je izmenjena karta");
                }

                return BadRequest("Ne postoji karta sa trazenik id-jem");
            
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }    
                
        }

        

    }
}
