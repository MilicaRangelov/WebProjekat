using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace Models
{
    public class Izlozeno
    {
        [Key]
        public int ID { get; set; }

        public virtual Izlozba Izlozba { get; set; }
        
        public virtual UmetnickoDelo UmetnickoDelo { get; set; }

    }
}