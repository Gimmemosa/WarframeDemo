using System;
using System.Collections.Generic;
using System.Text;
using WarframeResDemo.Data.Entities;

namespace WarframeResDemo.Domain.Interfaces
{
    public interface IPlanetService
    {
        void AddResource(int planetId, int resourceId);
        void AddResource(int planetId, List<int> resourcesId);
        void AddMission(int planetId, int missionId);
        void AddMission(int planetId, List<int> missionsId);
        List<Planet> GetAllPlanetsByResource(int resourceId);


    }
}
