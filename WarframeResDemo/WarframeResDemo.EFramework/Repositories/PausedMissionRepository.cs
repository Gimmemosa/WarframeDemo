using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using WarframeResDemo.Data.Entities;
using WarframeResDemo.Data.Repositories;

namespace WarframeResDemo.EFr.Repositories
{
    public class PausedMissionRepository : IPausedMissionRepository
    {
        public void CreateMission(PausedMission pausedMission)
        {
            using (var ctx = new WarframeResDemoContext())
            {
                ctx.PausedMissions.Add(pausedMission);
                ctx.SaveChanges();
            }
        }

        public void DeleteMission(int pausedMissionId)
        {
            using (var ctx = new WarframeResDemoContext())
            {
                var mission = ctx.PausedMissions.Find(pausedMissionId);
                ctx.PausedMissions.Remove(mission);
                ctx.SaveChanges();
            }
        }

        public List<PausedMission> GetAllMissions()
        {
            using (var ctx = new WarframeResDemoContext())
            {
                return ctx.PausedMissions.ToList();
            }
        }

        public PausedMission GetMissionDetails(int pausedMissionId)
        {
            using (var ctx = new WarframeResDemoContext())
            {
                return ctx.PausedMissions.First(x => x.Id == pausedMissionId);
            }
        }

        public void UpdateMission(PausedMission pausedMission)
        {
            using (var ctx = new WarframeResDemoContext())
            {
                ctx.PausedMissions.Attach(pausedMission);
                ctx.Entry(pausedMission).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }
    }
}
