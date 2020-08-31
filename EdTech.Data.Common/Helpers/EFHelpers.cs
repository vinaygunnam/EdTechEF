using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Data.Common.Helpers
{
    public static class EFHelpers
    {
        public static List<System.Reflection.PropertyInfo> GetNavigationProperties<T>(this DbContext context, Type entityType) where T : class
        {
            List<System.Reflection.PropertyInfo> properties = new List<System.Reflection.PropertyInfo>();
            //Get the System.Data.Entity.Core.Metadata.Edm.EntityType
            //associated with the entity.
            var entitySetElementType = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)context).ObjectContext.CreateObjectSet<T>().EntitySet.ElementType;
            //Iterate each 
            //System.Data.Entity.Core.Metadata.Edm.NavigationProperty
            //in EntityType.NavigationProperties, get the actual property 
            //using the entityType name, and add it to the return set.
            foreach (var navigationProperty in entitySetElementType.NavigationProperties)
            {
                properties.Add(entityType.GetProperty(navigationProperty.Name));
            }
            return properties;
        }
    }
}
