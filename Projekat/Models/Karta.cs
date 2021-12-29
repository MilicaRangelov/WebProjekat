using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

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

        public virtual Izlozba Izlozba { get; set; }

        

    }
}