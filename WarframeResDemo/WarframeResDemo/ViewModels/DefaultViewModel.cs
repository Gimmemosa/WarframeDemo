using System;
using System.Collections.Generic;
using System.Text;
using WarframeResDemo.Data.Entities;

namespace WarframeResDemo.ViewModels
{
    public class DefaultViewModel
    {
        public float Progress;
        public virtual void StartMission() { }
        public virtual void StopMission() { }
    }
}
