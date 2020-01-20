using System;
using System.Collections.Generic;
using WarframeResDemo.Data.Entities;
using WarframeResDemo.Data.Repositories;
using WarframeResDemo.Domain.Interfaces;

namespace WarframeResDemo.Domain.DefaultImplementations
{
    public class PlanetService : IPlanetService
    {
        private IPlanetRepository _planetRepository;
        private IResourceRepository _resourceRepository;
        private IMissionRepository _missionRepository;

        public PlanetService(IPlanetRepository planetRepository, IResourceRepository resourceRepository, IMissionRepository missionRepository)
        {
            _planetRepository = planetRepository;
            _resourceRepository = resourceRepository;
            _missionRepository = missionRepository;
        }

        #region IPlanetServise Members
        public void AddResource(int planetId, int resourceId)
        {
            var planet = _planetRepository.GetPlanetDetails(planetId);
            var resource = _resourceRepository.GetResourceDetails(resourceId);
            PlanetResource link = new PlanetResource();
            link.PlanetId = planetId;
            link.ResourceId = resourceId;
            link.Planet = planet;
            planet.PlanetResource.Add(link);
            resource.PlanetResource.Add(link);
            _planetRepository.UpdatePlanet(planet);
            _resourceRepository.UpdateResource(resource);
        }

        public void AddResource(int planetId, List<int> resourcesId)
        {
            var planet = _planetRepository.GetPlanetDetails(planetId);
            resourcesId.ForEach(r =>
            {
                var resource = _resourceRepository.GetResourceDetails(r);
                PlanetResource link = new PlanetResource();
                link.PlanetId = planetId;
                link.ResourceId = r;
                link.Planet = planet;
                planet.PlanetResource.Add(link);
                resource.PlanetResource.Add(link);
                _resourceRepository.UpdateResource(resource);
            });
            _planetRepository.UpdatePlanet(planet);
        }

        public void AddMission(int planetId, int missionId)
        {
            var planet = _planetRepository.GetPlanetDetails(planetId);
            bool canAdd = true;
            planet.Missions.ForEach(m =>
            {
                if (m.Id == missionId)
                {
                    canAdd = false;
                }
            });
            if (canAdd)
            {
                var mission = _missionRepository.GetMissionDetails(missionId);
                planet.Missions.Add(mission);
                mission.Planet = planet;
                _planetRepository.UpdatePlanet(planet);
                _missionRepository.UpdateMission(mission);
            }
        }

        public void AddMission(int planetId, List<int> missionsId)
        {
            var planet = _planetRepository.GetPlanetDetails(planetId);
            missionsId.ForEach(m =>
            {
                bool canAdd = true;
                planet.Missions.ForEach(p =>
                {
                    if (p.Id == m)
                    {
                        canAdd = false;
                    }
                });
                if (canAdd)
                {
                    var mission = _missionRepository.GetMissionDetails(m);
                    planet.Missions.Add(mission);
                    mission.Planet = planet;
                    _missionRepository.UpdateMission(mission);
                }
            });
            _planetRepository.UpdatePlanet(planet);
        }

        public List<Planet> GetAllPlanetsByResource(int resourceId)
        {
            List<Planet> planets = new List<Planet>();
            _planetRepository.GetAllPlanets().ForEach(p =>
            {
                p.PlanetResource.ForEach(r =>
                {
                    if (r.ResourceId == resourceId)
                    {
                        planets.Add(p);
                    }
                });
            });
            return planets;
        }
    }
    #endregion
}
