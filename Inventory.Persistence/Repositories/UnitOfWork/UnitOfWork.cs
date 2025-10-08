using Inventory.Application.Contracts.Persistence.Repositories;
using Inventory.Persistence.Context;
using Inventory.Persistence.Repositories.GenericRepository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Persistence.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InventoryDbContext _dbContext;
        private readonly Dictionary<string, object> _repositories = new();
        public UnitOfWork(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> CompleteAsync()
        {
           return await _dbContext.SaveChangesAsync();
           
        }

        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }


        public IGenericRepository<T> Repositories<T>() where T : class
        {
            var typeName = typeof(T).FullName!;

            if (!_repositories.ContainsKey(typeName))
            {
                var repositoryInstance = new GenericRepository<T>(_dbContext);
                _repositories.Add(typeName, repositoryInstance);
            }

            return (IGenericRepository<T>)_repositories[typeName];
        }
    }
}
