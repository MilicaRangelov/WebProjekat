
export class Umetnici{

    constructor(id,ime,umetnickoIme,prezime, drzavaRodjenja){

        this.id = id;
        this.ime = ime;
        this.prezime = prezime;
        this.umetnickoIme = umetnickoIme;
        this.drzavaRodjenja = drzavaRodjenja;

    }

    crtaj(){

        let  p = document.querySelector(".pIme");
        p.innerHTML = this.ime;

        p = document.querySelector(".pUmetnicko");
        p.innerHTML = this.umetnickoIme;


        p = document.querySelector(".pPrezime");
        p.innerHTML = this.prezime;
        

        p = document.querySelector(".pDrzava");
        p.innerHTML = this.drzavaRodjenja;
    }

}