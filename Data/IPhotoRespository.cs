using MedinetAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedinetAPI.Data
{
    interface IPhotoRespository : IDisposable
    {
        IEnumerable<Object> GetPhotos();
        Photo GetPhotoById(int photoId);
        Photo GetPhotoContentById(int photoId);
        void AddPhoto(Photo photo);
        void Save();
    }
}
