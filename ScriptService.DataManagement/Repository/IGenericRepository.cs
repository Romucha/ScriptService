﻿using ScriptService.Models.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ScriptService.DataManagement.Repository
{
    public interface IGenericRepository<T> where T : class
	{
		Task<IPagedList<T>> GetAll(
			RequestParams requestParams,
			Expression<Func<T, bool>> expression = null,
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
			List<string> includes = null);

		Task<T> Get(Expression<Func<T, bool>> expression = null, List<string> includes = null);

		Task Insert(T entity);

		Task InsertRange(IEnumerable<T> entities);

		void Update(T entity);

		Task Delete(int id);

		void DeleteRange(IEnumerable<T> entities);
	}
}
