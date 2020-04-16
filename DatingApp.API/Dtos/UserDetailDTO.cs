using System;
using System.Collections.Generic;
using DatingApp.API.Models;

namespace DatingApp.API.Dtos
{
    public class UserDetailDTO
    {
        public int  id { get; set; }
        public string username { get; set; }
        public string gender { get; set; }
        public int age { get; set; }
        public string nickName { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime lastActive { get; set; }
        public string introduction { get; set; }
        public string lookingFor { get; set; }
        public string interests { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string  photoUrl { get; set; }
        public ICollection<PhotoDetailDTO> Photos { get; set; }

    }
}