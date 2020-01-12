using System;
using System.Collections.Generic;
using System.Text;

namespace WarframeResDemo.Domain.Interfaces
{
    public interface IResourceService
    {
        void ChangeDropChance(int resourceId, float dropChance);
        bool FarmResource(int resourceId);
        int HowManyFarm(int resourceId/*, int missionId*/);
    }
}
