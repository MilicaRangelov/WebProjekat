import { Izlozba } from "./Izlozba.js";
import { UmetnickaDela } from "./UmetnickaDela.js";
import { Karte } from "./Karte.js";
import { Umetnici } from "./Umetnici.js";
import { Crtaj } from "./Crtaj.js";


export class Galerija{

    constructor(id,naziv){

        this.id = id;
        this.naziv=naziv;
        this.listaGalerija = [];
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
        this.kontejner.className = "Glavni2";
        host.appendChild(this.kontejner);

        
        let div1= document.createElement("div");
        div1.className = "div1";
        this.kontejner.appendChild(div1);

        let h1 = document.createElement("h1");
        h1.className = "headerGlavne";
        h1.innerHTML = this.naziv;
        div1.appendChild(h1);

        let pom = document.createElement("div");
        pom.className = "Izlozbe";
        this.kontejner.appendChild(pom);

        let p = document.createElement("p");
        p.innerHTML = "Exibitions";
        p.className = "headerGlavne";
        pom.appendChild(p);

        let btnIzlozba = document.createElement("button");
        btnIzlozba.innerHTML = "Exibitions";
        btnIzlozba.className = "View";
        pom.appendChild(btnIzlozba);

        btnIzlozba.onclick = (ev)=> this.DostupneIzlozbe(host);

        let konForma = document.createElement("div");
        konForma.className ="Karte";
        this.kontejner.appendChild(konForma);

        p = document.createElement("p");
        p.innerHTML = "Tickets";
        p.className = "headerGlavne";
        konForma.appendChild(p);

        let btnKarte = document.createElement("button");
        btnKarte.innerHTML = "Book a ticket";
        btnKarte.className = "View";
        konForma.appendChild(btnKarte);

        btnKarte.onclick = (ev)=> this.RezervisanjeKarte(host);

        let konPrikaz = document.createElement("div");
        konPrikaz.className ="Umetnici";
        this.kontejner.appendChild(konPrikaz);

        p = document.createElement("p");
        p.innerHTML = "Artists";
        p.className = "headerGlavne";
        konPrikaz.appendChild(p);

        let btnUmetnici = document.createElement("button");
        btnUmetnici.innerHTML = "ShowArtists";
        btnUmetnici.className = "View";
        konPrikaz.appendChild(btnUmetnici);
        btnUmetnici.onclick = (ev)=> this.PretraziUmetnike(host);

        this.crtajBack(this.kontejner);
        let btn = document.querySelector(".Back");
        btn.onclick= (ev) =>this.prikaziSveGalerije(host);
        btn.innerHTML = "Go Back";

    }

    prikaziSveGalerije(host){

            while(this.listaGalerija.length > 0)
                this.listaGalerija.pop();
            fetch("https://localhost:5001/Galerija/GalerijaPrikaz")
            .then(p=>{

            p.json().then(galerije =>{

            galerije.forEach(galerija => {

                var g = new Galerija(galerija.id, galerija.naziv);
                this.listaGalerija.push(g);
                
            });

            var c = new Crtaj(this.listaGalerija);
            c.crtaj2(host);
        })

    })
 }

    DostupneIzlozbe(host){

        this.obrisiDecu(host);


        this.kontejner = document.createElement("div");
        this.kontejner.className = "Glavni3";
        host.appendChild(this.kontejner);

        this.crtajBack(this.kontejner);
        let btn = document.querySelector(".Back");
        let span = document.createElement("span");
        btn.onclick= (ev) => this.crtaj(host);
        span.innerHTML = "Go Back";
        btn.appendChild(span);

        let  spoj = document.createElement("div");
        spoj.className = "spojDiv";
        this.kontejner.appendChild(spoj);

        let konForma = document.createElement("div");
        konForma.className ="KonForma";
        spoj.appendChild(konForma);

        let konPrikaz = document.createElement("div");
        konPrikaz.className ="KonPrikaz";
         spoj.appendChild(konPrikaz);

        let konPodIzlozbe = document.createElement("div");
        konPodIzlozbe.className ="PodIzlozbe";
        spoj.appendChild(konPodIzlozbe);

        this.crtajFormu(konForma);
        this.crtajPrikaz(konPrikaz);
        this.crtajPodatkeIzlozbe(konPodIzlozbe);
    
        this.crtajNext(this.kontejner);
        btn = document.querySelector(".Next");
        btn.onclick= (ev) => this.RezervisanjeKarte(host);
        btn.innerHTML = "Book a ticket";


    }

    RezervisanjeKarte(host){

        this.obrisiDecu(host);

        this.kontejner = document.createElement("div");
        this.kontejner.className = "Glavni3";
        host.appendChild(this.kontejner);

        this.crtajBack(this.kontejner);
        let btn = document.querySelector(".Back");
        btn.onclick= (ev) => this.crtaj(host);
        btn.innerHTML = "Go back";
       
        let  spoj = document.createElement("div");
        spoj.className = "spojDiv";
        this.kontejner.appendChild(spoj);

        let konDostupen = document.createElement("div");
        konDostupen.className ="KonKarte";
        spoj.appendChild(konDostupen);

        let l = document.createElement("label");
        l.innerHTML="Exibitions: ";
        l.className = "headerGlavne";
        konDostupen.appendChild(l);

        let divSe = document.createElement("div");
        divSe.className = "select";
        konDostupen.appendChild(divSe);

        let se = document.createElement("select");
        se.className = "select1";
        divSe.appendChild(se);
        this.PreuzmiIzlozbeGalerije(se);


        let konKarte = document.createElement("div");
        konKarte.className ="KonKarte";
        spoj.appendChild(konKarte);

        let konPrikazKarti = document.createElement("div");
        konPrikazKarti.className ="KonPrikazK";
        spoj.appendChild(konPrikazKarti);
        this.crtajKarte(konKarte);

        this.crtajNext(this.kontejner);
        btn = document.querySelector(".Next");
        btn.onclick= (ev) => this.PretraziUmetnike(host);
        btn.innerHTML = "ShowArtists";


    }


    PretraziUmetnike(host){

        this.obrisiDecu(host);

        this.kontejner = document.createElement("div");
        this.kontejner.className = "Glavni3";
        host.appendChild(this.kontejner );

        this.crtajBack(this.kontejner);
        let btn = document.querySelector(".Back");
        btn.onclick= (ev) => this.crtaj(host);
        btn.innerHTML = "Go Back";

        let  spoj = document.createElement("div");
        spoj.className = "spojDiv";
        this.kontejner.appendChild(spoj);

        let div = document.createElement("div");
        div.className = "FormaUmetnika";
        spoj.appendChild(div);

        let prikazD = document.createElement("div");
        prikazD.className = "PrikazUmetnika";
        spoj.appendChild(prikazD);

        let divPodaci = document.createElement("div");
        divPodaci.className = "PodaciUmetnika";
        spoj.appendChild(divPodaci);

        this.crtajFormaUmetnik();
        this.crtajPrikaz(prikazD);
        this.podaciUmetnika(divPodaci);


    }

    crtajFormaUmetnik(){

        let div = document.querySelector(".FormaUmetnika");

        let l = document.createElement("label");
        l.innerHTML="ArtWorks: ";
        l.className = "headerGlavne";
        div.appendChild(l);

        let divSe = document.createElement("div");
        divSe.className = "select";
        div.appendChild(divSe);

        let se = document.createElement("select");
        divSe.appendChild(se);

        this.umetniciGalerije(se);

        let btnPrikazi = document.createElement("button");
        btnPrikazi.onclick = (ev) => this.nadjiDelaUmetnika();
        btnPrikazi.innerHTML ="ShowArtworks";
        btnPrikazi.className = "View";
        div.appendChild(btnPrikazi);


    }


    crtajFormu(host){

        let l = document.createElement("label");
        l.innerHTML="Exibitions: ";
        l.className = "headerGlavne";
        host.appendChild(l);

        let divSe = document.createElement("div");
        divSe.className = "select";
        host.appendChild(divSe);
        let se = document.createElement("select");
        divSe.appendChild(se);

        this.PreuzmiIzlozbeGalerije(se);

        let btnPrikazi = document.createElement("button");
        btnPrikazi.onclick = (ev) => this.nadjiUmetnickaDela();
        btnPrikazi.innerHTML ="Find";
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

        var lista = ["Name", "Year", "Type of artwork"];

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
        h.innerHTML = "Book a ticket";
        h.className = "headerGlavne";
        div.appendChild(h);

        div = this.crtajDiv(host);
        let pod = document.createElement("input");
        pod.type = "text";
        pod.className = "Ime";
        pod.placeholder = "your firstname"
        div.appendChild(pod);

        div = this.crtajDiv(host);
       
        pod = document.createElement("input");
        pod.type = "text";
        pod.placeholder = "your lastname"
        pod.className = "Prezime";
        div.appendChild(pod);

        pod = document.createElement("input");
        pod.type = "number";
        pod.min = "0";
        pod.max = "20";
        pod.placeholder = "numberOfCards"
        pod.className = "NumberCards";
        div.appendChild(pod);

        div = this.crtajDiv(host);
        let div2 = this.crtajDiv(div);
        let rb1 = document.createElement("input");
        rb1.type="radio";
        rb1.value = "rez";
        rb1.name = "karta";
        div2.appendChild(rb1);

        
        let l = document.createElement("label");
        l.innerHTML = "Book a ticket";
        l.className = "headerGlavne";
        div2.appendChild(l);

        let div3 = this.crtajDiv(div);
        let rb2 = document.createElement("input");
        rb2.type="radio";
        rb2.value = "change";
        rb2.name = "karta";
        div3.appendChild(rb2);

        l = document.createElement("label");
        l.innerHTML = "Change your tickets";
        l.className = "headerGlavne";
        div3.appendChild(l);

        let divSe = document.createElement("div");
        //divSe.className = "select";
        div.appendChild(divSe);

        let se = document.createElement("select");
        se.className = "select2";
        divSe.appendChild(se);
        this.PreuzmiIzlozbeGalerije(se);

        let div5 = this.crtajDiv(div);
        let rb4 = document.createElement("input");
        rb4.type="radio";
        rb4.value = "show";
        rb4.name = "karta";
        div5.appendChild(rb4);

        l = document.createElement("label");
        l.innerHTML = "Show your tickets";
        l.className = "headerGlavne";
        div5.appendChild(l);

        let div4 = this.crtajDiv(div);
        let rb3 = document.createElement("input");
        rb3.type="radio";
        rb3.value = "del";
        rb3.name = "karta";
        div4.appendChild(rb3);

      
        l = document.createElement("label");
        l.innerHTML = "Remove ticket";
        l.className = "headerGlavne";
        div4.appendChild(l);

        div = this.crtajDiv(host)
        let btnInformacije = document.createElement("button");
        btnInformacije.innerHTML = "Informations";
        btnInformacije.className= "View";
        btnInformacije.onclick = (ev) => this.prikaziInformacijuKarte();
        div.appendChild(btnInformacije);

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
    }

    prikaziInformacijuKarte(){

        let optionEl = this.kontejner.querySelector(".select1");
        var izlozbaId = optionEl.options[optionEl.selectedIndex].value;
        let ime = document.querySelector(".Ime").value;
        let prezime = document.querySelector(".Prezime").value;
        let broj = document.querySelector(".NumberCards").value;
    
        console.log(`${ime} + ${prezime} + ${izlozbaId} + ${broj}`);
        var rb = document.querySelector( 'input[name="karta"]:checked');   
    
        console.log(rb.value);
    
        if(rb.value === "show"){
             //h2.innerHTML = "Povratne informacije vasih karata";
            this.ucitajPodatkeKarte(ime,prezime,izlozbaId);
        }
        else if(rb.value === "del"){
           // h2.innerHTML = "Vasa karta uspesno obrisina";
            this.obrisiKartu(ime,prezime,izlozbaId,broj);
        }
        else if(rb.value === "rez"){
            //h2.innerHTML = "Uspesno ste rezervisali kartu";
            if(broj < 0)
            {
                alert("Broj karata nije validan");
            }
            else{
                for(let j=0;j<broj;j++)
                    this.dodajKartu(ime,prezime,izlozbaId,broj);
            }
        }
        else if(rb.value === "change"){

            let optionEl2 = this.kontejner.querySelector(".select2");
            var izlozbaId2 = optionEl2.options[optionEl2.selectedIndex].value;
             this.izmeniKartu(ime,prezime,izlozbaId,izlozbaId2,broj);
        }
    
    
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

    PreuzmiIzlozbeGalerije(host){

        fetch("https://localhost:5001/Izlozba/PrikaziIzlozbeGalerije/" + this.id,{
            method:"GET"
        })
        .then(p=>{

            p.json().then( izlozbe =>{

                izlozbe.forEach( izlozba => {

                    let op = document.createElement("option");
                    let izl = new Izlozba(izlozba.id, izlozba.naziv, izlozba.datumPoc, izlozba.datKraja, izlozba.brojKarata);
                    op.innerHTML = izl.naziv;
                    op.value = izl.id;
                    host.appendChild(op);

                })
            })


        })

    }

    nadjiUmetnickaDela(){

        let optionEl = this.kontejner.querySelector("select");
        var izlozbaId = optionEl.options[optionEl.selectedIndex].value;

        console.log(izlozbaId);

        this.ucitajPodatkeIzlozbe(izlozbaId);

        this.ucitajUmetnickaDela(izlozbaId);
    }

    ucitajPodatkeIzlozbe(izlozbaId){

        fetch("https://localhost:5001/Izlozba/PodaciIzlozbe/"+ izlozbaId,{
            method: "GET"
        })
        .then(p=>{
            if(p.ok){
                p.json().then(izlozbe =>{

                    izlozbe.forEach(el => {

                        var izl = new Izlozba(el.id, el.naslov, el.datumPocetka, el.datumKraja, el.brojKarata);
                        izl.crtaj(); 

                    })
                
                })
        }
        else{

            alert("Uneti podaci nisu validni");
        }
        })

    }

    ucitajUmetnickaDela(izlozbaId){
        fetch("https://localhost:5001/Izlozba/IzlozenaDela/" + izlozbaId, {
            method: "GET"
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
            else{

                alert("Uneti podaci nisu validni");
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
            else{

                alert("Uneti podaci nisu validni");
            }


        })
    }


    obrisiKartu(ime,prezime,idIzlozbe,broj ){

        fetch("https://localhost:5001/Karte/ObrisiKartuPosetioca/" + ime + "/" + prezime + "/" + idIzlozbe + "/" + broj, {
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
            else{

                alert("Uneti podaci nisu validni");
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
            else{

                alert("Uneti podaci nisu validni");
            }


        })
    }

    izmeniKartu(ime,prezime,izlozbaId,izlozbaId2,broj){

        fetch("https://localhost:5001/Karte/IzmeniKartu/" + ime + "/"+ prezime + "/"+izlozbaId + "/" + izlozbaId2 + "/" + broj, {
            method: "PUT"
        })
        .then(p => {

            if(p.ok){

                p.json().then( karte => {


                     var k = new Karte(karte[0].ime, karte[0].prezime, karte[0].izlozba,karte[0].broj);
                     k.crtaj();

                })

            }
            else{

                alert("Uneti podaci nisu validni");
            }


        })

    }


    umetniciGalerije(host){

        fetch("https://localhost:5001/Galerija/PrikaziUmetnike/"+ this.id, {
            method: "GET"
        })
        .then(p=>{

            if(p.ok){

                p.json().then(umetnici => {

                    umetnici.forEach(umetnik => {

                        var u = new Umetnici(umetnik.id,umetnik.ime, umetnik.umetnickoIme, umetnik.prezime, umetnik.drzavaRodjenja);
                        let op = document.createElement("option");
                        op.innerHTML = u.ime;
                        op.value = u.id;
                        host.appendChild(op);
                    })
                })
            }
            else{

                alert("Uneti podaci nisu validni");
            }
        })
    }

    nadjiDelaUmetnika(){

        let optionEl = this.kontejner.querySelector("select");
        var umetnikId = optionEl.options[optionEl.selectedIndex].value;


        this.ucitajDelaUmetnika(umetnikId);
        this.prikaziPodatkeUmetnika(umetnikId);

    }

    ucitajDelaUmetnika(umetnikId){

        fetch("https://localhost:5001/UmetnickoDelo/DelaUmetnika/"+ umetnikId + "/"+this.id, {
            method : "GET"
        })
        .then(p=>{

            if(p.ok){

                var tabela = document.querySelector(".Tabela");
                    
                    if(i = tabela.rows.length != 0){
                        for(var i = tabela.rows.length-1; i >= 0; i--)
                        {
                            tabela.deleteRow(i);
                        }
                    }
                
                p.json().then(dela => {

                    dela.forEach(delo => {

                        var d = new UmetnickaDela(delo.naslov, delo.godina, delo.tipDela);
                        d.crtaj(tabela);
                    })
                })    


            }
            else{

                alert("Uneti podaci nisu validni");
            }
        })

    }

    prikaziPodatkeUmetnika(umetnikId){

        fetch("https://localhost:5001/Umetnik/Umetnik/" + umetnikId , {
            method: "GET"
        })
        .then(p=> {

            if(p.ok){

                p.json().then(umetnici => {
                    
                    umetnici.forEach(umetnik => {
                        
                        var um = new Umetnici(umetnik.id, umetnik.ime, umetnik.umetnickoIme, umetnik.prezime, umetnik.drzavaRodj);
                        um.crtaj();
                    })
                })
            }
            else{

                alert("Uneti podaci nisu validni");
            }
        })

    }


}