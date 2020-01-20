using System.Collections.Generic;
using WarframeResDemo.Data.Entities;

namespace WarframeResDemo.Domain.Interfaces
{
    public interface IMissionService
    {
        void ChangeFraction(int missionId, int fractionId);
        void ChangeType(int missionId, int missionTypeId);
        void ChangeLevel(int missionId, int level);
        void StartMission(int missionId);
        void PauseMission(int missionId, int progress);
        int IsPaused(int missionId);
        void EndMission(int missionId);
        List<Mission> GetAllMissionsByPlanet(int planetId);
    }
}
