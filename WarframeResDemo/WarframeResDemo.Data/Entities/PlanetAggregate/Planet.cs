using System.Collections.Generic;
using WarframeResDemo.Data.Entities;

namespace WarframeResDemo.Data.Entities
{
    public class Planet
    {
        public int Id { get; set; }
        public string PlanetName { get; set; }
        public List<Mission> Missions { get; set; }
        public List<PlanetResource> PlanetResource { get; set; }
        public Planet()
        {
            Missions = new List<Mission>();
            PlanetResource = new List<PlanetResource>();
        }
    }
}
