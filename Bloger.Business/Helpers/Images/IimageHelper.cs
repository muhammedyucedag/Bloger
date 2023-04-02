using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloger.Entity.Concrete;
using Microsoft.AspNetCore.Http;

namespace Bloger.Business.Helpers.Images
{
    public interface IimageHelper
    {
        Task<ImageUploaded> Upload(string name, IFormFile imageFile, string folderName = null);
        Task Delete(string imageName);
    }
}
