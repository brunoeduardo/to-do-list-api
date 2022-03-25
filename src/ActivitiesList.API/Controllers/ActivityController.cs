using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivitiesList.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ActivitiesList.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivityController : ControllerBase
    {
        [HttpGet]
        public Activity Get()
        {
            return new Activity();
        }
        
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return $"Get params {id}. Works!";
        }

        [HttpPost]
        public Activity Post(Activity activity) {
            return activity;
        }

        [HttpPut("{id}")]
        public string Put(int id)
        {
            return "Put Works!";
        }        

        [HttpDelete("{id}")]
        public string Delete(int id) {
            return "Delete Works!";
        }
    }
}