

export class Galerija{

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
        h1.innerHTML = "This is your gallery";
        div1.appendChild(h1);

        let btnView = document.createElement("button");
        btnView.className = "View";
        btnView.innerHTML = "ViewMore";
        this.kontejner.appendChild(btnView);

        btnView.onclick = (ev) => this.crtaj2(host);
        
    }
}