using System;
using System.Collections.Generic;
using System.Text;
using WarframeResDemo.Data.Entities;

namespace WarframeResDemo.Data.Repositories
{
    public interface IMissionTypeRepository
    {
        List<MissionType> GetAllTypes();
        MissionType GetTypeDetails(int typeId);
        void CreateType(MissionType type);
        void UpdateFraction(MissionType type);
        void DeleteFraction(int typeId);
    }
}
