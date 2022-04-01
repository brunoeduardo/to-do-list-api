using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivitiesList.Data.Context;
using ActivitiesList.Domain.Entities;
using ActivitiesList.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ActivitiesList.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService _activityService;

        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var activities = await _activityService.GetAllActivitiesAsync();
                if (activities == null) return NoContent();

                return Ok(activities);
            }
            catch (System.Exception ex)
            {
                return this
                    .StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error to get Activities. Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                var activities =
                    await _activityService.GetActivityByIdAsync(id);
                if (activities == null) return NoContent();

                return Ok(activities);
            }
            catch (System.Exception ex)
            {
                return this
                    .StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error to get Activity id: {id}. Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(Activity model)
        {
            try
            {
                var act = await _activityService.AddActivity(model);

                if (act == null) return NoContent();

                return Ok(act);
            }
            catch (System.Exception ex)
            {
                return this
                    .StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error to save the Activity. Error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Activity activity)
        {
            if (activity.Id != id)
                this
                    .StatusCode(StatusCodes.Status409Conflict,
                    $"You can't update an activity with different id");

            try
            {
                var act = await _activityService.UpdateActivity(activity);

                if (act == null) return NoContent();

                return Ok(act);
            }
            catch (System.Exception ex)
            {
                return this
                    .StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error to update activity Id: {id}. Error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var act = await _activityService.DeleteActivity(id);
                if (act)
                    return Ok(act);
                else
                    return BadRequest($"Something went wrong to delete this activity");
            }
            catch (System.Exception ex)
            {
                return this
                    .StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error to delete activity Id: {id}. Error: {ex.Message}");
            }
        }
    }
}
