using System;
using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos

{
    public class UserDTO
    {

        #region Validation Notes
        // Validation in DTO class using by   namespace as System.ComponentModel.DataAnnotations; 
        //and [ApiController] in contoller for validations by DTO vaidation
        #endregion

        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength (8,MinimumLength=4, ErrorMessage="Please specify password between 4 to 8 characters")]
        public string Password { get; set; }
        
        [Required]
        public string Gender { get; set; }

        [Required]
        public string NickName { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastActive { get; set; }

        public UserDTO()
        {
            CreatedDate= DateTime.Now;
            LastActive= DateTime.Now;
        }
    }
}