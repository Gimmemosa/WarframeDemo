using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ninject;
using WarframeResDemo.EFr.Repositories;
using WarframeResDemo.Data.Entities;
using WarframeResDemo.Data.Repositories;
using WarframeResDemo.Domain.Interfaces;
using WarframeResDemo.ViewModels;
using WarframeResDemo.AppStart;
using WarframeResDemo.Models;

namespace WarframeResDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IResourceRepository resourceRepo;
        private IPlanetRepository planetRepo;
        private IMissionRepository missionRepo;
        private IFractionRepository fractionRepo;
        private IMissionTypeRepository typeRepo;
        private IPausedMissionRepository pausedRepo;
        private IEndedMissionRepository endedRepo;
        private IPlanetService planetSer;
        private IMissionService missionSer;
        private IResourceService resourceSer;

        public ObservableCollection<string> resourceCollection;
        public ObservableCollection<string> planetCollection;
        public ObservableCollection<string> missionCollection;

        public DefaultViewModel ViewModel;
        public MainWindow window;

        public void FillDatabase()
        {
            //var obj = new Mission
            //{
            //    FractionId = 3,
            //    MissionLevel = 20,
            //    PlanetId = 7,
            //    MissionTypeId = 7,
            //    MissionName = "Exc"
            //};
            //missionRepo.CreateMission(obj);
            //var obj2 = new Mission
            //{
            //    FractionId = 3,
            //    MissionLevel = 20,
            //    PlanetId = 7,
            //    MissionTypeId = 8,
            //    MissionName = "Surv"
            //};
            //missionRepo.CreateMission(obj2);
        }

        #region Handlers
        public void ResourceListBox_Click(object sender, EventArgs e)
        {
            if (resourcesListBox.SelectedItem != null)
            {
                string str = resourcesListBox.SelectedValue.ToString();
                resourceRepo.GetAllResources().ForEach(r =>
                {
                    if (r.ResourceName == str)
                    {
                        FillPlanetsList(r.Id);
                    }
                });
            }
        }
        public void PlanetListBox_Click(object sender, EventArgs e)
        {
            if (planetsListBox.SelectedItem != null)
            {
                string str = planetsListBox.SelectedValue.ToString();
                planetRepo.GetAllPlanets().ForEach(p =>
                {
                    if (p.PlanetName == str)
                    {
                        FillMissionsList(p.Id);
                    }
                });
            }
        }
        public void MissionListBox_Click(object sender, EventArgs e)
        {
            if (missionsListBox.SelectedItem != null)
            {
                string str = missionsListBox.SelectedValue.ToString();
                missionRepo.GetAllMissions().ForEach(m =>
                {
                    if (m.MissionName == str)
                    {
                        ShowInfo(m.Id);
                    }
                });
            }
        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            MissionType type = new MissionType();
            Mission mission = new Mission();
            Resource res = new Resource();
            float progress = 0;
            missionRepo.GetAllMissions().ForEach(m =>
            {
                if (missionsListBox.SelectedItem != null)
                {
                    if (m.MissionName == missionsListBox.SelectedItem.ToString())
                    {
                        type = m.MissionType;
                        mission = m;
                    }
                }
            });

            pausedRepo.GetAllMissions().ForEach(pM =>
            {
                if (mission.Id == pM.MissionId)
                {
                    progress = pM.Progress;
                    pausedRepo.DeleteMission(pM.Id);
                }
            });

            if (resourcesListBox.SelectedItem != null)
            {
                string str = resourcesListBox.SelectedValue.ToString();
                resourceRepo.GetAllResources().ForEach(r =>
                {
                    if (r.ResourceName == str)
                    {
                        res = r;
                    }
                });
            }
            ViewModel?.StopMission();
            ViewModel = new DefaultViewModel();

            /*------Добавление нового типа------*/
            if (type.GetType() == typeof(ExcavationType))
            {
                DataContext = ViewModel = new ExcavationVIewModel(progress, mission, res);
            }
            else if (type.GetType() == typeof(SurvivalType))
            {
                DataContext = ViewModel = new SurvivalViewModel(progress, mission, res);
            }
            else
            {
                DataContext = ViewModel = new DefaultViewModel();
            }
            /*----------------------------------*/
            ViewModel.onMissionStop += onMissionStopHandler;
            ViewModel.StartMission();
        }
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.StopMission();
        }
        public void onMissionStopHandler(PausedMission mission)
        {
            if (mission.Progress >= 100)
            {
                EndedMissions ended = new EndedMissions
                {
                    MissionId = mission.MissionId
                };
                endedRepo.CreateMission(ended);
                MessageBox.Show("ура товарищи! Мы смогли!");
            }
            else
                pausedRepo.CreateMission(mission);
            ViewModel.onMissionStop -= onMissionStopHandler;
            Dispatcher.Invoke(new Action(() => DataContext = new DefaultViewModel()));
        }
        #endregion Handlers

        #region Methods
        public MainWindow()
        {
            InitializeComponent();
            window = this;

            IKernel kernel;
            kernel = new StandardKernel(new NinjectConfigModule());

            resourceRepo = kernel.Get<IResourceRepository>();
            planetRepo = kernel.Get<IPlanetRepository>();
            missionRepo = kernel.Get<IMissionRepository>();
            fractionRepo = kernel.Get<IFractionRepository>();
            typeRepo = kernel.Get<IMissionTypeRepository>();
            pausedRepo = kernel.Get<IPausedMissionRepository>();
            endedRepo = kernel.Get<IEndedMissionRepository>();
            planetSer = kernel.Get<IPlanetService>();
            missionSer = kernel.Get<IMissionService>();
            resourceSer = kernel.Get<IResourceService>();

            resourceCollection = new ObservableCollection<string>();
            planetCollection = new ObservableCollection<string>();
            missionCollection = new ObservableCollection<string>();

            resourcesListBox.ItemsSource = resourceCollection;
            planetsListBox.ItemsSource = planetCollection;
            missionsListBox.ItemsSource = missionCollection;

            resourcesListBox.SelectionChanged += new SelectionChangedEventHandler(ResourceListBox_Click);
            planetsListBox.SelectionChanged += new SelectionChangedEventHandler(PlanetListBox_Click);
            missionsListBox.SelectionChanged += new SelectionChangedEventHandler(MissionListBox_Click);

            FillDatabase();
            LoadResources();
        }
        public void LoadResources()
        {
            resourceCollection.Clear();
            planetCollection.Clear();
            missionCollection.Clear();
            List<Resource> resourceList = resourceRepo.GetAllResources();
            resourceList.ForEach(r =>
            {
                resourceCollection.Add(r.ResourceName);
            });
        }
        public void FillPlanetsList(int resourceId)
        {
            planetCollection.Clear();
            missionCollection.Clear();
            List<Planet> planets = planetSer.GetAllPlanetsByResource(resourceId);
            planets.ForEach(p =>
            {
                planetCollection.Add(p.PlanetName);
            });
            planetsListBox.UpdateLayout();
        }
        public void FillMissionsList(int planetId)
        {
            missionCollection.Clear();
            List<Mission> missions = missionSer.GetAllMissionsByPlanet(planetId);
            missions.ForEach(m =>
            {
                missionCollection.Add(m.MissionName);
            });
        }
        public void ShowInfo(int missionId)
        {
            planetTextBox.Text = "Planet: " + planetsListBox.SelectedItem.ToString();
            missionTextBox.Text = "Mission: " + missionsListBox.SelectedItem.ToString();
            typeTextBox.Text = "Type: " + missionRepo.GetMissionDetails(missionId).MissionType.Type;
            levelTextBox.Text = "Level: " + missionRepo.GetMissionDetails(missionId).MissionLevel.ToString();
            fractionTextBox.Text = "Fraction: " + missionRepo.GetMissionDetails(missionId).Fraction.FractionName;
            resourceTextBox.Text = "Resource: " + resourcesListBox.SelectedItem.ToString();
            resourceRepo.GetAllResources().ForEach(r =>
            {
                if (r.ResourceName == resourcesListBox.SelectedItem.ToString())
                {
                    dropChanceTextBox.Text = "DropChanse: " + r.DropChance;
                }
            });
        }
        #endregion Methods
    }
}
