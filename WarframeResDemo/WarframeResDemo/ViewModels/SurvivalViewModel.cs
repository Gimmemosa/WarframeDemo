using System.Threading;
using WarframeResDemo.Data.Entities;
using System.ComponentModel;
using System.Windows;
using WarframeResDemo.Models;
using System.Threading.Tasks;
using System;

namespace WarframeResDemo.ViewModels
{
    public class SurvivalViewModel : DefaultViewModel, INotifyPropertyChanged
    {
        public static string time;
        private static int seconds;
        private static int minutes;
        private static Mission Mission;
        private static Task timerTask;
        private static CancellationTokenSource cancelTokenSource;
        private static CancellationToken token;

        public event PropertyChangedEventHandler PropertyChanged;

        public static int Seconds
        {
            get
            {
                return seconds;
            }
            set
            {
                seconds = value;
                while(seconds >= 60)
                {
                    Minutes++;
                    seconds -= 60;
                }
                TimeToString();
            }
        }
        public static int Minutes
        {
            get
            {
                return minutes;
            }
            set
            {
                minutes = value;
            }
        }
        public static string Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
                ((SurvivalViewModel)(Model.viewModel)).OnPropertyChanged("Time");
            }
        }

        #region Handlers
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion Handlers

        #region Methods
        public SurvivalViewModel()
        {
            Mission = Model.mission;
            Model.ViewModel = this;
            Minutes = 0;
            SurvivalType type = (SurvivalType)(Mission.MissionType);
            Seconds = Convert.ToInt32(Math.Round((Progress * (type.Time.Minute * 60 + type.Time.Second) / 100), MidpointRounding.ToEven));
        }
        private static void Count()
        {
            SurvivalType type = (SurvivalType)(Mission.MissionType);
            for (int i = 0; i <= (type.Time.Minute * 60 + type.Time.Second); i++)
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }
                Seconds++;
                Thread.Sleep(1000);
            }
            ((SurvivalViewModel)(Model.viewModel)).StopMission();
            return;
        }
        public void StopTimer()
        {
            cancelTokenSource.Cancel();
        }
        public static void TimeToString()
        {
            Time = string.Format("{0}:{1}", Minutes, Seconds);
        }
        public override void StartMission()
        {
            SurvivalType type = new SurvivalType();
            type = (SurvivalType)(Mission.MissionType);
            cancelTokenSource = new CancellationTokenSource();
            token = cancelTokenSource.Token;
            timerTask = new Task(() => Count());
            timerTask.Start();
        }
        public override void StopMission()
        {
            StopTimer();
            SurvivalType type = (SurvivalType)(Mission.MissionType);
            Progress = (float)(Minutes * 60 + Seconds) / (float)(type.Time.Minute * 60 + type.Time.Second) * 100;
            Model.PausedMission = new PausedMission
            {
                Progress = Progress,
                MissionId = Mission.Id
            };
        }
        #endregion Methods
    }
}
