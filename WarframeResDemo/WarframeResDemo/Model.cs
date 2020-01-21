using WarframeResDemo.Data.Entities;
using WarframeResDemo.ViewModels;

namespace WarframeResDemo.Models.Deleted
{
    public static class Model
    {
        public static Mission mission;
        public static Resource resource;
        public static PausedMission pausedMission;
        public static PausedMission PausedMission
        {
            get
            {
                return pausedMission;
            }
            set
            {
                pausedMission = value;
                onMissionStop.Invoke(pausedMission);
            }
        }
        public delegate void MissionStop(PausedMission mission);
        public static event MissionStop onMissionStop;
    }
}
