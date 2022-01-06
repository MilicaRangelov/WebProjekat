import { Crtaj } from "./Crtaj.js";
import {Galerija} from "./Galerija.js";

let listaGalerija = [];

fetch("https://localhost:5001/Galerija/GalerijaPrikaz")
.then(p=>{

    p.json().then(galerije =>{

        galerije.forEach(galerija => {

            var g = new Galerija(galerija.id, galerija.naziv);
            listaGalerija.push(g);
            
        });

        var c = new Crtaj(listaGalerija);
        c.crtaj(document.body);
    })

})

