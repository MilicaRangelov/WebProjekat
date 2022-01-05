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
    public class IzlozbaController : ControllerBase
    {

        public GalerijaContext Context { get; set; }

        public IzlozbaController(GalerijaContext context)
        {
            Context = context;
        }

        [Route("PrikaziIzlozbeGalerije/idGalerije")]
        [HttpGet]

        public async Task<ActionResult> PrikaziIzlozbeGalerije(int idGalerije){

            if(idGalerije < 0){

                return BadRequest("Pogresan id");
            }

            try{

                var izlozbe = await Context.Izlozbe.Where(p=> p.Galerija.ID == idGalerije && p.DatumPocetka.CompareTo(DateTime.Now.Date) > 0).ToListAsync();

                return Ok(izlozbe.Select(p=> new {

                    Id = p.ID,
                    Naziv = p.NazivIzlozbe,
                    DatumPoc = p.DatumPocetka.ToShortDateString(),
                    DatumKraja = p.DatumKraja.ToShortDateString(),
                    BrojKarata = Context.Karte.Where(q => q.Izlozba.ID == p.ID).ToList().Count,

                }).ToList());

            }catch(Exception ex){

                return BadRequest(ex.Message);
            }

        }

        [Route("DodajIzlozbe/{idGalerije}/{naziIzlozbe}/{datumPocetka}/{datumKraja}")]
        [HttpPost]

          public async Task<ActionResult> DodajIzlozbu(int idGalerije,string naziIzlozbe, DateTime datumPocetka, DateTime datumKraja)
        {
            if (string.IsNullOrWhiteSpace(naziIzlozbe) || naziIzlozbe.Length > 50)
            {
                return BadRequest("Pogresan naslov");
            }

            try
            {

                var galerija = Context.Galerije.Where(p=>p.ID ==idGalerije).FirstOrDefault();

                Izlozba izlozba = new Izlozba();
                izlozba.NazivIzlozbe = naziIzlozbe;
                izlozba.DatumKraja = datumKraja;
                izlozba.DatumPocetka = datumPocetka;
                izlozba.Galerija = galerija;

                Context.Izlozbe.Add(izlozba);
                await Context.SaveChangesAsync();
                return Ok("Uspesno je dodata izlozba");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("ObrisiIzlozbu/{id}")]
        [HttpDelete]
        public async Task<ActionResult> ObrisiIzlozbu(int id)
        {
            if (id <= 0)
                return BadRequest("Pogresan id.Mora biti veci od 0");

            try
            {
                var izlozba = await Context.Izlozbe.Where(p => p.ID == id).FirstOrDefaultAsync();
                string naziv = izlozba.NazivIzlozbe;
                Context.Izlozbe.Remove(izlozba);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno ste uklonili  izlozbu: {naziv}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

         [Route("DodajIzlozenaDela/{idDela}/{idIzlozbe}")]
        [HttpPost]
        public async Task<ActionResult> DodajIzlozenDela(int idDela, int idIzlozbe)
        {
            if (idDela <= 0)
                return BadRequest("Pogresan id umetnickog dela");
            if (idIzlozbe <= 0)
                return BadRequest("Pogresan id izlozbe");
            try
            {
                var umetnickoDelo = await Context.UmetnickaDela.Where(p => p.ID == idDela).FirstOrDefaultAsync();
                var izlozba = await Context.Izlozbe.Where(p => p.ID == idIzlozbe).FirstOrDefaultAsync();

                if (umetnickoDelo == null)
                    return BadRequest("Trazeno umetnicko delo ne postoji");
                if (izlozba == null)
                    return BadRequest("Trazena izlozba ne postoji");

                Izlozeno izlozeno = new Izlozeno();
                izlozeno.UmetnickoDelo = umetnickoDelo;
                izlozeno.Izlozba = izlozba;
                Context.DelaIzlozbe.Add(izlozeno);

                await Context.SaveChangesAsync();

                var podaciOIzlozbi = await Context.DelaIzlozbe.Include(p => p.Izlozba)
                                    .Include(p => p.UmetnickoDelo)
                                    .Where(p => p.Izlozba.ID == idIzlozbe)
                                    .Select(p => new
                                    {
                                        NazivIzlozbe = p.Izlozba.NazivIzlozbe,
                                        UmetnickoDelo = p.UmetnickoDelo.Naslov,
                                        GodinaKreiranja = p.UmetnickoDelo.Godina,
                                        UmetnikIme = p.UmetnickoDelo.Umetnik.Ime,
                                        UmetnikPrezime = p.UmetnickoDelo.Umetnik.Prezime

                                    }).ToListAsync();
                return Ok(podaciOIzlozbi);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }



        

    }
}
