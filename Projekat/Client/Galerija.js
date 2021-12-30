import { Karte } from "./Karte.js";
import { Izlozbe } from "./Izlozbe.js";
import { UmetnickaDela } from "./UmetnickaDela.js";
import { Umetnici } from "./Umetnici.js";

export class Galerija{

    constructor(listaIzlozba, listaUmetnika){
        this.listaIzlozba = listaIzlozba;
        console.log(this.listaIzlozba);
        this.listaUmetnika = listaUmetnika;
        console.log(this.listaUmetnika);
        this.kontejner = null;
    }
//#region Crtanje 

    obrisiDecu(){
        if(this.kontejner != null){
            while (this.kontejner.lastChild) {
                this.kontejner.removeChild(this.kontejner.lastChild);
            }
        }

    }


    crtaj(host){

        this.obrisiDecu();

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

    crtaj2(host){

        this.obrisiDecu();

        this.kontejner = document.createElement("div");
        this.kontejner.className = "Glavni2";
        host.appendChild(this.kontejner);

        let pom = document.createElement("div");
        pom.className = "Izlozbe";
        this.kontejner.appendChild(pom);

        let p = document.createElement("p");
        p.innerHTML = "Izlozbe";
        p.className = "headerGlavne";
        pom.appendChild(p);

        let btnIzlozba = document.createElement("button");
        btnIzlozba.innerHTML = "PrikaziIzlozbe";
        btnIzlozba.className = "View";
        pom.appendChild(btnIzlozba);

        btnIzlozba.onclick = (ev)=> this.DostupneIzlozbe(host);

        let konForma = document.createElement("div");
        konForma.className ="Karte";
        this.kontejner.appendChild(konForma);

        p = document.createElement("p");
        p.innerHTML = "Karte";
        p.className = "headerGlavne";
        konForma.appendChild(p);

        let btnKarte = document.createElement("button");
        btnKarte.innerHTML = "RezervisiKartu";
        btnKarte.className = "View";
        konForma.appendChild(btnKarte);

        btnKarte.onclick = (ev)=> this.RezervisanjeKarte(host);

        let konPrikaz = document.createElement("div");
        konPrikaz.className ="Umetnici";
        this.kontejner.appendChild(konPrikaz);

        p = document.createElement("p");
        p.innerHTML = "Umetnici";
        p.className = "headerGlavne";
        konPrikaz.appendChild(p);

        let btnUmetnici = document.createElement("button");
        btnUmetnici.innerHTML = "VidiUmetnike";
        btnUmetnici.className = "View";
        konPrikaz.appendChild(btnUmetnici);
        btnUmetnici.onclick = (ev)=> this.PretraziUmetnike(host);


    }

   PretraziUmetnike(host){

        this.obrisiDecu();

        this.kontejner = document.createElement("div");
        this.kontejner.className = "Glavni3";
        host.appendChild(this.kontejner );

        this.crtajBack(this.kontejner);
        let btn = document.querySelector(".Back");
        btn.onclick= (ev) => this.crtaj2(host);
        btn.innerHTML = "Vrati se na pregled";

        let div = document.createElement("div");
        div.className = "FormaUmetnika";
        this.kontejner.appendChild(div);

        let prikazD = document.createElement("div");
        prikazD.className = "PrikazUmetnika";
        this.kontejner.appendChild(prikazD);

        let divPodaci = document.createElement("div");
        divPodaci.className = "PodaciUmetnika";
        this.kontejner.appendChild(divPodaci);

        this.crtajFormaUmetnik();
        this.crtajPrikaz(prikazD);
        this.podaciUmetnika(divPodaci);

        
    }

    crtajFormaUmetnik(){

        let div = document.querySelector(".FormaUmetnika");

        let l = document.createElement("label");
        l.innerHTML="Dostupna dela umetnika: ";
        l.className = "headerGlavne";
        div.appendChild(l);

        let divSe = document.createElement("div");
        divSe.className = "select";
        div.appendChild(divSe);

        let se = document.createElement("select");
        divSe.appendChild(se);

        let op;
        this.listaUmetnika.forEach(p => {
            
            op = document.createElement("option");
            op.innerHTML = p.ime + " " +p.prezime;
            op.value = p.id;

            se.appendChild(op);

        });

        let btnPrikazi = document.createElement("button");
        btnPrikazi.onclick = (ev) => this.nadjiDelaUmetnika();
        btnPrikazi.innerHTML ="Dostupna dela";
        btnPrikazi.className = "View";
        div.appendChild(btnPrikazi);


    }

    RezervisanjeKarte(host){

        this.obrisiDecu();

        this.kontejner = document.createElement("div");
        this.kontejner.className = "Glavni3";
        host.appendChild(this.kontejner);

        this.crtajBack(this.kontejner);
        let btn = document.querySelector(".Back");
        btn.onclick= (ev) => this.crtaj2(host);
        btn.innerHTML = "Vrati se na pregled";
       
        let konDostupen = document.createElement("div");
        konDostupen.className ="KonKarte";
        this.kontejner.appendChild(konDostupen);

        let l = document.createElement("label");
        l.innerHTML="Dostupne izlozbe: ";
        l.className = "headerGlavne";
        konDostupen.appendChild(l);

        let divSe = document.createElement("div");
        divSe.className = "select";
        konDostupen.appendChild(divSe);

        let se = document.createElement("select");
        divSe.appendChild(se);
        let op;

        this.listaIzlozba.forEach(p => {
            
            op = document.createElement("option");
            op.innerHTML = p.nazivIzlozbe;
            op.value = p.id;

            se.appendChild(op);

        });


        let konKarte = document.createElement("div");
        konKarte.className ="KonKarte";
        this.kontejner.appendChild(konKarte);

        let konPrikazKarti = document.createElement("div");
        konPrikazKarti.className ="KonPrikazK";
        this.kontejner.appendChild(konPrikazKarti);
        this.crtajKarte(konKarte);

        this.crtajNext(this.kontejner);
        btn = document.querySelector(".Next");
        btn.onclick= (ev) => this.PretraziUmetnike(host);
        btn.innerHTML = "PregledajUmetnike";

    }

    DostupneIzlozbe(host){

    
        this.obrisiDecu();


        this.kontejner = document.createElement("div");
        this.kontejner.className = "Glavni3";
        host.appendChild(this.kontejner);

        this.crtajBack(this.kontejner);
        let btn = document.querySelector(".Back");
        btn.onclick= (ev) => this.crtaj2(host);
        btn.innerHTML = "Vrati se na pregled";


        let konForma = document.createElement("div");
        konForma.className ="KonForma";
        this.kontejner.appendChild(konForma);

        let konPrikaz = document.createElement("div");
        konPrikaz.className ="KonPrikaz";
        this.kontejner.appendChild(konPrikaz);

        let konPodIzlozbe = document.createElement("div");
        konPodIzlozbe.className ="PodIzlozbe";
        this.kontejner.appendChild(konPodIzlozbe);

        this.crtajFormu(konForma);
        this.crtajPrikaz(konPrikaz);
        this.crtajPodatkeIzlozbe(konPodIzlozbe);
    
        this.crtajNext(this.kontejner);
        btn = document.querySelector(".Next");
        btn.onclick= (ev) => this.RezervisanjeKarte(host);
        btn.innerHTML = "Rezervisi svoju kartu";


    }



    crtajFormu(host){

        let l = document.createElement("label");
        l.innerHTML="Dostupne izlozbe: ";
        l.className = "headerGlavne";
        host.appendChild(l);

        let divSe = document.createElement("div");
        divSe.className = "select";
        host.appendChild(divSe);
        let se = document.createElement("select");
        divSe.appendChild(se);
        let op;
        this.listaIzlozba.forEach(p => {
            
            op = document.createElement("option");
            op.innerHTML = p.nazivIzlozbe;
            op.value = p.id;

            se.appendChild(op);

        });

        let btnPrikazi = document.createElement("button");
        btnPrikazi.onclick = (ev) => this.nadjiUmetnickaDela();
        btnPrikazi.innerHTML ="Nadji";
        btnPrikazi.className = "View";
        host.appendChild(btnPrikazi);
    }

    crtajPrikaz(host){


        var tabela = document.createElement("table");
        tabela.className="Tabela";
        host.appendChild(tabela);

        var tHead = document.createElement("thead");
        tHead.className = "TableHead";
        tabela.appendChild(tHead);

        var tBody = document.createElement("tBody");
        tBody.className = "TableBody";
        tabela.appendChild(tBody);

        var lista = ["Naslov dela", "Kreirano", "Tip dela"];

        let th;

        lista.forEach( el=>{

            th = document.createElement("th");
            th.innerHTML = el;
            tHead.appendChild(th);
        })


    }

    podaciUmetnika(host){

        let p = document.createElement("p");
        p.className = "pIme";
        host.appendChild(p);

        p = document.createElement("p");
        p.className = "pUmetnicko";
        host.appendChild(p);

        p = document.createElement("p");
        p.className = "pPrezime";
        host.appendChild(p);

        
        p = document.createElement("p");
        p.className = "pDrzava";
        host.appendChild(p);


    }

    crtajPodatkeIzlozbe(host){

        let  p = document.createElement("h2");
        p.className = "hNaslov";
        host.appendChild(p);

        p = document.createElement("p");
        p.className = "pPocetak";
        host.appendChild(p);

        p = document.createElement("p");
        p.className = "pKraj";
        host.appendChild(p);

        p = document.createElement("p");
        p.className = "pK";
        host.appendChild(p);
    }

    crtajKarte(host){

        let div = this.crtajDiv(host);
        let h = document.createElement("h2");
        h.innerHTML = "Rezervisite kartu";
        h.className = "headerGlavne";
        div.appendChild(h);

        div = this.crtajDiv(host);
        let pod = document.createElement("input");
        pod.type = "text";
        pod.className = "Ime";
        pod.placeholder = "Vase ime"
        div.appendChild(pod);

        div = this.crtajDiv(host);
       
        pod = document.createElement("input");
        pod.type = "text";
        pod.placeholder = "Vase prezime"
        pod.className = "Prezime";
        div.appendChild(pod);

        div = this.crtajDiv(host);
        let div2 = this.crtajDiv(div);
        let rb1 = document.createElement("input");
        rb1.type="radio";
        rb1.value = "rez";
        rb1.name = "karta";
        div2.appendChild(rb1);

        
        let l = document.createElement("label");
        l.innerHTML = "Rezervisi kartu";
        l.className = "headerGlavne";
        div2.appendChild(l);

        let div3 = this.crtajDiv(div);
        let rb2 = document.createElement("input");
        rb2.type="radio";
        rb2.value = "show";
        rb2.name = "karta";
        div3.appendChild(rb2);

        l = document.createElement("label");
        l.innerHTML = "Prikazi kartu";
        l.className = "headerGlavne";
        div3.appendChild(l);

        let div4 = this.crtajDiv(div);
        let rb3 = document.createElement("input");
        rb3.type="radio";
        rb3.value = "del";
        rb3.name = "karta";
        div4.appendChild(rb3);

      
        l = document.createElement("label");
        l.innerHTML = "Obrisi kartu";
        l.className = "headerGlavne";
        div4.appendChild(l);

        div = this.crtajDiv(host)
        let btnInformacije = document.createElement("button");
        btnInformacije.innerHTML = "Informacije";
        btnInformacije.className= "View";
        btnInformacije.onclick = (ev) => this.prikaziInformacijuKarte();
        div.appendChild(btnInformacije);
    }


    crtajDiv(host){

        let div = document.createElement("div");
        host.appendChild(div);
        return div;

    }

    crtajNext(host){

        let div = document.createElement("div");
        host.appendChild(div);

        let button = document.createElement("button");
        button.className="Next";
        div.appendChild(button);

    }

    crtajBack(host){
        let div = document.createElement("div");
        host.appendChild(div);

        let button = document.createElement("button");
        button.className="Back";
        div.appendChild(button);
    }
//#endregion
    
//#region UcitajPodatke 

prikaziInformacijuKarte(){

    let optionEl = this.kontejner.querySelector("select");
    var izlozbaId = optionEl.options[optionEl.selectedIndex].value;
    let ime = document.querySelector(".Ime").value;
    let prezime = document.querySelector(".Prezime").value;

    console.log(`${ime} + ${prezime} + ${izlozbaId}`);
    var rb = document.querySelector( 'input[name="karta"]:checked');   
    let prikaz = document.querySelector(".KonPrikazK");

    let h2 = document.createElement("h2");
    h2.className = "headerGlavne";
    prikaz.appendChild(h2);

    let p = document.createElement("p");
    p.className = "pIme";
    prikaz.appendChild(p);

    p = document.createElement("p");
    p.className = "pPrezime";
    prikaz.appendChild(p);

    p = document.createElement("p");
    p.className = "pIzlozbaNaziv";
    prikaz.appendChild(p);

    p = document.createElement("p");
    p.className = "pBroj";
    prikaz.appendChild(p);

    console.log(rb.value);

    if(rb.value === "show"){
         //h2.innerHTML = "Povratne informacije vasih karata";
        this.ucitajPodatkeKarte(ime,prezime,izlozbaId);
    }
    else if(rb.value === "del"){
       // h2.innerHTML = "Vasa karta uspesno obrisina";
        this.obrisiKartu(ime,prezime,izlozbaId);
    }
    else if(rb.value === "rez"){
        //h2.innerHTML = "Uspesno ste rezervisali kartu";
        this.dodajKartu(ime,prezime,izlozbaId);
    }


}

    nadjiDelaUmetnika(){

        let optionEl = this.kontejner.querySelector("select");
        var umetnikId = optionEl.options[optionEl.selectedIndex].value;

        console.log(umetnikId);

        this.ucitajPodatkeUmetnika(umetnikId);

        this.ucitajDelaUmetnika(umetnikId);

    }

    nadjiUmetnickaDela(){

            let optionEl = this.kontejner.querySelector("select");
            var izlozbaId = optionEl.options[optionEl.selectedIndex].value;

            console.log(izlozbaId);

            this.ucitajPodatkeIzlozbe(izlozbaId);

            this.ucitajUmetnickaDela(izlozbaId);

    }
    
    ucitajUmetnickaDela(izlozbaId){

        fetch("https://localhost:5001/Izlozba/IzlozenaDela/" + izlozbaId,{
            method:"GET"
        })
        .then(s=>{

            if(s.ok){

                s.json().then(data =>{

                    var tabela = document.querySelector(".Tabela");
                    
                    if(i = tabela.rows.length != 0){
                        for(var i = tabela.rows.length-1; i >= 0; i--)
                        {
                            tabela.deleteRow(i);
                        }
                    }

                    var teloTab = document.querySelector(".TableBody");
                    data.forEach(dl =>{

                        let d = new UmetnickaDela(dl.naslov,dl.kreirano, dl.tip);
                        d.crtaj(teloTab);
                        console.log(d);
                    })

                    
                })
            }
        })


    }


    ucitajDelaUmetnika(umetnikId){

        fetch("https://localhost:5001/UmetnickoDelo/DelaUmetnika/" + umetnikId, {

            method: "GET"
        })
        .then(p=>{

            if(p.ok){

                p.json().then(data =>{
                    console.log(data);
                    var tabela = document.querySelector(".Tabela");
                    
                    if(i = tabela.rows.length != 0){
                        for(var i = tabela.rows.length-1; i >= 0; i--)
                        {
                            tabela.deleteRow(i);
                        }
                    }

                    var teloTab = document.querySelector(".TableBody");

                    data.forEach(el =>{

                        var u = new UmetnickaDela(el.naslov, el.godina,el.tipDela);
                        u.crtaj(teloTab);
                    })
                })
            }

        })

        
    }


    ucitajPodatkeUmetnika(umetnikId){
        fetch("https://localhost:5001/Umetnik/Umetnik/" + umetnikId, {

            method :"GET"
        })
        .then(p=>{
            
            if(p.ok){

                p.json().then( data =>{

                    data.forEach(el => {
                        var um = new Umetnici(el.id, el.ime, el.umetnickoIme, el.prezime, el.drzavaRodj);
                        console.log(data);
                    
                        um.crtaj(); 

                    })
                   
    
                })

            }


        })

    }

    ucitajPodatkeIzlozbe(izlozbaId){

        fetch("https://localhost:5001/Izlozba/Izlozba/" + izlozbaId, {

            method :"GET"
        })
        .then(p=>{
            
            if(p.ok){

                p.json().then( data =>{

                    data.forEach(el => {
                        var izl = new Izlozbe(el.id, el.naslov, el.datumPocetka, el.datumKraja, el.brojKarata);
                        console.log(data);
                    
                        izl.crtaj(); 

                    })
                   
    
                })

            }


        })

    }





    ucitajPodatkeKarte( ime, prezime, idIzlozbe){

        fetch("https://localhost:5001/Karte/KartaPosetioca/" + ime + "/" + prezime + "/" + idIzlozbe, {
            method:"GET"
        })
        .then(s=>{
            if(s.ok){
                s.json().then(
                
                    data => {
                      
                        console.log(data);
                        if(data.length > 0){
                            var k = new Karte(data[0].ime, data[0].prezime, data[0].izlozba,data[0].broj);
                            k.crtaj();
                        }
                        else
                        {
                            let p = document.querySelector(".pIme");
                            p.innerHTML = "Ime posetioca: " + ime;

                            p = document.querySelector(".pPrezime");
                            p.innerHTML = "Prezime posetioca: " + prezime;

                            p = document.querySelector(".pBroj");
                            p.innerHTML = "Broj rezervisanih karata: 0";
                        }
                        
                       

                })
            }


        })
    }

    obrisiKartu(ime,prezime,idIzlozbe){

        fetch("https://localhost:5001/Karte/ObrisiKartuPosetioca/" + ime + "/" + prezime + "/" + idIzlozbe, {
            method:"DELETE"
        })
        .then(s=>{
            if(s.ok){
                s.json().then( data =>{

                    console.log(data);
                    if(data.length > 0){
                        var k = new Karte(data[0].ime, data[0].prezime, data[0].izlozba,data[0].broj);
                        k.crtaj();
                    }
                    else
                    {
                        let p = document.querySelector(".pIme");
                        p.innerHTML = "Ime posetioca: " + ime;

                        p = document.querySelector(".pPrezime");
                        p.innerHTML = "Prezime posetioca: " + prezime;

                        p = document.querySelector(".pBroj");
                        p.innerHTML = "Broj rezervisanih karata: 0";
                    }
                })
            }


        })


    }

    dodajKartu(ime,prezime,idIzlozbe){

        fetch("https://localhost:5001/Karte/DodajKartu/" + ime + "/" + prezime + "/" + idIzlozbe, {
            method:"POST"
        })
        .then(s=>{
            if(s.ok){
                s.json().then( data =>{

                    var k = new Karte(data[0].ime, data[0].prezime, data[0].izlozba,data[0].broj);
                    k.crtaj();

                })
            }


        })
    }

    
    //#endregion

}