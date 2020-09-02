using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EdTech.Data.Common;

namespace EdTech.Data.ZetaDestination.Repositories
{
    public class CategoryRepository : GenericRepository<Category, NWDestinationEntities>
    {
        public CategoryRepository(NWDestinationEntities entities) : base(entities)
        {
        }

        public void Upsert(IEnumerable<Category> collection)
        {
            if (collection == null || collection.Count() == 0) return;

            var primaryKeys = collection.Select(c => c.CategoryID);
            var existingRecords = this.SelectAll().Where(c => primaryKeys.Contains(c.CategoryID)).ToList();

            collection.ToList().ForEach(categoryToBeUpserted =>
            {
                var match = existingRecords.SingleOrDefault(c => c.CategoryID == categoryToBeUpserted.CategoryID);
                if (match == null)
                {
                    // insert the record
                    Insert(categoryToBeUpserted);
                }
                else
                {
                    Update(categoryToBeUpserted);
                }
            });
        }
    }
}
