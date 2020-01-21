using System;
using System.Collections.Generic;
using System.Text;
using WarframeResDemo.Data.Entities;

namespace WarframeResDemo.ViewModels
{
    public class DefaultViewModel
    {
        public Mission Mission;
        public Resource Resource;
        public PausedMission paused;
        public float Progress;
        public virtual void StartMission() { }
        public virtual void StopMission() { onMissionStop?.Invoke(paused); }

        public delegate void MissionStop(PausedMission mission);
        public event MissionStop onMissionStop;
    }
}
