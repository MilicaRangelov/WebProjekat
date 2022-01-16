using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors;
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

        [EnableCors("CORS")]
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

        [EnableCors("CORS")]
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

        [EnableCors("CORS")]
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

        [EnableCors("CORS")]
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

        [EnableCors("CORS")]
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

        [EnableCors("CORS")]
        [Route("ObrisiKartuPosetioca/{ime}/{prezime}/{idIzlozbe}/{broj}")]
        [HttpDelete]
        public async Task<ActionResult> ObrisiKartuPosetioca(string ime, string prezime, int idIzlozbe,int broj)
        {
            if(idIzlozbe <= 0)
                return BadRequest("Broj mora biti veci od 0");

            if(string.IsNullOrEmpty(ime) || string.IsNullOrEmpty(prezime))
                return BadRequest("Prazan string");


            
            try
            {
                var izlozba = await Context.Izlozbe.Where(p=>p.ID == idIzlozbe).FirstOrDefaultAsync();
                var karta = await Context.Karte.Where(p=> p.Izlozba.ID == idIzlozbe && p.ImePosetioca.Equals(ime) 
                 && p.PrezimePosetioca.Equals(prezime)).ToListAsync();

                int i =0;
                while(i < broj){
                    Context.Karte.Remove(karta[i]);
                    await Context.SaveChangesAsync();
                    i++;
                }
          
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

        [EnableCors("CORS")]
        [Route("IzmeniKartu/{imePosetioca}/{prezimePosetioca}/{idStareIzlozbe}/{idNoveIzlozbe}/{broj}")]
        [HttpPut]

        public async Task<ActionResult> IzmeniKartu(string imePosetioca, string prezimePosetioca, int idStareIzlozbe,int idNoveIzlozbe, int broj)
        {
            if(string.IsNullOrWhiteSpace(imePosetioca) || imePosetioca.Length > 20)
            {
                return BadRequest("Pogresnan ime posetioca");
            }

            if(string.IsNullOrWhiteSpace(prezimePosetioca) || prezimePosetioca.Length > 20)
            {
                return BadRequest("Pogresno prezime posetioca");
            }

            if(idStareIzlozbe <= 0 || idNoveIzlozbe < 0)
            {
                return BadRequest("Pogresna id izlozbe");
            }
            try
            {
                var karte = await Context.Karte.Where(p=> p.ImePosetioca.Equals(imePosetioca) && p.PrezimePosetioca.Equals(prezimePosetioca) && p.Izlozba.ID == idStareIzlozbe).ToListAsync();
                var izlozba = await Context.Izlozbe.Where(p => p.ID == idNoveIzlozbe).FirstOrDefaultAsync();
        
                if(karte != null && izlozba != null )
                {
                    int i = 0;
                    while( i < broj){
                        karte[i].ImePosetioca = imePosetioca;
                        karte[i].PrezimePosetioca = prezimePosetioca;
                        karte[i].Izlozba = izlozba;
                        await Context.SaveChangesAsync();
                        i++;

                    }

                    var karte2 = await Context.Karte.Where(p=> p.ImePosetioca.Equals(imePosetioca) && p.PrezimePosetioca.Equals(prezimePosetioca) && p.Izlozba.ID == idNoveIzlozbe).ToListAsync();

                    return Ok(
                    karte2.Select(p=> new{
                        Ime = p.ImePosetioca,
                        Prezime = p.PrezimePosetioca,
                        Izlozba = p.Izlozba.NazivIzlozbe,
                        Broj = karte2.Count


                    }).ToList()
                );
                }
                return BadRequest("Neuspesno menjenje karata");

            
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }    
                
        }

        

    }
}
