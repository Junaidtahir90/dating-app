namespace DatingApp.API.Dtos
{
    public class UserDataForUpdateDTO
    {
        public string introduction { get; set; }
        public string lookingFor { get; set; }
        public string interests { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public bool isMain { get; set;}
      
    }
}