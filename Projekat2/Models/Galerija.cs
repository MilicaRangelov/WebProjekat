using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;

namespace Models{


    public class Galerija{

        [Key]
        public int ID { get; set; }

        [Required]
        public string Naziv { get; set; }

        [JsonIgnore] 

        public virtual List<UmetnickoDelo> UmetnickaDela { get; set; }

        public virtual List<Izlozba> Izlozbe { get; set; }

        public virtual List<Dostupno> GakerijaUmetnici { get; set; }
    
    }


}