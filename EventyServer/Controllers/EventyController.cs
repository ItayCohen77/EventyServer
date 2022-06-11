using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
        public User SignUp([FromBody] User sentUser)
        {
            User current = HttpContext.Session.GetObject<User>("user");
            // Check if user isn't logged in!
            if (current == null)
            {
                User u = new User()
                {
                    Email = sentUser.Email,
                    Pass = sentUser.Pass,
                    FirstName = sentUser.FirstName,
                    LastName = sentUser.LastName,
                    BirthDate = sentUser.BirthDate,
                    IsAdmin = sentUser.IsAdmin,
                    PhoneNumber = sentUser.PhoneNumber,
                    ProfileImage = sentUser.ProfileImage
                };

                try
                {
                    bool exists = context.EmailExists(u.Email);
                    if (!exists)
                    {
                        User a = context.Register(u);
                        HttpContext.Session.SetObject("user", u);
                        return a;
                    }
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Conflict;
                    return null;
                }
                catch
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Conflict;
                    return null;
                }  
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

        [Route("login")]
        [HttpGet]
        public User Login([FromQuery] string email, [FromQuery] string password)
        {
            User user = null;

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

        [Route("email-exists")]
        [HttpGet]
        public bool? EmailExists([FromQuery] string email)
        {
            try
            {
                bool exists = context.EmailExists(email);
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return exists;
            }
            catch
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return null;
            }
        }

        [Route("logout")]
        [HttpGet]
        public IActionResult LogOut()
        {
            User user = HttpContext.Session.GetObject<User>("user");
            if (user != null)
            {
                HttpContext.Session.Clear();
                return Ok();
            }
            else
            {
                return Forbid();
            }
        }

        [Route("uploadimage")]
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            User user = HttpContext.Session.GetObject<User>("user");

            if (user != null)
            {
                if (file == null)
                {
                    return BadRequest();
                }

                try
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imgs", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    if (file.FileName.StartsWith("a"))
                        context.UpdateUserPfp(file.FileName, user.Id);

                    return Ok(new { length = file.Length, name = file.FileName });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return BadRequest();
                }
            }
            return Forbid();
        }

        [Route("hostplace")]
        [HttpPost]
        public Place HostPlace([FromBody] PlaceObj sentPlaceObj)
        {

            User current = HttpContext.Session.GetObject<User>("user");
            if (current != null)
            {
                PlaceObj placeObj = new PlaceObj()
                {
                    placeObj = sentPlaceObj.placeObj,
                    apartmentObj = sentPlaceObj.apartmentObj,
                    privateHouseObj = sentPlaceObj.privateHouseObj,
                    hallObj = sentPlaceObj.hallObj,
                    houseBackyardObj = sentPlaceObj.houseBackyardObj,
                };               

                try
                {
                    Place p = context.UploadPlace(placeObj);
                    if (p != null)
                    {
                        Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                        return p;
                    }
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Conflict;
                    return null;
                }
                catch
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Conflict;
                }
                return null;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

        [Route("getplaces")]
        [HttpGet]
        public string GetAllPlaces() 
        {
            try
            {
                List<Place> places = context.GetPlaces();

                JsonSerializerSettings options = new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.All
                };

                string json = JsonConvert.SerializeObject(places, options);

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return json;
            }
            catch
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return null;
            }
        }

        [Route("getplacesbycity")]
        [HttpGet]
        public string GetPlacesByCity([FromQuery] string city)
        {
            try
            {
                List<Place> places = context.GetPlacesByCity(city);

                JsonSerializerSettings options = new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.All
                };

                string json = JsonConvert.SerializeObject(places, options);

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return json;
            }
            catch
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return null;
            }
        }

        [Route("getplacebyid")]
        [HttpGet]
        public string GetPlaceById([FromQuery] int placeId)
        {
            try
            {
                Place place = context.GetPlacesById(placeId);

                JsonSerializerSettings options = new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.All
                };

                string json = JsonConvert.SerializeObject(place, options);

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return json;
            }
            catch
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return null;
            }
        }

        [Route("addlikedplace")]
        [HttpGet]
        public bool AddLikedPlace([FromQuery] int placeID)
        {
            User current = HttpContext.Session.GetObject<User>("user");
            if (current != null)
            {
                try
                {
                    bool added = context.AddLikedPlace(current.Id, placeID);
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    return added;
                }
                catch
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                    return false;
                }
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return false;
            }
        }

        [Route("removelikedplace")]
        [HttpGet]
        public bool RemoveLikedPlace([FromQuery] int placeID)
        {
            User current = HttpContext.Session.GetObject<User>("user");
            if (current != null)
            {
                try
                {
                    bool added = context.RemoveLikedPlace(current.Id, placeID);
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    return added;
                }
                catch
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                    return false;
                }
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return false;
            }
        }

        [Route("getlikedplaces")]
        [HttpGet]
        public string GetLikedPlaces([FromQuery] int userID)
        {
            try
            {
                List<Place> places = context.GetLikedPlaces(userID);

                JsonSerializerSettings options = new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.All
                };

                string json = JsonConvert.SerializeObject(places, options);

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return json;
            }
            catch
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return null;
            }
        }

        [Route("getfeatureslist")]
        [HttpGet]
        public List<Feature> GetFeaturesList([FromQuery] int placeId, [FromQuery] string placeT)
        {                    
            try
            {
                string placeType = placeT;
                List<Feature> features = new List<Feature>();
                Apartment a = new Apartment();
                PrivateHouse ph = new PrivateHouse();
                HouseBackyard hb = new HouseBackyard();
                Hall h = new Hall();

                switch(placeType)
                {
                    case "Apartment":
                        a = context.GetApartmentList(placeId);
                        break;
                    case "PrivateHouse":
                        ph = context.GetPrivateHouseList(placeId);
                        break;
                    case "HouseBackyard":
                        hb = context.GetHouseBackyardList(placeId);
                        break;
                    case "Hall":
                        h = context.GetHallList(placeId);
                        break;
                }

                if(a.PlaceId != 0)
                {
                    if(a.HasCoffeeMachine)
                    {
                        features.Add(new Feature
                        {
                            FeatureType = "Coffee Machine"
                        });
                    }
                    if(a.HasTv)
                    {
                        features.Add(new Feature
                        {
                            FeatureType = "TV"
                        });
                    }
                    if(a.HasWaterHeater)
                    {
                        features.Add(new Feature
                        {
                            FeatureType = "Water Heater"
                        });
                    }
                    if(a.HasAirConditioner)
                    {
                        features.Add(new Feature
                        {
                            FeatureType = "Air Conditioner"
                        });
                    }
                    if(a.HasSpeakerAndMic)
                    {
                        features.Add(new Feature
                        {
                            FeatureType = "Speaker And Mic"
                        });
                    }
                }
                else if(ph.PlaceId != 0)
                {
                    if (ph.HasCoffeeMachine)
                    {
                        features.Add(new Feature
                        {
                            FeatureType = "Coffee Machine"
                        });
                    }
                    if (ph.HasTv)
                    {
                        features.Add(new Feature
                        {
                            FeatureType = "TV"
                        });
                    }
                    if (ph.HasWaterHeater)
                    {
                        features.Add(new Feature
                        {
                            FeatureType = "Water Heater"
                        });
                    }
                    if (ph.HasAirConditioner)
                    {
                        features.Add(new Feature
                        {
                            FeatureType = "Air Conditioner"
                        });
                    }
                    if (ph.HasSpeakerAndMic)
                    {
                        features.Add(new Feature
                        {
                            FeatureType = "Speaker And Mic"
                        });
                    }
                }
                else if(hb.PlaceId != 0)
                {
                    if (hb.HasBbq)
                    {
                        features.Add(new Feature
                        {
                            FeatureType = "BBQ"
                        });
                    }
                    if (hb.HasHotub)
                    {
                        features.Add(new Feature
                        {
                            FeatureType = "Hot Tub"
                        });
                    }
                    if (hb.HasTables)
                    {
                        features.Add(new Feature
                        {
                            FeatureType = "Tables"
                        });
                    }
                    if (hb.HasPool)
                    {
                        features.Add(new Feature
                        {
                            FeatureType = "Pool"
                        });
                    }
                    if (hb.HasChairs)
                    {
                        features.Add(new Feature
                        {
                            FeatureType = "Chairs"
                        });
                    }
                }
                else
                {
                    if (h.HasBar)
                    {
                        features.Add(new Feature
                        {
                            FeatureType = "Bar"
                        });
                    }
                    if (h.HasProjector)
                    {
                        features.Add(new Feature
                        {
                            FeatureType = "Projector"
                        });
                    }
                    if (h.HasTables)
                    {
                        features.Add(new Feature
                        {
                            FeatureType = "Tables"
                        });
                    }
                    if (h.HasSpeakerAndMic)
                    {
                        features.Add(new Feature
                        {
                            FeatureType = "Speaker And Mic"
                        });
                    }
                    if (h.HasChairs)
                    {
                        features.Add(new Feature
                        {
                            FeatureType = "Chairs"
                        });
                    }
                }

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return features; 
            }
            catch
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Conflict;
                return null;
            }                     
        }


        [Route("makeorder")]
        [HttpPost]
        public Order MakeOrder([FromBody] Order order)
        {

            User current = HttpContext.Session.GetObject<User>("user");
            if (current != null)
            {
                Order newOrder = new Order()
                {
                    UserId = order.UserId,
                    PlaceId = order.PlaceId,
                    Price = order.Price,
                    Total = order.Total,
                    EventDate = order.EventDate,
                    AmountOfPeople = order.AmountOfPeople,
                    StartTime = order.StartTime,
                    EndTime = order.EndTime,
                    TotalHours = order.TotalHours
                };

                try
                {
                    Order o = context.MakeOrder(newOrder);
                    if (o != null)
                    {
                        Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                        return o;
                    }
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Conflict;
                    return null;
                }
                catch
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Conflict;
                }
                return null;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }
        [Route("getorders")]
        [HttpGet]
        public string GetOrders([FromQuery] int userID)
        {
            try
            {
                List<Order> orders = context.GetOrders(userID);

                JsonSerializerSettings options = new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.All
                };

                string json = JsonConvert.SerializeObject(orders, options);

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return json;
            }
            catch
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return null;
            }
        }

        [Route("updateprofileinfo")]
        [HttpGet]
        public IActionResult UpdateProfile([FromQuery] string firstName, [FromQuery] string lastName, [FromQuery] string phoneNum, [FromQuery] string password)
        {
            User loggedInUser = HttpContext.Session.GetObject<User>("user");

            if (loggedInUser != null)
            {
                User user = context.Users.FirstOrDefault(u => u.Id == loggedInUser.Id);
                if (user == null) return Forbid();

                if (firstName != null) user.FirstName = firstName;
                if (lastName != null) user.LastName = lastName;
                if (phoneNum != null) user.PhoneNumber = phoneNum;
                if (password != null) user.Pass = password;
                context.SaveChanges();

                return Ok();
            }

            return Forbid();
        }

        [Route("getestates")]
        [HttpGet]
        public string GetEstates([FromQuery] int userID)
        {
            try
            {
                List<Place> places = context.GetEstates(userID);

                JsonSerializerSettings options = new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.All
                };

                string json = JsonConvert.SerializeObject(places, options);

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return json;
            }
            catch
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return null;
            }
        }

        [Route("updateplaceinfo")]
        [HttpGet]
        public IActionResult UpdatePlace([FromQuery] int totalOccupancy, [FromQuery] string summary, [FromQuery] string placeAddress, [FromQuery] string apartment, [FromQuery] string city, [FromQuery] string zip, [FromQuery] string country, [FromQuery] int price, [FromQuery] int placeId)
        {
            User loggedInUser = HttpContext.Session.GetObject<User>("user");

            if (loggedInUser != null)
            {
                Place place = context.Places.FirstOrDefault(p => p.Id == placeId);
                if (place == null) return Forbid();

                if (totalOccupancy != null) place.TotalOccupancy = totalOccupancy;
                if (summary != null) place.Summary = summary;
                if (placeAddress != null) place.PlaceAddress = placeAddress;
                if (apartment != null) place.Apartment = apartment;
                if (city != null) place.City = city;
                if (zip != null) place.Zip = zip;
                if (country != null) place.Country = country;
                if (price != null) place.Price = price;
                context.SaveChanges();

                return Ok();
            }

            return Forbid();
        }

        [Route("deleteplace")]
        [HttpGet]
        public IActionResult DeletePlace([FromQuery] int placeId)
        {
            User loggedInUser = HttpContext.Session.GetObject<User>("user");

            if (loggedInUser != null)
            {
                Place place = context.Places.Include(p => p.Orders).FirstOrDefault(p => p.Id == placeId);              
                if(place.Orders.Count == 0)
                {
                    context.Remove(place);
                    switch (place.PlaceType)
                    {
                        case 1:
                            Apartment a = context.Apartments.FirstOrDefault(p => p.Id == placeId);
                            context.Apartments.Remove(a); break;
                        case 2:
                            Hall h = context.Halls.FirstOrDefault(p => p.Id == placeId);
                            context.Halls.Remove(h); break;
                        case 3:
                            PrivateHouse p = context.PrivateHouses.FirstOrDefault(p => p.Id == placeId);
                            context.PrivateHouses.Remove(p); break;
                        case 4:
                            HouseBackyard b = context.HouseBackyards.FirstOrDefault(p => p.Id == placeId);
                            context.HouseBackyards.Remove(b); break;
                    }
                    context.SaveChanges();

                    return Ok();
                }              
            }

            return Forbid();
        }

        [Route("cancelevent")]
        [HttpGet]
        public IActionResult CancelEvent([FromQuery] int orderId)
        {
            User loggedInUser = HttpContext.Session.GetObject<User>("user");

            if (loggedInUser != null)
            {
                Order order = context.Orders.FirstOrDefault(o => o.Id == orderId);
                
                context.Remove(order);
                context.SaveChanges();

                return Ok();
            }

            return Forbid();
        }
    }
}
