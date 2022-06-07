using EventyServerBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EventyServerBL.Models
{
    partial class EventyDBContext
    {
        // receives an object of type Account and adds it to the DB. Returns the Account object.
        public User Register(User a)
        {
            try
            {
                this.Users.Add(a);
                this.SaveChanges();

                return a;
            }
            catch
            {
                return null;
            }
        }

        // receives the needed info to create an account, creates the object and registers it. Returns the Account object.
        public Place UploadPlace(PlaceObj placeObj)
        {
            try
            {
                Place p = placeObj.placeObj;
                this.Places.Add(p);
                this.SaveChanges();

                if(placeObj.apartmentObj != null)
                {
                    Apartment a = placeObj.apartmentObj;
                    a.PlaceId = p.Id;
                    this.Apartments.Add(a);
                    this.SaveChanges();
                }
                else if (placeObj.privateHouseObj != null)
                {
                    PrivateHouse ph = placeObj.privateHouseObj;
                    ph.PlaceId = p.Id;
                    this.PrivateHouses.Add(ph);
                    this.SaveChanges();
                }
                else if (placeObj.hallObj != null)
                {
                    Hall hall = placeObj.hallObj;
                    hall.PlaceId = p.Id;
                    this.Halls.Add(hall);
                    this.SaveChanges();
                }
                else
                {
                    HouseBackyard hb = placeObj.houseBackyardObj;
                    hb.PlaceId = p.Id;
                    this.HouseBackyards.Add(hb);
                    this.SaveChanges();
                }

                return p;
            }
            catch
            {
                return null;
            }
        }

        public User Register(string firstName, string lastName, DateTime birthDate, string email, string phoneNumber, string password)
        {
            User a = new User()
            {
                FirstName = firstName,
                LastName = lastName,
                BirthDate = birthDate,
                Email = email,
                PhoneNumber = phoneNumber,
                Pass = password,
                IsAdmin = false,
                ProfileImage = ""
            };

            return Register(a);
        }

        // returns the accounts if correct credentials, else returns null
        public User Login(string email, string password) => this.Users.Include(l => l.LikedPlaces).FirstOrDefault(a => a.Email == email && a.Pass == password);

        // returns true if email exists otherwise returns false
        public bool EmailExists(string email) => this.Users.Any(a => a.Email == email);

        public bool CheckPassword(string password, string email)
        {
            User u = this.Users.FirstOrDefault(u => u.Email == email);
            if (u != null)
                return u.Pass == password;

            return false;
        }

        public bool UpdateUserPfp(string path, int id)
        {
            User account = this.Users.FirstOrDefault(u => u.Id == id);
            if (account != null)
            {
                account.ProfileImage = path;
                this.SaveChanges();
                return true;
            }

            return false;
        }

        public List<Place> GetPlaces() => this.Places.ToList();

        public List<Place> GetPlacesByCity(string city) => this.Places.Where(s => s.City.ToLower() == city).Include(s => s.PlaceTypeNavigation).Include(s => s.Orders).ToList();
        public Place GetPlacesById(int placeId) => this.Places.Include(s => s.PlaceTypeNavigation).FirstOrDefault(s => s.Id == placeId);
        public bool AddLikedPlace(int userID, int placeId)
        {
            User user = this.Users.FirstOrDefault(u => u.Id == userID);
            if (user != null)
            {
                user.LikedPlaces.Add(new LikedPlace()
                {
                    UserId = userID,
                    PlaceId = placeId
                });
                this.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RemoveLikedPlace(int userID, int placeId)
        {
            User user = this.Users.Include(l => l.LikedPlaces).FirstOrDefault(u => u.Id == userID);
            if (user != null)
            {
                LikedPlace likedPlace = user.LikedPlaces.FirstOrDefault(p => p.PlaceId == placeId);

                if (likedPlace != null)
                {
                    user.LikedPlaces.Remove(likedPlace);
                    this.SaveChanges();
                    return true;
                }

                return false;
            }
            else
            {
                return false;
            }
        }

        public List<Place> GetLikedPlaces(int userID)
        {
            User user = this.Users.Include(l => l.LikedPlaces).FirstOrDefault(u => u.Id == userID);
            if (user != null)
            {
                List<LikedPlace> like = user.LikedPlaces.ToList();
                List<Place> likedPlaces = new List<Place>();

                foreach (LikedPlace liked in like)
                {
                    likedPlaces.Add(this.Places.Include(s => s.PlaceTypeNavigation).FirstOrDefault(p => p.Id == liked.PlaceId));
                }             

                if (likedPlaces != null)
                { 
                    return likedPlaces;
                }

                return null;
            }
            else
            {
                return null;
            }
        }

        public Apartment GetApartmentList(int placeId) => this.Apartments.Where(a => a.PlaceId == placeId).FirstOrDefault();
        public PrivateHouse GetPrivateHouseList(int placeId) => this.PrivateHouses.Where(p => p.PlaceId == placeId).FirstOrDefault();
        public HouseBackyard GetHouseBackyardList(int placeId) => this.HouseBackyards.Where(h => h.PlaceId == placeId).FirstOrDefault();
        public Hall GetHallList(int placeId) => this.Halls.Where(h => h.PlaceId == placeId).FirstOrDefault();
        public Order MakeOrder(Order o)
        {
            try
            {
                this.Orders.Add(o);
                this.SaveChanges();

                return o;
            }
            catch
            {
                return null;
            }
        }
        public List<Order> GetOrders(int userId) => this.Orders.Where(o => o.UserId == userId).Include(s => s.Place).ToList();
        public List<Place> GetEstates(int userId) => this.Places.Where(p => p.OwnerId == userId).Include(s => s.PlaceTypeNavigation).ToList();
    }
}
