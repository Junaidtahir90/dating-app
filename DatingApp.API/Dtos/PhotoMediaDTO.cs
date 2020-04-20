using System;
using Microsoft.AspNetCore.Http;

namespace DatingApp.API.Dtos
{
    public class PhotoMediaDTO
    {
        public string url { get; set; }
        public IFormFile file { get; set; }
        public string publicId  { get; set; }
        public string description { get; set; }
        public DateTime dateAdded { get; set; }
        public bool isMain { get; set;}
        public PhotoMediaDTO()
        {
            dateAdded=DateTime.Now;
        }
    }
}