using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedinetAPI.Data;
using MedinetAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedinetAPI.Controllers
{
    [Route("api/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {

        private IPhotoRespository _photoRepository;

        public PhotosController(BaseDbContext dbContext)
        {
            _photoRepository = new PhotoRespository(dbContext);
        }
        // GET: api/<PhotosController>
        [HttpGet]
        public IEnumerable<Object> Get()
        {
            return _photoRepository.GetPhotos();
        }

        // GET api/<PhotosController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var photo = _photoRepository.GetPhotoById(id);
            if (photo == null)
            {
                return NotFound("Invalid photo id :" + id);
            }
            return Ok(photo);
        }

        [HttpGet]
        [Route("content/{id}")]
        public IActionResult Content(int id)
        {
            var photo = _photoRepository.GetPhotoContentById(id);
            if (photo == null) {
                return NotFound("photo not found");
            }
            return File(photo.Content, photo.ContentType);
        }

        // POST api/<PhotosController>
        [HttpPost]
        public IActionResult Post([FromForm] FileUpload objFile)
        {
            var fileSize = objFile.files.Length;
            if (fileSize > 0)
            {
                try
                {
                   byte[] fileByte = new byte[objFile.files.Length];

                   var inputStream= objFile.files.OpenReadStream();
                   inputStream.Read(fileByte, 0 , (int)fileSize);
                    _photoRepository.AddPhoto(
                        new Models.Photo { 
                            Description = objFile.desc,
                            FileName = objFile.files.FileName,
                            Title = objFile.title,
                            ContentType = objFile.files.ContentType,
                            Content = fileByte,
                            CreatedBy = objFile.createdBy,
                            CreatedDate = DateTime.Now
                        }
                   );
                   _photoRepository.Save();

                   return Ok("Photo saved.");
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }
            }
            else
            {
                return BadRequest();
            }
        }

    
        public class FileUpload
        {
            public IFormFile files { get; set; }
            public String desc { get; set; }
            public String title { get; set; }
            public int createdBy { get; set; }
        }

    }
}
