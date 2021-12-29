using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Models
{
    public class Umetnik
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(20)]
        public string Ime { get; set; }

        [Required]
        [MaxLength(20)]
        public string UmetnickoIme { get; set; }

        [Required]
        [MaxLength(20)]
        public string Prezime { get; set; }

        [Required]
        [MaxLength(30)]
        public string DrzavaRodjenja { get; set; }

        public virtual List<UmetnickoDelo> UmetnickaDela { get; set; }
        

    }
}