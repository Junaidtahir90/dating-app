using System;
using System.Collections.Generic;

namespace DatingApp.API.Models
{
    public class User
    {
        public int  id { get; set; }
        public string username { get; set; }
        public byte[] passwordHash{get;set;}
        public byte[] passwordSalt { get; set; }
        public string gender { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string nickName { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime lastActive { get; set; }
        public string introduction { get; set; }
        public string lookingFor { get; set; }
        public string interests { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public ICollection<Photo> Photos { get; set; }
         public ICollection<Like> Likers { get; set; }
        public ICollection<Like> Likees { get; set; }
        public ICollection<Message> MessageSent { get; set; }
        public ICollection<Message> MessageReceived { get; set; }

        internal object where(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
    }
}