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
    public class ResourceRepository : IResourceRepository
    {
        public void CreateResource(Resource resource)
        {
            using (var ctx = new WarframeResDemoContext())
            {
                ctx.Resources.Add(resource);
                ctx.SaveChanges();
            }
        }

        public void DeleteResource(int resourceId)
        {
            using (var ctx = new WarframeResDemoContext())
            {
                var resource = ctx.Resources.Find(resourceId);
                ctx.Resources.Remove(resource);
                ctx.SaveChanges();
            }
        }

        public List<Resource> GetAllResources()
        {
            using (var ctx = new WarframeResDemoContext())
            {
                return ctx.Resources.Include(r=>r.PlanetResource).ToList();
            }
        }

        public Resource GetResourceDetails(int resourceId)
        {
            using (var ctx = new WarframeResDemoContext())
            {
                return ctx.Resources.Include(r=> r.PlanetResource).First(x => x.Id == resourceId);
            }
        }

        public void UpdateResource(Resource resource)
        {
            using (var ctx = new WarframeResDemoContext())
            {
                ctx.Resources.Attach(resource);
                ctx.Entry(resource).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }
    }
}
