using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using WarframeResDemo.Data.Entities;
using WarframeResDemo.Data.Repositories;

namespace WarframeResDemo.EFr.Repositories
{
    public class EndedMissionRepository : IEndedMissionRepository
    {
        public void CreateMission(EndedMissions endedMission)
        {
            using (var ctx = new WarframeResDemoContext())
            {
                ctx.EndedMissions.Add(endedMission);
                ctx.SaveChanges();
            }
        }

        public void DeleteMission(int endedMissionId)
        {
            using (var ctx = new WarframeResDemoContext())
            {
                var mission = ctx.EndedMissions.Find(endedMissionId);
                ctx.EndedMissions.Remove(mission);
                ctx.SaveChanges();
            }
        }

        public List<EndedMissions> GetAllMissions()
        {
            using (var ctx = new WarframeResDemoContext())
            {
                return ctx.EndedMissions.ToList();
            }
        }

        public EndedMissions GetMissionDetails(int endedMissionId)
        {
            using (var ctx = new WarframeResDemoContext())
            {
                return ctx.EndedMissions.First(x => x.Id == endedMissionId);
            }
        }

        public void UpdateMission(EndedMissions endedMission)
        {
            using (var ctx = new WarframeResDemoContext())
            {
                ctx.EndedMissions.Attach(endedMission);
                ctx.Entry(endedMission).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }
    }
}
