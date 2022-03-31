using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivitiesList.Domain.Entities;

namespace ActivitiesList.Domain.Interfaces.Repositories
{
    public interface IActivityRepo: IGeneralRepo
    {
        Task<Activity[]> GetAllAsync();
        Task<Activity> GetByIdAsync(int id);

        Task<Activity> GetByTitleAsync(string title);
    }
}