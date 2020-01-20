using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using WarframeResDemo.Data.Entities;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using WarframeResDemo.Models;

namespace WarframeResDemo.ViewModels
{
    public class ExcavationVIewModel : DefaultViewModel, INotifyPropertyChanged
    {
        public const string PropertyName = nameof(Text);
        private string text;
        private int resourceCount;
        private static int needResource;

        private Mission Mission;
        private Resource Resource;

        public int ResourceCount
        {
            get
            {
                return resourceCount;
            }
            set
            {
                resourceCount = value;
                ResourceToString();
                if(resourceCount >= needResource && resourceCount != 0)
                {
                    StopMission();
                }
            }
        }
        public string Text
        {
            get => text;
            set
            {
                text = value;
                RaisePropertyChanged(PropertyName);
                OnPropertyChanged(PropertyName);
            }
        }

        #region Handlers

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand ButtonCommand { get; set; }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void MainButtonClick(object sender)
        {
            if (Resource.DropChance <= new Random().Next(0, 101))
            {
                ResourceCount++;
            }
        }
        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion Handlers

        #region Methods
        public ExcavationVIewModel()
        {
            ButtonCommand = new RelayCommand(o => MainButtonClick("MainButton"));
            Mission = Model.mission;
            Resource = Model.resource;
            Model.ViewModel = this;
            ExcavationType type = (ExcavationType)(Mission.MissionType);
            needResource = type.FarmedResource;
            ResourceCount = Convert.ToInt32(Math.Round(Progress * needResource / 100, MidpointRounding.ToEven));
        }
        public void ResourceToString()
        {
            Text = string.Format("{0}/{1}", ResourceCount, needResource);
        }
        public override void StartMission()
        {
            ResourceCount = (int)(needResource * Progress / 100);
        }
        public override void StopMission()
        {
            ExcavationType type = (ExcavationType)(Mission.MissionType);
            Progress = ResourceCount / type.FarmedResource * 100;
            Model.PausedMission = new PausedMission
            {
                Progress = Progress,
                MissionId = Model.mission.Id
            };
        }
        #endregion Methods
    }
}
