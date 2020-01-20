using WarframeResDemo.Data.Entities;
using WarframeResDemo.ViewModels;

namespace WarframeResDemo.Models
{
    public static class Model
    {
        public static Mission mission;
        public static Resource resource;
        public static DefaultViewModel viewModel;
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
        public static DefaultViewModel ViewModel
        {
            get
            {
                return viewModel;
            }
            set
            {
                viewModel = null;
                viewModel = value;
                onViewModelChanged?.Invoke(viewModel);
            }
        }
        public delegate void VievModelChanged(DefaultViewModel viewModel);
        public static event VievModelChanged onViewModelChanged;
        public delegate void MissionStop(PausedMission mission);
        public static event MissionStop onMissionStop;
    }
}
