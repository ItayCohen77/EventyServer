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

        [Route("login")]
        [HttpGet]
        public User Login([FromQuery] string email, [FromQuery] string pass)
        {           
            User u = context.Login(email, pass);

            if (u != null)
            {
                HttpContext.Session.SetObject("User", u);

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return u;
            }
            else
            {

                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

        //[Route("register")]
        //[HttpGet]
        //public User Register([FromQuery] string firstName, [FromQuery] string lastName, [FromQuery] string email, [FromQuery] DateTime dt, [FromQuery] string username, [FromQuery] string password)
        //{
        //    PlayerDTO pDto = HttpContext.Session.GetObject<PlayerDTO>("player");
        //    //Check if user logged in!
        //    if (pDto == null)
        //    {
        //        Player p = context.Register(firstName, lastName, email, dt, username, password);

        //        Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

        //        if (p != null)
        //            return new PlayerDTO(p);
        //        else
        //            return null;
        //    }
        //    else
        //    {
        //        Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
        //        return null;
        //    }
        //}
    } 
}
