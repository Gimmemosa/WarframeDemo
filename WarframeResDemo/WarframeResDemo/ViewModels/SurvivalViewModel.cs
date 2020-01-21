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
        private const string PropertyName = nameof(Time);
        public string time;
        private int seconds;
        private int minutes;
        private Task timerTask;
        private CancellationTokenSource cancelTokenSource;
        private CancellationToken token;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Seconds
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
        public int Minutes
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
        public string Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
                this.OnPropertyChanged(PropertyName);
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
        public SurvivalViewModel(float progress, Mission mission, Resource resource)
        {
            Progress = progress;
            Mission = mission;
            Resource = resource;
            Minutes = 0;
            SurvivalType type = (SurvivalType)(mission.MissionType);
            Seconds = Convert.ToInt32(Math.Round((Progress * (type.Time.Minute * 60 + type.Time.Second) / 100), MidpointRounding.ToEven));
        }
        private void Count()
        {
            SurvivalType type = (SurvivalType)(Mission.MissionType);
            int less = (type.Time.Minute * 60 + type.Time.Second) - (Minutes * 60 + Seconds);
            for (int i = 0; i <= less; i++)
            {
                Thread.Sleep(1000);
                if (token.IsCancellationRequested)
                {
                    return;
                }
                Seconds++;
            }
            StopMission();
            return;
        }
        public void StopTimer()
        {
            cancelTokenSource.Cancel();
        }
        public void TimeToString()
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
            paused = new PausedMission
            {
                Progress = Progress,
                MissionId = Mission.Id
            };
            base.StopMission();
        }
        #endregion Methods
    }
}
