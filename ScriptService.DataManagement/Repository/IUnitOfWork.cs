using ScriptService.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptService.DataManagement.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Script> Scripts { get; }

        Task Save();
    }
}
