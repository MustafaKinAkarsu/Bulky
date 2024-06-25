using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _db;
		internal DbSet<T> dbSet; // To add generic type entities, we use DbSet. Normally, when we know the type of the entitity, we add it by _db.Category.Add etc.

        public Repository(ApplicationDbContext db)
        {
            _db = db;
			this.dbSet = _db.Set<T>();
        }

        public void Add(T entity)
		{
			dbSet.Add(entity);
		}

		public void Delete(T entity)
		{
			dbSet.Remove(entity);
		}

		public void DeleteRange(IEnumerable<T> entities)
		{
			dbSet.RemoveRange(entities);
		}

		public T Get(Expression<Func<T, bool>> filter)
		{
			IQueryable<T> query = dbSet;
			query = query.Where(filter);
			return query.FirstOrDefault();
			//Yukarıda yapılanlar, Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault() aynısıdır.
		}

		public IEnumerable<T> GetAll()
		{
			IQueryable<T> query = dbSet;
			return query.ToList();
		}
	}
}
