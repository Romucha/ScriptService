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
        private readonly ScriptDbContext _context;

        private IGenericRepository<Script> _scripts;
        public IGenericRepository<Script> Scripts 
        {
            get
            {
                if (_scripts == null)
                {
                    _scripts = new GenericRepository<Script>(_context);
                }
                return _scripts;
            }
        }

        public UnitOfWork(ScriptDbContext context)
        {
            _context = context;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
