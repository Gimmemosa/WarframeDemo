using WarframeResDemo.Data.Entities;
using System.Collections.Generic;

namespace WarframeResDemo.Data.Entities
{
    public class Resource
    {
        public int Id { get; set; }
        public string ResourceName { get; set; }
        public float DropChance { get; set; }
        public List<PlanetResource> PlanetResource { get; set; }
        public Resource()
        {
            PlanetResource = new List<PlanetResource>();
        }
    }
}
