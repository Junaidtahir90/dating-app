using System;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class AuthRepositry : IAuthRepositry
     {
        private readonly DataContext _context;
        public AuthRepositry(DataContext context)
        {
           _context=context; 
        }
        public async Task<User> Login(string username, string password)
        {
           var user = await _context.Users.FirstOrDefaultAsync(us=> us.username.ToLower()==username.ToLower());
           if(user == null){
               return null;
           }
           
           //var user = await _context.Users.FirstOrDefaultAsync(us=> us.username.ToLower()==username.ToLower());
           if(!VerifyPasswordHash(password, user.passwordHash, user.passwordSalt) ){
               return null;
           }
           return user;
           // throw new System.NotImplementedException();
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
             using (var hmac=new System.Security.Cryptography.HMACSHA512(passwordSalt)){
                   var computedHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                   for(int i=0;i<computedHash.Length; i++ ){
                       if(computedHash[i]!= passwordHash[i]) 
                                return false;
                          /*  {
                                return false;
                            }   
                       else {
                                return true;
                            }*/
                   }
            }
            return true;
            //throw new NotImplementedException();
        }

        public async Task<User> Register(User user, string password)
        {
            byte [] _passwordHash,_passwordSalt;
            CreatePasswordHash(password,  out _passwordHash, out _passwordSalt); // out keyword why?

            user.passwordHash=_passwordHash;
            user.passwordSalt=_passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
            //throw new System.NotImplementedException();
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac=new System.Security.Cryptography.HMACSHA512()){
                    passwordSalt=hmac.Key;
                    passwordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            //throw new NotImplementedException();
        }

        public async Task<bool> UserExits(string username)
        {
            if (await _context.Users.AnyAsync(us => us.username == username))
            {
                return true;
            }
            else
            {   
                return false;
            }

            //throw new System.NotImplementedException();
        }
    }
}