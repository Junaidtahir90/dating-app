using System.Linq;
using AutoMapper;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using DatingApp.API.Helper;

namespace DatingApp.API.Helper
{
    public class AutoMapperPofiles: Profile
    {
        
        public AutoMapperPofiles()
        {
            CreateMap<User, UserListDTO>().
            ForMember(dest=> dest.photoUrl,opt =>{
                opt.MapFrom(src=> src.Photos.FirstOrDefault(p => p.isMain).url);
            }).
            ForMember(dest => dest.age, opt => {
                 opt.MapFrom(d => d.dateOfBirth.CalculateAge());
                    });
            CreateMap<User, UserDetailDTO>().
            ForMember(dest=> dest.photoUrl,opt =>{
                opt.MapFrom(src=> src.Photos.FirstOrDefault(p => p.isMain).url);
            }).
            ForMember(dest => dest.age, opt => {
                 opt.MapFrom(d => d.dateOfBirth.CalculateAge());
                    });
            CreateMap<Photo, PhotoDetailDTO>();
            CreateMap<UserDataForUpdateDTO,User>();
            CreateMap<PhotoMediaDTO, Photo>();
            CreateMap<Photo, PhotoForReturnDTO>();
            CreateMap<UserDTO,User>();
            CreateMap<MessageDTO,Message>().ReverseMap();
            CreateMap<Message,MessageDetailDTO>()
            .ForMember(m => m.senderPhotoUrl,opt => opt
            .MapFrom(u => u.sender.Photos.FirstOrDefault(p => p.isMain).url))
            .ForMember(m => m.recipientPhotoUrl ,opt => opt
            .MapFrom(u => u.recipient.Photos.FirstOrDefault(p => p.isMain).url));
        }
    }
}