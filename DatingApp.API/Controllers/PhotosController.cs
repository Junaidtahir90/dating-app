using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Helper;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [Route("api/users/{userId}/photos")]
    [ApiController]

    public class PhotosController : ControllerBase
    {

        private readonly IDatingRepositry _repo;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig ;
        private Cloudinary _cloudinary;

        public PhotosController(IDatingRepositry repo, IMapper mapper,
        IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _repo = repo;
            _mapper = mapper;
            _cloudinaryConfig = cloudinaryConfig;
           
           Account acc= new Account(
               _cloudinaryConfig.Value.CloudName,
               _cloudinaryConfig.Value.ApiKey,
               _cloudinaryConfig.Value.ApiSecret
           );

           _cloudinary = new Cloudinary(acc);
        }

        [HttpGet("{id}",Name="GetImage")]
         public async Task<IActionResult> GetImage(int id){

                var photo = await _repo.GetImage(id);
                var photoForReturnDTO=_mapper.Map<PhotoForReturnDTO>(photo);
                
                return Ok(photoForReturnDTO);
         }

        [HttpPost]
        public async Task<IActionResult> UploadPhotos (int userId,[FromForm]PhotoMediaDTO _photoMediaDTO){

             if( userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)){
                return Unauthorized();
            }

            var userFromRepo=await _repo.GetUser(userId);

            var file= _photoMediaDTO.file; 
            var uploadResult= new ImageUploadResult();

            if(file.Length>0){

                using(var stream= file.OpenReadStream()){
                    var uploadParams= new ImageUploadParams(){
                        File= new FileDescription(file.Name,stream),
                        Transformation= new Transformation()
                                    .Width(500).Height(500).Crop("fill").Gravity("face")
                    };
                    uploadResult=_cloudinary.Upload(uploadParams);
                }
            }
                _photoMediaDTO.url=uploadResult.Uri.ToString();
                _photoMediaDTO.publicId=uploadResult.PublicId;

                var photo=_mapper.Map<Photo>(_photoMediaDTO);

                if(!userFromRepo.Photos.Any(ph =>ph.isMain)){
                    photo.isMain = true;
                }
                userFromRepo.Photos.Add(photo);

                if(await _repo.SaveAll()){
                    var photoForReturn=_mapper.Map<PhotoForReturnDTO>(photo);
                    return CreatedAtRoute("GetImage",new {id=photo.Id}, photoForReturn);
                    //return Ok("Image Uploaded");
                }
                else
                {
                return BadRequest("Issues in Image Uploading");
                    
                }
        }

     [HttpPost("{id}/setMain")]
     public async Task<IActionResult> SetMainPhoto (int userId,int id){

            if( userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)){
                return Unauthorized();
            }

            var user=await _repo.GetUser(userId);
            if(!user.Photos.Any(ph =>ph.Id==id))
             return Unauthorized();

             var photoFromRepo = await _repo.GetImage(id);
             if(photoFromRepo.isMain){
                 return BadRequest("This is already main photo");
             }

             var currentMainPhoto = await _repo.GetMainPhotoForUser(userId);

             currentMainPhoto.isMain =false;

             photoFromRepo.isMain =true;

             if(await _repo.SaveAll()){
                 return NoContent();
             }

             return BadRequest("Error in Setting main Photo");

     }   
    }
}