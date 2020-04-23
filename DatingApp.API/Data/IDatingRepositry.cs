using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Helper;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    public interface IDatingRepositry
    {
        void Add<T> (T entity) where T:class;
        void Delete<T> (T entity) where T:class;
        Task<bool> SaveAll();
         Task<PagedList<User>> GetUsers (UserParams userParams);
        //Task<IEnumerable<User>> GetUsers ();

        Task<User> GetUser (int id);
        Task<Photo> GetImage (int id);

        Task<Photo> GetMainPhotoForUser (int id);
        Task<Like> GetLike (int userId,int recipientId);
    
    }
}