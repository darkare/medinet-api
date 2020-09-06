using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedinetAPI.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public String FileName { get; set; }
        public int CreatedBy { get; set; }
        public String ContentType { get; set; }
        public byte[] Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
