using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class RepairVoucherDetailRepository : RepositoryBase<MobileContext, RepairVoucherDetail> , IRepairVoucherDetailRepository
    {
        public RepairVoucherDetailRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface IRepairVoucherDetailRepository : IRepository<RepairVoucherDetail>
    {
    }
    
}
