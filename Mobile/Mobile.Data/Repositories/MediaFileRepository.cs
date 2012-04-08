using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class MediaFileRepository : RepositoryBase<MobileContext, MediaFile> , IMediaFileRepository
    {
        public MediaFileRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface IMediaFileRepository : IRepository<MediaFile>
    {
    }
    
}
