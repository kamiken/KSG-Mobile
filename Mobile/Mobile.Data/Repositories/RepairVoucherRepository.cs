using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class RepairVoucherRepository : RepositoryBase<MobileContext, RepairVoucher> , IRepairVoucherRepository
    {
        public RepairVoucherRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface IRepairVoucherRepository : IRepository<RepairVoucher>
    {
    }
    
}
