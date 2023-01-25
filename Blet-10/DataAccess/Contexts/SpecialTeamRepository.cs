using Core.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts;

public class SpecialTeamRepository : Repository<SpecialTeam>, ISpecialTeamRepository
{
	public SpecialTeamRepository(AppDbContext context) : base(context)
	{
	}
}
