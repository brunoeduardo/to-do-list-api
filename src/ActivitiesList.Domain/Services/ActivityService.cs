using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivitiesList.Domain.Entities;
using ActivitiesList.Domain.Interfaces.Repositories;
using ActivitiesList.Domain.Interfaces.Services;

namespace ActivitiesList.Domain.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepo _activityRepo;

        public ActivityService(IActivityRepo activityRepo)
        {
            _activityRepo = activityRepo;
        }

        public async Task<Activity> AddActivity(Activity model)
        {
            if (await _activityRepo.GetByTitleAsync(model.Title) != null)
                throw new Exception("There is already an activity with the same title ");

            if (await _activityRepo.GetByIdAsync(model.Id) == null)
            {
                _activityRepo.Add (model);
                if (await _activityRepo.SaveChangesAsync()) return model;
            }

            return null;
        }

        public async Task<bool> CompleteActivity(Activity model)
        {
            if (model != null)
            {
                model.Conclusion();
                _activityRepo.Update<Activity> (model);
                return await _activityRepo.SaveChangesAsync();
            }

            return false;
        }

        public async Task<bool> DeleteActivity(int activityId)
        {
            var activity = await _activityRepo.GetByIdAsync(activityId);

            if (activity == null)
                throw new Exception("Activity does not exist");

            _activityRepo.Delete<Activity> (activity);
            return await _activityRepo.SaveChangesAsync();
        }

        public async Task<Activity> GetActivityByIdAsync(int activityId)
        {
            try
            {
                var activities = await _activityRepo.GetByIdAsync(activityId);
                if (activities == null) return null;

                return activities;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Activity[]> GetAllActivitiesAsync()
        {
             try
            {
                var activity= await _activityRepo.GetAllAsync();
                if (activity == null ) return null;

                return activity;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Activity> UpdateActivity(Activity model)
        {
            if (model.ConclusionDate != null)
                throw new Exception("You can't update a completed activity ");

            if (await _activityRepo.GetByIdAsync(model.Id) != null)
            {
                _activityRepo.Update (model);
                if (await _activityRepo.SaveChangesAsync()) return model;
            }

            return null;
        }
    }
}
