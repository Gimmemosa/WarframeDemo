using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarframeResDemo.Data.Entities;
using WarframeResDemo.Data.Repositories;

namespace WarframeResDemo.EFr.Repositories
{
    public class MissionTypeRepository : IMissionTypeRepository
    {
        public void CreateType(MissionType type)
        {
            using (var ctx = new WarframeResDemoContext())
            {
                ctx.MissionTypes.Add(type);
                ctx.SaveChanges();
            }
        }

        public void DeleteFraction(int typeId)
        {
            using (var ctx = new WarframeResDemoContext())
            {
                var type = ctx.MissionTypes.Find(typeId);
                ctx.MissionTypes.Remove(type);
                ctx.SaveChanges();
            }
        }

        public List<MissionType> GetAllTypes()
        {
            using (var ctx = new WarframeResDemoContext())
            {
                return ctx.MissionTypes.ToList();
            }
        }

        public MissionType GetTypeDetails(int typeId)
        {
            using (var ctx = new WarframeResDemoContext())
            {
                return ctx.MissionTypes.First(t => t.Id == typeId);
            }
        }

        public void UpdateFraction(MissionType type)
        {
            using (var ctx = new WarframeResDemoContext())
            {
                ctx.MissionTypes.Attach(type);
                ctx.Entry(type).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }
    }
}
