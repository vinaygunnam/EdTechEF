using EdTech.Data.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Data.Common
{
    public class UnitOfWork<T> : IDisposable where T: DbContext
    {
        private T _context;

        protected T Context
        {
            get
            {
                if (_context != null) return _context;
                throw new NoNullAllowedException("DbContext must be instantiated inside the contructor");
            }
            set
            {
                _context = value;
            }
        }
        private DbContextTransaction _transaction;

        public List<System.Reflection.PropertyInfo> NavigationPropertiesFor<U>() where U : class
        {
            return Context.GetNavigationProperties<U>(typeof(U));
        }

        public void Save()
        {
            try
            {
                _transaction = Context.Database.BeginTransaction();
                Context.SaveChanges();
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Context != null)
                {
                    Context.Dispose();
                    Context = null;
                }
            }
        }
    }
}
