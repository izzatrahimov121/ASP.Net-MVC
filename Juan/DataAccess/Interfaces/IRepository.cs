using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces;

public interface IRepository<T> where T: class,IEntity,new()
{
	Task<IEnumerable<T>> GetAllAsync();
	Task<T?> GetAsync(int? id);
	Task CreateAsync(T entity);
	void Update(T entity);
	void Delete(T entity);
	Task SaveAsync();

}
