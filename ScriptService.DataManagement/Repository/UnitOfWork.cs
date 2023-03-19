using Microsoft.EntityFrameworkCore;
using ScriptService.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptService.DataManagement.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        private IGenericRepository<Script> _scripts;
        public IGenericRepository<Script> Scripts => _scripts ?? new GenericRepository<Script>(_context);

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public async Task Save()
        {
            throw new NotImplementedException();
        }
    }
}
