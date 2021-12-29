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

        [Route("PrikaziIzlozbe")]
        [HttpGet]
        public async Task<ActionResult> PrikaziIzlozbe() =>
        Ok(await Context.Izlozbe.Select(p => new
        {
            Id = p.ID,
            Naziv = p.NazivIzlozbe,
            DatumPoc = p.DatumPocetka.ToShortDateString(),
            DatumKraja = p.DatumKraja.ToShortDateString(),
            BrojKarata = Context.Karte.Where(q => q.Izlozba.ID == p.ID).ToList().Count,

        }).ToListAsync());

        [Route("Izlozba/{idIzlozbe}")]
        [HttpGet]

        public async Task<ActionResult> Izlozba(int idIzlozbe){

             if(idIzlozbe < 0){
                 return BadRequest("Pogresan id izlozbe");
            }

            try{

                return Ok( await Context.Izlozbe.Where(q => q.ID == idIzlozbe)
                .Select ( p => new{
                    id = p.ID,
                    naslov = p.NazivIzlozbe,
                    datumPocetka = p.DatumPocetka.ToShortDateString(),
                    datumKraja = p.DatumKraja.ToShortDateString(),
                    BrojKarata = Context.Karte.Where(k => k.Izlozba.ID == idIzlozbe).ToList().Count
                
                }).ToListAsync());

            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }

        }


        [Route("IzlozenaDela/{idIzlozbe}")]
        [HttpGet]

        public async Task<ActionResult> IzlozenaDela(int idIzlozbe){

            if(idIzlozbe < 0){

                return BadRequest("Id izlozbe je manji od 0");
            }

            try{

                var dela = await Context.DelaIzlozbe
                        .Include(p=> p.UmetnickoDelo)
                        .Include(p=>p.Izlozba)
                        .Where( p => p.Izlozba.ID == idIzlozbe).ToListAsync();

                return Ok(

                    dela.Select(
                        p=> new{
                            Naslov = p.UmetnickoDelo.Naslov,
                            Kreirano = p.UmetnickoDelo.Godina,
                            Tip = p.UmetnickoDelo.TipDela,
                            Izlozba = p.Izlozba.NazivIzlozbe
                           
                        }).ToList()
                );

            }
            catch(Exception ex){

                return BadRequest(ex.Message);
            }


        }


        [Route("DodajIzlozbu/{naziv}/{datumPocetka}/{datumKraja}")]
        [HttpPost]

      
        public async Task<ActionResult> DodajIzlozbu(string naziv, DateTime datumPocetka, DateTime datumKraja)
        {
            if (string.IsNullOrWhiteSpace(naziv) || naziv.Length > 50)
            {
                return BadRequest("Pogresan naslov");
            }

            try
            {

                Izlozba izlozba = new Izlozba();
                izlozba.NazivIzlozbe = naziv;
                izlozba.DatumKraja = datumKraja;
                izlozba.DatumPocetka = datumPocetka;

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

        [Route("IzmeniIzlozbu/{id}/{naziv}/{datumPocetka}/{datumKraja}")]
        [HttpPut]

        public async Task<ActionResult> IzmeniIzlozbu(int id, string naziv, DateTime datumPocetka, DateTime datumKraja)
        {
            if (id <= 0)
                return BadRequest("Pogresna vrednost id-ja");

            if (string.IsNullOrWhiteSpace(naziv) || naziv.Length > 50)
            {
                return BadRequest("Pogresnan naziv");
            }

            try
            {

                var izlozba = await Context.Izlozbe.Where(p => p.ID == id).FirstOrDefaultAsync();
                izlozba.NazivIzlozbe = naziv;
                izlozba.DatumPocetka = datumPocetka;
                izlozba.DatumKraja = datumKraja;
                await Context.SaveChangesAsync();
                return Ok("Uspesno je izmenjena izlozba");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        // dodavanje dela koja su bila izlozena na zadatoj izlozbi

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
