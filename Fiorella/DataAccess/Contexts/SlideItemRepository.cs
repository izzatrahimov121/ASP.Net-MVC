using Core.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contexts;

public class SlideItemRepository : Repository<SlideItem>, ISlideItemRepository
{
	public SlideItemRepository(AppDbContext context) : base(context)
	{
	}
}
