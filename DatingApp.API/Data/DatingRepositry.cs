using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Helper;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DatingRepositry : IDatingRepositry
    {
        private readonly DataContext _context;
        public DatingRepositry(DataContext context)
        {
            _context = context;
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
            var p = await _context.Photos.FirstOrDefaultAsync(_ph => _ph.Id == id);
            return p;
            //throw new System.NotImplementedException();
        }

        public async Task<Like> GetLike(int userId, int recipientId)
        {
            return await _context.Likes.FirstOrDefaultAsync
                                        (lik => lik.LikerId == userId && lik.LikeeId == recipientId);
            //throw new NotImplementedException();
        }

        public async Task<Photo> GetMainPhotoForUser(int userId)
        {
            return await _context.Photos.Where(u => u.UserId == userId).FirstOrDefaultAsync(ph => ph.isMain);
            // return mainPhotoForUser;
            //throw new System.NotImplementedException();
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.id == id);

            return user;
        }

        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            //var allUsers= await _context.Users.Include(p=>p.Photos).ToListAsync();
            var users = _context.Users.Include(p => p.Photos).OrderByDescending(us => us.lastActive).AsQueryable();
            users = users.Where(us => us.id != userParams.UserId);
            users = users.Where(us => us.gender == userParams.Gender);
            //users= users.Where(us => us.nickName.c == userParams.Gender);
            if (userParams.MinAge != 18 || userParams.MaxAge != 99)
            {
                var minDOB = DateTime.Today.AddYears(-userParams.MaxAge - 1);
                var maxDOB = DateTime.Today.AddYears(-userParams.MinAge);
                users = users.Where(us => us.dateOfBirth >= minDOB && us.dateOfBirth <= maxDOB);
            }

            if (userParams.Likers)
            {
                var userLikers = await GetUserLikes(userParams.UserId,userParams.Likers);
                users= users.Where(u => userLikers.Contains(u.id));
            }

            if (userParams.Likees)
            {
                var userLikees = await GetUserLikes(userParams.UserId,userParams.Likers);
                users= users.Where(u => userLikees.Contains(u.id));
            }

            if (!string.IsNullOrEmpty(userParams.OrderBy))
            {

                switch (userParams.OrderBy)
                {
                    case "created":
                        users = users.OrderByDescending(usr => usr.createdDate);
                        break;
                    default:
                        users = users.OrderByDescending(usr => usr.lastActive);
                        break;
                }
            }
            return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);// allUsers.P;
        }

        private async Task<IEnumerable<int>> GetUserLikes(int id, bool likers){

            var user = await _context.Users
                            .Include(x=> x.Likers)
                            .Include(x=>x.Likees)
                            .FirstOrDefaultAsync(u =>u.id==id);


           if(likers)
           {
               return user.Likers.Where(u => u.LikeeId==id).Select(i =>i.LikerId);
           }
           else{

               return user.Likees.Where(u => u.LikerId==id).Select(i =>i.LikeeId);
           }


        }   
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}