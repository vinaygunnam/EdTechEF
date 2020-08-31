using EdTech.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Data.AlphaSource
{
    public class UnitOfWorkForAlpha : UnitOfWork<NWSourceEntities>
    {
        public GenericRepository<Category, NWSourceEntities> CategoryRepository { get; private set; }

        public UnitOfWorkForAlpha()
        {
            Context = new NWSourceEntities();
            CategoryRepository = new GenericRepository<Category, NWSourceEntities>(Context);
        }
    }
}
