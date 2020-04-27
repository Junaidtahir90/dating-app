using System;

namespace DatingApp.API.Dtos
{
    public class MessageDTO
    {

        //public int id { get; set; }
        public int senderId { get; set; }
        public int recipientId { get; set; }
        public string content { get; set; }
        public DateTime messageSent { get; set; }
        public MessageDTO()
        {
            messageSent = DateTime.Now;
        }

    }
}