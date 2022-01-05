using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;

namespace Models
{
    public class Dostupno
    {
        [Key]
        public int ID { get; set; }

          [JsonIgnore]  

        public virtual Umetnik Umetnik { get; set; }  
        public virtual Galerija Galerija { get; set; }

        

    }
}