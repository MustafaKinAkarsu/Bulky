using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
	public interface IRepository<T> where T : class
	{
		IEnumerable<T> GetAll();
		T Get(Expression<Func<T, bool>> filter); // Expression<Func<T, bool>> filter is a general syntax for implementing the LINQ lambda operation
		void Add(T entity);
		//void Update(T entity); Won't update in the Repository because in Update method, In EF Core, we need to save the changes and it is best to keep saving changes outside repository.
		void Delete(T entity);
		void DeleteRange(IEnumerable<T> entities);
	}
}
