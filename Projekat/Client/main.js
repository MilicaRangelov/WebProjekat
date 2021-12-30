import { Galerija } from "./Galerija.js";
import { Karte } from "./Karte.js";
import { Izlozbe } from "./Izlozbe.js";
import { Umetnici } from "./Umetnici.js";

var listaIzlozbi = [];
var listaUmetnika = [];

fetch("https://localhost:5001/Izlozba/PrikaziIzlozbe")
.then(p =>{

    p.json().then(izlozbe =>{

        izlozbe.forEach(izlozba => {

            var i = new Izlozbe(izlozba.id, izlozba.naziv, izlozba.datumPoc, izlozba.datumaKraja);
            listaIzlozbi.push(i);
            
        });

        fetch("https://localhost:5001/Umetnik/PrikaziUmetnike")
        .then(p =>{
        
            p.json().then(umetnici =>{
        
                umetnici.forEach(umetnik => {
        
                    var u = new Umetnici(umetnik.id, umetnik.ime, umetnik.umetnickoIme, umetnik.prezime, umetnik.drzavaRodjenja);
                    listaUmetnika.push(u);
                    
                });
        
                console.log(listaIzlozbi);
        
                var galerija = new Galerija(listaIzlozbi,listaUmetnika);
                galerija.crtaj(document.body);
        
            })
        
        })
        

    })

})




