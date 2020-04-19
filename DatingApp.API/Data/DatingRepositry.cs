using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DatingRepositry : IDatingRepositry
    {
        private readonly DataContext _context;
        public DatingRepositry(DataContext context)
        {
            _context=context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Photo> GetImage(int id)
        {
            var p= await _context.Photos.FirstOrDefaultAsync(_ph =>_ph.Id==id);
            return p;
            //throw new System.NotImplementedException();
        }

        public async Task<User> GetUser(int id)
        {
            var user= await _context.Users.Include(p=>p.Photos).FirstOrDefaultAsync(u=> u.id ==id);
            
            return user;
        }

     
        public async Task<IEnumerable<User>> GetUsers()
        {
            var allUsers= await _context.Users.Include(p=>p.Photos).ToListAsync();
            
            return allUsers;
        }

        public async Task<bool> SaveAll()
        {
           return await _context.SaveChangesAsync() >0;
        }
      
    }
}