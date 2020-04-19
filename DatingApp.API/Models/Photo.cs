using System;

namespace DatingApp.API.Models
{
    public class Photo
    {
        public int Id  { get; set; }
        public string url { get; set; }
        public string description { get; set; }
        public DateTime dateAdded { get; set; }
        public string publicId { get; set; }
        public bool isMain { get; set;}
        public User User { get; set; }
        public int UserId { get; set; }
    }
}