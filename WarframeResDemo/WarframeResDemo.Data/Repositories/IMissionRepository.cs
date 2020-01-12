using System;
using System.Collections.Generic;
using System.Text;
using WarframeResDemo.Data.Entities;

namespace WarframeResDemo.Data.Repositories
{
    public interface IMissionRepository
    {
        List<Mission> GetAllMissions();
        Mission GetMissionDetails(int missionId);
        void CreateMission(Mission mission);
        void DeleteMission(int missionId);
        void UpdateMission(Mission mission);
    }
}
