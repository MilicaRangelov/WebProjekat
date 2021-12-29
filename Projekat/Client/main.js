import { Galerija } from "./Galerija.js";
import { Karte } from "./Karte.js";
import { Izlozbe } from "./Izlozbe.js";

var listaIzlozbi = [];
var listaDela = [];

fetch("https://localhost:5001/Izlozba/PrikaziIzlozbe")
.then(p =>{

    p.json().then(izlozbe =>{

        izlozbe.forEach(izlozba => {

            var i = new Izlozbe(izlozba.id, izlozba.naziv, izlozba.datumPoc, izlozba.datumaKraja);
            listaIzlozbi.push(i);
            
        });

        console.log(listaIzlozbi);

        var galerija = new Galerija(listaIzlozbi,listaDela);
        galerija.crtaj(document.body);

    })

})

