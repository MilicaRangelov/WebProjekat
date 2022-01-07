import { Galerija } from "./Galerija.js";

export class Crtaj{

    constructor(listaGalerija){
        
        this.listaGalerija = listaGalerija;
        this.kontejner = null;
    }

    obrisiDecu(host){
        if(this.kontejner != null){
            while (this.kontejner.lastChild) {
                this.kontejner.removeChild(this.kontejner.lastChild);
            }

        }

        while (host.lastChild) {
            host.removeChild(host.lastChild);
        }
    }

    crtaj(host){

        this.obrisiDecu(host);

        this.kontejner = document.createElement("div");
        this.kontejner.className = "GlavnaForma";
        host.appendChild(this.kontejner);

        
        let div1= document.createElement("div");
        div1.className = "div1";
        this.kontejner.appendChild(div1);

        let h1 = document.createElement("h1");
        h1.className = "headerGlavne";
        h1.innerHTML = "Choose your gallery";
        div1.appendChild(h1);

        let btnView = document.createElement("button");
        btnView.className = "View";
        btnView.innerHTML = "ViewMore";
        this.kontejner.appendChild(btnView);

        btnView.onclick = (ev) => this.crtaj2(host);
        
    }

    crtaj2(host){

        this.obrisiDecu(host);

        this.kontejner = document.createElement("div");
        this.kontejner.className = "Glavni2";
        host.appendChild(this.kontejner);

        
        this.listaGalerija.forEach(galerija => {
            
            var div = document.createElement("div");
            div.className = "Izlozbe";
            this.kontejner.appendChild(div);

            let p = document.createElement("p");
            p.className = "headerGlavne";
            p.innerHTML = galerija.naziv;
            div.appendChild(p);

            let btn = document.createElement("button");
            btn.innerHTML = "ViewGallery";
            btn.className = "View";
            btn.onclick = (ev)=> galerija.crtaj(host);
            div.appendChild(btn);
        });



    }
}