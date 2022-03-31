using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivitiesList.Domain.Entities;

namespace ActivitiesList.Domain.Interfaces.Services
{
    public interface IActivityService
    {
        Task<Activity> AddActivity(Activity model);

        Task<Activity> UpdateActivity(Activity model);

        Task<bool> DeleteActivity(int activityId);

        Task<bool> CompleteActivity(Activity model);

        Task<Activity[]> GetAllActivitiesAsync();

        Task<Activity> GetActivityByIdAsync(int activityId);
    }
}
