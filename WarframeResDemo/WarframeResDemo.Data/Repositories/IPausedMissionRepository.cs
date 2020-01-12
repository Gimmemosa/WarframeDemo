using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarframeResDemo.Data.Entities;

namespace WarframeResDemo.Data.Repositories
{
    public interface IPausedMissionRepository
    {
        List<PausedMission> GetAllMissions();
        PausedMission GetMissionDetails(int pausedMissionId);
        void CreateMission(PausedMission pausedMission);
        void DeleteMission(int pausedMissionId);
        void UpdateMission(PausedMission pausedMission);
    }
}
