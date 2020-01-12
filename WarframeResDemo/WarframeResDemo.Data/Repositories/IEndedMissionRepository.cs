using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarframeResDemo.Data.Entities;

namespace WarframeResDemo.Data.Repositories
{
    public interface IEndedMissionRepository
    {
        List<EndedMissions> GetAllMissions();
        EndedMissions GetMissionDetails(int endedMissionId);
        void CreateMission(EndedMissions endedMission);
        void DeleteMission(int endedMissionId);
        void UpdateMission(EndedMissions endedMission);
    }
}
