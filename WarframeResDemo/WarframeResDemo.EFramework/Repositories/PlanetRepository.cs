using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarframeResDemo.Data.Entities;
using WarframeResDemo.Data.Repositories;

namespace WarframeResDemo.EFr.Repositories
{
    public class PlanetRepository : IPlanetRepository

    {
        public void CreatePlanet(Data.Entities.Planet planet)
        {
            using (var ctx = new WarframeResDemoContext())
            {
                ctx.Planets.Add(planet);
                ctx.SaveChanges();
            }
        }

        public void DeletePlanet(int planetId)
        {
            using (var ctx = new WarframeResDemoContext())
            {
                var planet = ctx.Planets.Find(planetId);
                ctx.Planets.Remove(planet);
                ctx.SaveChanges();
            }
        }

        public List<Planet> GetAllPlanets()
        {
            using (var ctx = new WarframeResDemoContext())
            {
                return ctx.Planets.Include(p=>p.PlanetResource).Include(p=>p.Missions).ToList();
            }
        }

        public Planet GetPlanetDetails(int planetId)
        {
            using (var ctx = new WarframeResDemoContext())
            {
                return ctx.Planets.Include(p => p.PlanetResource).Include(p => p.Missions).First(x => x.Id == planetId);
            }
        }

        public void UpdatePlanet(Planet planet)
        {
            using (var ctx = new WarframeResDemoContext())
            {
                ctx.Planets.Attach(planet);
                ctx.Entry(planet).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }
    }
}
