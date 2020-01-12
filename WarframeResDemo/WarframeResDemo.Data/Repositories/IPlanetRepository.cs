using System;
using System.Collections.Generic;
using System.Text;
using WarframeResDemo.Data.Entities;

namespace WarframeResDemo.Data.Repositories
{
    public interface IPlanetRepository
    {
        List<Planet> GetAllPlanets();
        Planet GetPlanetDetails(int planetId);
        void CreatePlanet(Planet planet);
        void UpdatePlanet(Planet planet);
        void DeletePlanet(int planetId);
    }
}
