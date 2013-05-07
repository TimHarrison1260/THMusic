using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using THMusic.DataModel;

namespace THMusic.Data
{
    public interface IGroupModelDataService
    {
        Task<List<GroupModel>> LoadAsync(GroupTypeEnum groupType);
    }
}
