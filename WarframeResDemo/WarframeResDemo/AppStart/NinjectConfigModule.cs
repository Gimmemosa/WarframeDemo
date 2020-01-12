using System;
using System.Collections.Generic;
using System.Text;
using Ninject;
using Ninject.Modules;
using WarframeResDemo.Data.Repositories;
using WarframeResDemo.EFr.Repositories;
using WarframeResDemo.Domain.Interfaces;
using WarframeResDemo.Domain.DefaultImplementations;

namespace WarframeResDemo.AppStart
{
    class NinjectConfigModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPlanetRepository>().To<PlanetRepository>();
            Bind<IResourceRepository>().To<ResourceRepository>();
            Bind<IMissionRepository>().To<MissionRepository>();
            Bind<IMissionTypeRepository>().To<MissionTypeRepository>();
            Bind<IFractionRepository>().To<FractionRepository>();
            Bind<IEndedMissionRepository>().To<EndedMissionRepository>();
            Bind<IPausedMissionRepository>().To<PausedMissionRepository>();

            Bind<IPlanetService>().To<PlanetService>();
            Bind<IMissionService>().To<MissionService>();
            Bind<IResourceService>().To<ResourceService>();
        }
    }
}
