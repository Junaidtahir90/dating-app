using System;

namespace DatingApp.API.Dtos
{
    public class PhotoForReturnDTO
    {
        public int Id  { get; set; }
        public string url { get; set; }
        public string description { get; set; }
        public DateTime dateAdded { get; set; }
        public string publicId {get;set;}
      
    }
}