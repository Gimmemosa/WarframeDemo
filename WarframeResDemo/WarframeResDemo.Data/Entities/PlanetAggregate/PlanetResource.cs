using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WarframeResDemo.Data.Entities
{
    public class PlanetResource
    {
        [Key]
        public int PlanetId { get; set; }
        [ForeignKey("PlanetId")]
        public Planet Planet { get; set; }
        [Key]
        public int ResourceId { get; set; }
        //[ForeignKey("ResourceId")]
        //public Resource Resource { get; set; }
    }
}
