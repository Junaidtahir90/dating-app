using System.Linq;
using AutoMapper;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using DatingApp.API.Helper;

namespace DatingApp.API.Helper
{
    public class AutoMapperPofilles: Profile
    {
        
        public AutoMapperPofilles()
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

        }
    }
}