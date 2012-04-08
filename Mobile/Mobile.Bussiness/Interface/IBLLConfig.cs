using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Object;
using Mobile.Framework;

namespace Mobile.Business
{
    public interface IBLLConfig
    {
        SystemConfig GetConfig(string configId);
        IQueryable<SystemConfig> GetConfigs(string keyword, bool? isActived);
        PagedResult<SystemConfig> GetConfigsPaging(string keyword, bool? isActived, PagingInput pagingInput);
        bool AddConfig(SystemConfig objConfig);
        bool DeleteConfig(string configId, int userId);
        bool UpdateConfig(SystemConfig objConfig);        
    }
}
