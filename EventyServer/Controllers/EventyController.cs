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

        [Route("signup")]
        [HttpPost]
        public User SignUp([FromBody] User sentAccount)
        {
            User current = HttpContext.Session.GetObject<User>("user");
            // Check if user isn't logged in!
            if (current == null)
            {
                User acc = new User()
                {
                    Email = sentAccount.Email,
                    Pass = sentAccount.Pass,
                    FirstName = sentAccount.FirstName,
                    LastName = sentAccount.LastName,
                    BirthDate = sentAccount.BirthDate,
                    IsAdmin = sentAccount.IsAdmin,
                    PhoneNumber = sentAccount.PhoneNumber,
                    ProfileImage = sentAccount.ProfileImage
                };

                try
                {
                    bool exists = context.EmailExists(acc.Email);
                    if (!exists)
                    {
                        User a = context.Register(acc);
                        return a;
                    }
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Conflict;
                }
                catch
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Conflict;
                }

                if (acc != null)
                {
                    HttpContext.Session.SetObject("user", acc);
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                    return acc;
                }
                else
                    return null;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

        [Route("login")]
        [HttpPost]
        public User Login([FromBody] (string, string) credentials) // credentials is a tuple where item1 is the email and item2 is the password
        {
            User user = null;
            string email = credentials.Item1;
            string password = credentials.Item2;

            bool validPassword = context.CheckPassword(password, email);

            if (validPassword)
            {
                try
                {
                    user = context.Login(email, password);
                }
                catch
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                }

                // Check username and password
                if (user != null)
                {
                    HttpContext.Session.SetObject("user", user);
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                    return user;
                }
                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                    return null;
                }
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                return null;
            }
        }

        //[Route("login-token")]
        //[HttpPost]
        //public UserDTO LoginToken([FromQuery] string token)
        //{
        //    Account account = null;
        //    try
        //    {
        //        account = context.Login(token);
        //    }
        //    catch
        //    {
        //        Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
        //    }

        //    // Check username and password
        //    if (account != null)
        //    {
        //        AccountDTO aDTO = new AccountDTO(account);

        //        HttpContext.Session.SetObject("player", aDTO);
        //        Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

        //        return aDTO;
        //    }
        //    else
        //    {
        //        Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
        //        return null;
        //    }
        //}

        //[Route("generate-token")]
        //[HttpGet]
        //public string GenerateToken()
        //{
        //    try
        //    {
        //        AccountDTO current = HttpContext.Session.GetObject<AccountDTO>("account");
        //        if (current != null)
        //        {
        //            bool isUnique = false;
        //            string token = "";
        //            while (!isUnique)
        //            {
        //                token = GeneralProcessing.GenerateAlphanumerical(16);
        //                isUnique = context.TokenExists(token);
        //            }

        //            bool worked = context.AddToken(token, current.AccountId);
        //            if (worked)
        //            {
        //                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
        //                return token;
        //            }
        //            else
        //            {
        //                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
        //                return null;
        //            }

        //        }
        //        else
        //        {
        //            Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
        //            return null;
        //        }
        //    }
        //    catch
        //    {
        //        Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
        //        return null;
        //    }
        //}
    } 
}
