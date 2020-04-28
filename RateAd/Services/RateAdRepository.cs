using Microsoft.AspNetCore.Mvc;
using RateAd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateAd.Services
{
    public class RateAdRepository : IRateAdRepository,IDisposable
    {
        private readonly RateAdContext _context;

        public RateAdRepository(RateAdContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #region GET methods
        public User GetUser(long userId)
        {
            return _context.Users.FirstOrDefault(us => us.Id == userId);
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList();
        }
        #endregion

        #region POST methods
        public void CreateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.PasswordSalt = "testSalt";
            user.IsBlocked = false;
            user.IsDeleted = false;
            user.RegistrationToken = Guid.NewGuid().ToString();
            user.PasswordRecoveryToken = Guid.NewGuid().ToString();
            //user.Id = _context.Users.Max(x => x.Id) + 1;
            _context.Users.Add(user);

        }
        #endregion
        public bool UserExist(long userId)
        {
            return _context.Users.Any(us => us.Id == userId);
        }
        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose resources when needed
            }
        }
    }
}
