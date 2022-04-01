using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivitiesList.Data.Context;
using ActivitiesList.Domain.Interfaces.Repositories;

namespace ActivitiesList.Data.Repositories
{
    public class GeneralRepo : IGeneralRepo
    {
        private readonly DataContext _context;

        public GeneralRepo(DataContext context)
        {
            _context = context;
        }        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteSeveral<T>(T[] entityArray) where T : class
        {
            _context.Remove(entityArray);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
    }
}