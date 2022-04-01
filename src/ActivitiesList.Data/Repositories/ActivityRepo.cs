using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivitiesList.Data.Context;
using ActivitiesList.Domain.Entities;
using ActivitiesList.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ActivitiesList.Data.Repositories
{
    public class ActivityRepo : GeneralRepo, IActivityRepo
    {
        private readonly DataContext _context;

        public ActivityRepo(DataContext context) :
            base(context)
        {
            _context = context;
        }

        public async Task<Activity[]> GetAllAsync()
        {
            IQueryable<Activity> query = _context.Activities;

            query = query.AsNoTracking().OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Activity> GetByIdAsync(int id)
        {
            IQueryable<Activity> query = _context.Activities;

            query =
                query.AsNoTracking().OrderBy(e => e.Id).Where(a => a.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Activity> GetByTitleAsync(string title)
        {
            IQueryable<Activity> query = _context.Activities;

            query =
                query
                    .AsNoTracking()
                    .OrderBy(e => e.Title)
                    .Where(a => a.Title == title);

            return await query.FirstOrDefaultAsync();
        }
    }
}
