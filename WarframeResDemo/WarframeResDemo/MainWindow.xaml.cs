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
using WarframeResDemo.AppStart;

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
        public int FarmedResourceCount;
        public int ClickCount;

        public MainWindow()
        {
            InitializeComponent();


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

            Default();

            resourceCollection = new ObservableCollection<string>();
            planetCollection = new ObservableCollection<string>();
            missionCollection = new ObservableCollection<string>();
            FarmedResourceCount = 0;
            ClickCount = 0;

            resourcesListBox.ItemsSource = resourceCollection;
            planetsListBox.ItemsSource = planetCollection;
            missionsListBox.ItemsSource = missionCollection;

            resourcesListBox.SelectionChanged += new SelectionChangedEventHandler(ResourceListBox_Click);
            planetsListBox.SelectionChanged += new SelectionChangedEventHandler(PlanetListBox_Click);
            missionsListBox.SelectionChanged += new SelectionChangedEventHandler(MissionListBox_Click);
            farmButton.Click += new RoutedEventHandler(FarmButton_Click);

            LoadResources();
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
                        GetAllPlanetsByResource(r.Id);
                    }
                });
            }
            StopFarm();
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
                        GetAllMissionsByPlanet(p.Id);
                    }
                });
            }
            StopFarm();
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
                        StopFarm();
                        ShowInfo(m.Id);
                    }
                });
            }
        }

        public void FarmButton_Click(object sender, EventArgs e)
        {
            Farming();
            ClickCount++;
            clickLabel.Content = string.Format("Clicks: {0}", ClickCount);
        }
        #endregion Handlers

        #region Methods
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

            //List<Planet> planetList = planetRepo.GetAllPlanets();
            //planetList.ForEach(p =>
            //{
            //    planetCollection.Add(p.PlanetName);
            //});

            //List<Mission> missionsList = missionRepo.GetAllMissions();
            //missionsList.ForEach(m =>
            //{
            //    missionCollection.Add(m.MissionName);
            //});
        }
        public void GetAllPlanetsByResource(int resourceId)
        {
            planetCollection.Clear();
            missionCollection.Clear();
            List<Planet> planets = new List<Planet>();
            planetRepo.GetAllPlanets().ForEach(p =>
            {
                p.PlanetResource.ForEach(r =>
                {
                    if (r.ResourceId == resourceId)
                    {
                        planets.Add(p);
                    }
                });
            });
            planetCollection.Clear();
            planets.ForEach(p =>
            {
                planetCollection.Add(p.PlanetName);
            });
            planetsListBox.UpdateLayout();
        }
        public void GetAllMissionsByPlanet(int planetId)
        {
            missionCollection.Clear();
            List<Mission> missions = new List<Mission>();
            missionRepo.GetAllMissions().ForEach(m =>
            {
                if (m.PlanetId == planetId)
                {
                    missions.Add(m);
                }
            });
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
            if (missionRepo.GetMissionDetails(missionId).MissionType.Type == "Excavation")
            {
                StartFarm();
            }
        }

        public void StartFarm()
        {
            farmButton.Visibility = Visibility.Visible;
            farmLabel.Visibility = Visibility.Visible;
            farmButton.IsEnabled = true;
            farmLabel.Content = "0";
            clickLabel.Visibility = Visibility.Visible;
            clickLabel.Content = "Clicks: 0"; 
            int missionId = 0, resourceId = 0;
            missionRepo.GetAllMissions().ForEach(m =>
            {
                if (missionsListBox.SelectedItem != null)
                {
                    if (m.MissionName == missionsListBox.SelectedItem.ToString())
                    {
                        missionId = m.Id;
                    }
                }
            });
            resourceRepo.GetAllResources().ForEach(r =>
            {
                if (resourcesListBox.SelectedItem != null)
                {
                    if (r.ResourceName == resourcesListBox.SelectedItem.ToString())
                    {
                        resourceId = r.Id;
                    }
                }
            });
            int pausedId = missionSer.IsPaused(missionId, resourceId);
            if (pausedId != 0)
            {
                PausedMission mission = pausedRepo.GetMissionDetails(pausedId);
                farmLabel.Content = string.Format("{0}/{1}", mission.ResourceCount, resourceSer.HowManyFarm(resourceId));
            }
        }

        public void StopFarm()
        {
            FarmedResourceCount = 0;
            ClickCount = 0;
            farmButton.Visibility = Visibility.Hidden;
            farmLabel.Visibility = Visibility.Hidden;
            farmLabel.Content = "0";
            clickLabel.Visibility = Visibility.Hidden;
            clickLabel.Content = "Clicks: 0";
            int missionId = 0, resourceId = 0;
            missionRepo.GetAllMissions().ForEach(m =>
            {
                if (missionsListBox.SelectedItem != null)
                {
                    if (m.MissionName == missionsListBox.SelectedItem.ToString())
                    {
                        missionId = m.Id;
                    }
                }
            });
            if (missionId != 0)
            {
                if (missionRepo.GetMissionDetails(missionId).MissionType.Type == "Excavation")
                {
                    resourceRepo.GetAllResources().ForEach(r =>
                    {
                        if (resourcesListBox.SelectedItem != null)
                        {
                            if (r.ResourceName == resourcesListBox.SelectedItem.ToString())
                            {
                                resourceId = r.Id;
                            }
                        }
                    });
                    missionSer.PauseMission(missionId, resourceId, FarmedResourceCount);
                }
            }
        }

        public void Farming()
        {
            resourceRepo.GetAllResources().ForEach(r =>
            {
                if (r.ResourceName == resourcesListBox.SelectedItem.ToString())
                {
                    if (resourceSer.FarmResource(r.Id))
                    {
                        FarmedResourceCount++;
                        farmLabel.Content = string.Format("{0}/{1}", FarmedResourceCount, resourceSer.HowManyFarm(r.Id));
                        if (FarmedResourceCount >= resourceSer.HowManyFarm(r.Id))
                        {
                            EndFarm();
                        }
                    }
                }
            });
        }

        public void EndFarm()
        {
            farmButton.IsEnabled = false;
            int missionId = 0, resourceId = 0;
            missionRepo.GetAllMissions().ForEach(m =>
            {
                if (m.MissionName == missionsListBox.SelectedItem.ToString())
                {
                    missionId = m.Id;
                }
            });
            resourceRepo.GetAllResources().ForEach(r =>
            {
                if (r.ResourceName == resourcesListBox.SelectedItem.ToString())
                {
                    resourceId = r.Id;
                }
            });
            missionSer.EndMission(missionId, resourceId);
        }
        #endregion Methods

        public void Default()
        {
            //Fraction fraction = new Fraction();
            //fraction.Id = 0;
            //fraction.FractionName = "Grineer";

            //MissionType type = new MissionType();
            //type.Id = 0;
            //type.Type = "Defence";

            //Mission mission = new Mission();
            //mission.Id = 0;
            //mission.Fraction = fraction;
            //mission.FractionId = fraction.Id;
            //mission.MissionLevel = 42;
            //mission.MissionType = type;
            //mission.MissionTypeId = type.Id;
            //mission.MissionName = "Hydron";

            //Resource resource = new Resource();
            //resource.Id = 0;
            //resource.DropChance = 35;
            //resource.ResourceName = "Ferrit";

            //Planet planet = new Planet();
            //planet.Id = 10;
            //planet.Missions.Add(mission);
            //planet.PlanetName = "Sedna";
            //planet.Resources.Add(resource);
            //resource.Planets.Add(planet);

            //fractionRepo.CreateFraction(fraction);
            //typeRepo.CreateType(type);
            //planetRepo.CreatePlanet(planet);
            //missionRepo.CreateMission(mission);
            //resourceRepo.CreateResource(resource);
            //missionRepo.DeleteMission(3);
            //missionRepo.DeleteMission(4);

        }
    }
}
