using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PokemonTCGWishlist.Data;

namespace PokemonTCGWishlist.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the PokemonTCGWishlistUser class
    public class PokemonTCGWishlistUser : IdentityUser
    {
        private readonly PokemonTCGWishlistContext _context;
        public PokemonTCGWishlistUser(/*PokemonTCGWishlistContext context*/)
        {
            //_context = context;
        }
/*
        public PokemonTCGWishlistUser AddUser(PokemonTCGWishlistUser user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public PokemonTCGWishlistUser DeleteUser(int id)
        {
            PokemonTCGWishlistUser user = _context.Users.Find(id);
            if(user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            return user;
        }

        public PokemonTCGWishlistUser UpdateUser(PokemonTCGWishlistUser updatedUser)
        {
            var user = _context.Users.Attach(updatedUser);
            user.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return updatedUser;
        }

        public PokemonTCGWishlistUser GetUser(int id)
        {
            return _context.Users.Find(id);
        }

        public IEnumerable<PokemonTCGWishlistUser> GetAllUsers()
        {
            return _context.Users;
        }
*/



        //some list/object for collection
        //some list of cards watched 
    }
}
