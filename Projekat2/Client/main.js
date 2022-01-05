import { Galerija } from "./Galerija.js";

let listaIzlozbi = [];
let listaUmetnika = [];
let idGalerije;
let nazivG;

var galerija = new Galerija(idGalerije,nazivG,listaIzlozbi,listaUmetnika);
galerija.crtaj(document.body);