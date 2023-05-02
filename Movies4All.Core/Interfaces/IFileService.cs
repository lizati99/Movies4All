using Microsoft.AspNetCore.Http;
using Movies4All.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies4All.Core.Interfaces
{
    public interface IFileService
    {
        public Tuple<int, string, List<Image>> SaveImage(int movieId, List<IFormFile> imageFile);
        public bool DeleteImage(string imageFileName);
    }
}
