
export class Umetnici{

    constructor(id, ime, umetnickoIme, prezime, drzavaRodj){

        this.id = id;
        this.ime = ime;
        this.umetnickoIme = umetnickoIme;
        this.prezime = prezime;
        this.drzavaRodj = drzavaRodj;
    }

    crtaj(){

        let p =document.querySelector(".pIme");
        p.innerHTML = "Ime: " + this.ime;

        p = document.querySelector(".pUmetnicko");
        p.innerHTML = "Umetnicko Ime: " + this.umetnickoIme;

        p = document.querySelector(".pPrezime");
        p.innerHTML = "Prezime: " + this.prezime;

        p=document.querySelector(".pDrzava");
        p.innerHTML = "Drzava rodjenja: " + this.drzavaRodj;
    }

}