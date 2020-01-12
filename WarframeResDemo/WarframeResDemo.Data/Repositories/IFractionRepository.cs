using System;
using System.Collections.Generic;
using System.Text;
using WarframeResDemo.Data.Entities;

namespace WarframeResDemo.Data.Repositories
{
    public interface IFractionRepository
    {
        List<Fraction> GetAllFractions();
        Fraction GetFractionDetails(int fractionId);
        void CreateFraction(Fraction fraction);
        void UpdateFraction(Fraction fraction);
        void DeleteFraction(int fractionId);
    }
}
