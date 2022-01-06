
export class Izlozba{

    constructor(id, naziv, datPoc,datKraj, brKarata){

        this.id = id;
        this.naziv =naziv;
        this.datPoc = datPoc;
        this.datKraj = datKraj;
        this.brKarata = brKarata;
    }

    crtaj(){

        let  p = document.querySelector(".hNaslov");
        p.innerHTML = this.naziv;

        p = document.querySelector(".pPocetak");
        p.innerHTML = "Datum pocetka: " + this.datPoc;


        p = document.querySelector(".pKraj");
        p.innerHTML = "Datum zavrsavanja: " + this.datKraj;
        

        p = document.querySelector(".pK");
        p.innerHTML = "Broj rezervisanih karata: " + this.brKarata;
    }
}