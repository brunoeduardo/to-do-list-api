using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivitiesList.API.Data;
using ActivitiesList.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ActivitiesList.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivityController : ControllerBase
    {
        private readonly DataContext _context;

        public ActivityController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Activity> Get()
        {
            return _context.Activities;
        }

        [HttpGet("{id}")]
        public Activity Get(int id)
        {
            return _context.Activities.FirstOrDefault(acty => acty.Id == id);
        }

        [HttpPost]
        public Activity Post(Activity activity)
        {
            _context.Activities.Add (activity);

            if (_context.SaveChanges() > 0)
                return activity;
            else
                throw new Exception("You didn't add an activity");
        }

        [HttpPut("{id}")]
        public Activity Put(int id, Activity activity)
        {
            if (activity.Id != id)
                throw new Exception("You can't update an activity with different id");

            _context.Update (activity);
            if (_context.SaveChanges() > 0)
                return _context
                    .Activities
                    .FirstOrDefault(acty => acty.Id == id);
            else
                return new Activity();
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            var activity =
                _context.Activities.FirstOrDefault(acty => acty.Id == id);
            if (activity == null)
                throw new Exception("There is not actvity to delete");
            _context.Remove (activity);

            return _context.SaveChanges() > 0;
        }
    }
}
