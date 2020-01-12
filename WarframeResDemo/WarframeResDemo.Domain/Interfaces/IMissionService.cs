using System;
using System.Collections.Generic;
using System.Text;
using WarframeResDemo.Data.Entities;

namespace WarframeResDemo.Domain.Interfaces
{
    public interface IMissionService
    {
        void ChangeFraction(int missionId, int fractionId);
        void ChangeType(int missionId, int missionTypeId);
        void ChangeLevel(int missionId, int level);
        void PauseMission(int missionId, int resourceId, int resourceCount);
        int IsPaused(int missionId, int resourceId);
        void EndMission(int missionId, int resourceId);
    }
}
