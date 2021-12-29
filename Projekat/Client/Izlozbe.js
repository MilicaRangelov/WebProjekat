export class Izlozbe{

    constructor(id,nazivIzlozbe, datumPocetka,datumKraja,brojKarata){
        this.id = id;
        this.nazivIzlozbe = nazivIzlozbe;
        this.datumPocetka =datumPocetka;
        this.datumKraja = datumKraja;
        this.brojKarata = brojKarata;
    }

    crtaj(){

        let  p = document.querySelector(".hNaslov");
        p.innerHTML = this.nazivIzlozbe;

        p = document.querySelector(".pPocetak");
        p.innerHTML = "Datum pocetka: " + this.datumPocetka;


        p = document.querySelector(".pKraj");
        p.innerHTML = "Datum zavrsavanja: " + this.datumKraja;
        

        p = document.querySelector(".pK");
        p.innerHTML = "Broj rezervisanih karata: " + this.brojKarata;
    }
}