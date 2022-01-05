using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;

namespace Models
{
    public class Izlozeno
    {
        [Key]
        public int ID { get; set; }

        [JsonIgnore] 

        public virtual Izlozba Izlozba { get; set; }
        public virtual UmetnickoDelo UmetnickoDelo { get; set; }

    }
}