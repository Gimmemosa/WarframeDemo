using System;
using System.Collections.Generic;
using System.Text;
using WarframeResDemo.Data.Entities;
using WarframeResDemo.Data.Repositories;
using WarframeResDemo.Domain.Interfaces;

namespace WarframeResDemo.Domain.DefaultImplementations
{
    public class ResourceService : IResourceService
    {
        private IResourceRepository _resourceRepository;
        private IMissionRepository _missionRepository;
        public ResourceService(IResourceRepository resourceRepository, IMissionRepository missionRepository)
        {
            _resourceRepository = resourceRepository;
            _missionRepository = missionRepository;
        }

        #region IResourceService Members
        public void ChangeDropChance(int resourceId, float dropChance)
        {
            if (dropChance > 0 && dropChance <= 100)
            {
                var resource = _resourceRepository.GetResourceDetails(resourceId);
                resource.DropChance = dropChance;
                _resourceRepository.UpdateResource(resource);
            }
        }

        public bool FarmResource(int resourceId)
        {
            float dropped = new Random().Next(101);
            if (dropped <= _resourceRepository.GetResourceDetails(resourceId).DropChance)
            {
                return true;
            }
            else return false;
        }

        public int HowManyFarm(int resourceId)
        {
            return 10;
        }
        #endregion
    }
}
