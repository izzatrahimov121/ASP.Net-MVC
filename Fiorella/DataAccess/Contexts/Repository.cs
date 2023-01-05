using Core.Entities;
using Core.Interfaces;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace DataAccess.Contexts;

public class Repository<T> : IRepository<T> where T : class, IEntity, new()
{
	private readonly AppDbContext _context;
	public Repository(AppDbContext context)
	{
		_context = context;
		
	}
	public async Task<IEnumerable<T>> GetAllAsync()
	{
		return await _context.Set<T>().ToListAsync();
	}
	public async Task<T?> GetAll(int? id)
	{
		return await _context.Set<T>().FindAsync(id);
	}
	public async Task CreateAsync(T entity)
	{
		await _context.Set<T>().AddAsync(entity);
	}

	public void Delete(T entity)
	{
		_context.Set<T>().Remove(entity);
	}
	public void Update(T entity)
	{
		_context.Entry(entity).State = EntityState.Modified;
	}
	public async Task SaveAsync()
	{
		await _context.SaveChangesAsync();
	}
}
