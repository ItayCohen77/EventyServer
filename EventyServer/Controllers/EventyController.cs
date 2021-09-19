
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

//Add the below
using EventyServerBL.Models;
using System.IO;

namespace EventyServer.Controllers
{
    [Route("EventyAPI")]
    [ApiController]
    public class EventyController : ControllerBase
    {
        #region Add connection to the db context using dependency injection
        EventyDBContext context;
        public EventyController(EventyDBContext context)
        {
            this.context = context;
        }
        #endregion

        [Route("HelloWorld")]
        [HttpGet]
        public string HelloWorld()
        {
            return "Hello World";
        }
    } 
}
