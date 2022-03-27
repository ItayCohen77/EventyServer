using EventyServerBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public Place UploadPlace(Place p)
        {
            try
            {
                this.Places.Add(p);
                this.SaveChanges();

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
        public User Login(string email, string password) => this.Users.FirstOrDefault(a => a.Email == email && a.Pass == password);

        //log in using token
        public User Login(string token)
        {
            UserAuthToken u = this.UserAuthTokens.FirstOrDefault(a => a.AuthToken == token);
            if (u != null)
                return u.User;
            return null;
        }

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

        //returns true of token exists in the db, otherwise false
        public bool TokenExists(string token) => this.UserAuthTokens.Any(a => a.AuthToken == token);

        //adds token to db and returns true if it succeeded
        public bool AddToken(string token, int id)
        {
            try
            {
                this.UserAuthTokens.Add(new UserAuthToken()
                {
                    UserId = id,
                    AuthToken = token
                });
                this.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
