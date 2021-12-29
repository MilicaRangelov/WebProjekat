export class Karte{

    constructor(ime, prezime,nazivIzlozbe,brojKarata){
        this.ime= ime;
        this.prezime = prezime;
        this.nazivIzlozbe = nazivIzlozbe,
        this.brojKarata = brojKarata
        
    }

    crtaj(){

        let p = document.querySelector(".pIme");
        p.innerHTML = "Ime posetioca: " + this.ime;

        p = document.querySelector(".pPrezime");
        p.innerHTML = "Prezime posetioca: " + this.prezime;

        p = document.querySelector(".pIzlozbaNaziv");
        p.innerHTML = "Naziv izlozbe: " + this.nazivIzlozbe;


        p = document.querySelector(".pBroj");
        p.innerHTML = "Broj rezervisanih karata: " + this.brojKarata;
    }
}