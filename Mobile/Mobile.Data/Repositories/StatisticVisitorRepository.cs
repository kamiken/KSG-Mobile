using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class StatisticVisitorRepository : RepositoryBase<MobileContext, StatisticVisitor> , IStatisticVisitorRepository
    {
        public StatisticVisitorRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface IStatisticVisitorRepository : IRepository<StatisticVisitor>
    {
    }
    
}
