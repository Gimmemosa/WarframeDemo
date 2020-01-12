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
    public class MissionRepository : IMissionRepository
    {
        public void CreateMission(Mission mission)
        {
            using (var ctx = new WarframeResDemoContext())
            {
                ctx.Missions.Add(mission);
                ctx.SaveChanges();
            }
        }
        public void UpdateMission(Mission mission)
        {
            using (var ctx = new WarframeResDemoContext())
            {
                ctx.Missions.Attach(mission);
                ctx.Entry(mission).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public void DeleteMission(int missionId)
        {
            using (var ctx = new WarframeResDemoContext())
            {
                var mission = ctx.Missions.Find(missionId);
                ctx.Missions.Remove(mission);
                ctx.SaveChanges();
            }
        }

        public List<Mission> GetAllMissions()
        {
            using (var ctx = new WarframeResDemoContext())
            {
                return ctx.Missions.Include(m=>m.Fraction).Include(m=>m.MissionType).ToList();
            }
        }

        public Mission GetMissionDetails(int missionId)
        {
            using (var ctx = new WarframeResDemoContext())
            {
                return ctx.Missions.Include(m => m.Fraction).Include(m => m.MissionType).First(x => x.Id == missionId);
            }
        }
    }
}
