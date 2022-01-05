using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;

namespace Models
{
    public class Karta
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(20)]
        public string ImePosetioca { get; set; }

        [Required]
        [MaxLength(20)]
        public string PrezimePosetioca { get; set; }
        
        [JsonIgnore] 

        public virtual Izlozba Izlozba { get; set; }
    }
}