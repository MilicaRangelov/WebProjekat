using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace Models
{
    public class Izlozba
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string NazivIzlozbe { get; set; }

       [Required]
       public DateTime DatumPocetka { get; set; }

       [Required]
       public DateTime DatumKraja { get; set; }
        
       public virtual List<Izlozeno> IzlozbaDelo { get; set; } 

       public virtual List<Karta> Karte { get; set; }

    }
}