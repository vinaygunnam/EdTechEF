using EdTech.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Data.ZetaDestination
{
    public class UnitOfWorkForZeta : UnitOfWork<NWDestinationEntities>
    {
        public GenericRepository<Customer, NWDestinationEntities> CustomerRepository { get; private set; }

        public UnitOfWorkForZeta()
        {
            Context = new NWDestinationEntities();
            CustomerRepository = new GenericRepository<Customer, NWDestinationEntities>(Context);
        }
    }
}
