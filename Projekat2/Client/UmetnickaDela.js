export class UmetnickaDela{

    constructor(naslov,godina,tipDela,){
        this.naslov = naslov;
        this.godina = godina;
        this.tipDela = tipDela;
    }

    crtaj(host){

        var tr = document.createElement("tr");
        host.appendChild(tr);

        var td = document.createElement("td");
        td.innerHTML = this.naslov;
        tr.appendChild(td);

        td = document.createElement("td");
        td.innerHTML = this.godina;
        tr.appendChild(td);

        td = document.createElement("td");
        td.innerHTML = this.tipDela;
        tr.appendChild(td);
    }
}