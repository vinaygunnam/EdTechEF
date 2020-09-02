using EdTech.Data.Common;
using EdTech.Data.ZetaDestination.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Data.ZetaDestination
{
    public class UnitOfWorkForZeta : UnitOfWork<NWDestinationEntities>
    {
        public CategoryRepository CategoryRepository { get; private set; }

        public UnitOfWorkForZeta()
        {
            Context = new NWDestinationEntities();
            CategoryRepository = new CategoryRepository(Context);
        }
    }
}
