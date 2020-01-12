using System;
using System.Collections.Generic;
using System.Text;
using WarframeResDemo.Data.Entities;

namespace WarframeResDemo.Data.Repositories
{
    public interface IResourceRepository
    {
        List<Resource> GetAllResources();
        Resource GetResourceDetails(int resourceId);
        void CreateResource(Resource resource);
        void UpdateResource(Resource resource);
        void DeleteResource(int resourceId);
    }
}
