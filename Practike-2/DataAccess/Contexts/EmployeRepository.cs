using Core.Entities;
using DataAccess.Interfaces;

namespace DataAccess.Contexts
{
	public class EmployeRepository : Repository<Employee>, IEmployeeRepository
	{
		public EmployeRepository(AppDbContext context) : base(context)
		{
		}
	}
}
