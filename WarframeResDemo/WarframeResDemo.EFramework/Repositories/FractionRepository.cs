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
    public class FractionRepository : IFractionRepository
    {
        public void CreateFraction(Fraction fraction)
        {
            using (var ctx = new WarframeResDemoContext())
            {
                ctx.Fractions.Add(fraction);
                ctx.SaveChanges();
            }
        }

        public void DeleteFraction(int fractionId)
        {
            using (var ctx = new WarframeResDemoContext())
            {
                var fraction = ctx.Fractions.Find(fractionId);
                ctx.Fractions.Remove(fraction);
                ctx.SaveChanges();
            }
        }

        public List<Fraction> GetAllFractions()
        {
            using (var ctx = new WarframeResDemoContext())
            {
                return ctx.Fractions.ToList();
            }
        }

        public Fraction GetFractionDetails(int fractionId)
        {
            using (var ctx = new WarframeResDemoContext())
            {
                return ctx.Fractions.First(x => x.Id == fractionId);
            }
        }

        public void UpdateFraction(Fraction fraction)
        {
            using (var ctx = new WarframeResDemoContext())
            {
                ctx.Fractions.Attach(fraction);
                ctx.Entry(fraction).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }
    }
}
