using MedinetAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedinetAPI.Data
{
    public class PhotoRespository : IPhotoRespository, IDisposable
    {

        private BaseDbContext _dbContext;
        private bool disposed = false;
        public PhotoRespository(BaseDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Object> GetPhotos()
        {
            return _dbContext.Photos.Select(f => new { f.Id, f.Description, f.FileName, f.Title }).ToList();
        }

        public Photo GetPhotoById(int photoId)
        {
            var retObj =  _dbContext.Photos.Where(f => f.Id == photoId).Select(
                f => new Photo { Id=f.Id, 
                                 Description= f.Description, 
                                 FileName = f.FileName, 
                                 Title = f.Title,
                                 CreatedBy = f.CreatedBy,
                                 ContentType = f.ContentType,
                                 CreatedDate = f.CreatedDate});
            if (retObj.Count() == 0)
            {
                return null ;
            }
            return retObj.First();
        }

        public Photo GetPhotoContentById(int photoId)
        {
            return _dbContext.Photos.Find(photoId);
        }

        public void AddPhoto(Photo photo)
        {
            _dbContext.Photos.Add(photo);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
