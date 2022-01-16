
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Projekat2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GalerijaController : ControllerBase
    {
       
        public GalerijaContext Context { get; set; }
        public GalerijaController(GalerijaContext context)
        {
                Context = context;
        }

        [EnableCors("CORS")]
        [Route("Galerija/{id}")]
        [HttpGet]

        public async Task<ActionResult> Galerije(int id){

            if (id < 0){
                return BadRequest("Pogresan id galerije");
            }

            try{
                 return Ok(
                     await Context.Galerije.Where(p=>p.ID == id)
                     .Select(p=> new{

                         id = p.ID,
                         naziv = p.Naziv
                     }).ToListAsync()

                 );

            }
            catch(Exception ex){

                return BadRequest(ex.Message);
            }
          
        }

        [EnableCors("CORS")]
        [Route("GalerijaPrikaz")]
        [HttpGet]

        public async Task<ActionResult> GalerijaPrikaz(){

            try{
                 return Ok(
                     await Context.Galerije
                     .Select(p=> new{

                         id = p.ID,
                         naziv = p.Naziv
                     }).ToListAsync()

                 );

            }
            catch(Exception ex){

                return BadRequest(ex.Message);
            }
          
        }

        [EnableCors("CORS")]
        [Route("DodajGaleriju/{naziv}")]
        [HttpPost]

        public async Task<ActionResult> DodajGaleriju(string naziv){

            if (string.IsNullOrWhiteSpace(naziv)){
                return BadRequest("Unesite naziv galerije");
            }

            try{
               
               Galerija g = new Galerija();
               g.Naziv = naziv;
               Context.Galerije.Add(g);
               await Context.SaveChangesAsync();

               return Ok($"Uspesno dodata galerija pod nazivom: {naziv}");

            }
            catch(Exception ex){

                return BadRequest(ex.Message);
            }
          
        }

        [EnableCors("CORS")]
        [Route("ObrisiGaleriju/{id}")]
        [HttpDelete]

        public async Task<ActionResult> ObrisiGaleriju(int id){

            if (id < 0){
                return BadRequest("Pogresan id");
            }

            try{
               
               Galerija g = await Context.Galerije.Where(p=> p.ID == id).FirstOrDefaultAsync();

               Context.Galerije.Remove(g);
               await Context.SaveChangesAsync();

               return Ok("Uspesno uklonjena galerija");
             
            }
            catch(Exception ex){

                return BadRequest(ex.Message);
            }
          
        }

        [EnableCors("CORS")]
        [Route("PrikaziUmetnike/{id}")]
        [HttpGet]

        public async Task<ActionResult> PrikaziUmetnike(int id){

            if (id < 0){
                return BadRequest("Pogresan id");
            }

            try{
               
               
               var umetniciGalerija = await Context.GalerijaUmetnici
               .Include(p=> p.Galerija)
               .Include(p=>p.Umetnik)
               .Where(p => p.Galerija.ID == id).ToListAsync();

               return Ok(
                   umetniciGalerija.Select(p => new{

                       Id =p.Umetnik.ID,
                       Ime = p.Umetnik.Ime,
                       UmetnickoIme = p.Umetnik.UmetnickoIme,
                       Prezime = p.Umetnik.Prezime,
                       DrzavaRodjenja = p.Umetnik.DrzavaRodjenja

                   }).ToList()
               );


               

            }
            catch(Exception ex){

                return BadRequest(ex.Message);
            }
          
        }


        [EnableCors("CORS")]
        [Route("PrikaziDela/{id}")]
        [HttpGet]

        public async Task<ActionResult> PrikaziDela(int id){

            if (id < 0){
                return BadRequest("Pogresan id");
            }

            try{

               return Ok(
                    await Context.UmetnickaDela.Where(p => p.Galerija.ID == id)
                   .Select(p => new{

                       Id =p.ID,
                       Naslov = p.Naslov,
                       Godina = p.Godina,
                       TipDela = p.TipDela,
                       Ime = p.Umetnik.Ime,
                       UmetnickoIme = p.Umetnik.UmetnickoIme,
                       Prezime = p.Umetnik.Prezime,
                       DrzavaRodjenja = p.Umetnik.DrzavaRodjenja

                   }).ToListAsync()
               );


               

            }
            catch(Exception ex){

                return BadRequest(ex.Message);
            }
          
        }


        
    }
}
