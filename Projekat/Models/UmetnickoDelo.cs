using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Models
{
    public class UmetnickoDelo
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Naslov { get; set; }

        [Required]
        [MaxLength(4)]
        public int Godina { get; set; }

        [Required]
        [MaxLength(15)]
        public string TipDela { get; set; }

        public virtual Umetnik Umetnik { get; set; }
        public virtual List<Izlozeno> DeloIzlozba { get; set; }
        

    }
}