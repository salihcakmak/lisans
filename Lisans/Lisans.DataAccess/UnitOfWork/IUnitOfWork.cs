using Lisans.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lisans.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;
        int SaveChanges();
    }
}
