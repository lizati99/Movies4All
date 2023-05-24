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
        Tuple<int, string, List<Image>> SaveImage(int movieId, List<IFormFile> imageFile);
        Tuple<int, string, Image> SaveImage(int movieId, IFormFile imageFile);
        bool DeleteImage(string imageFileName);
        Tuple<bool,string> DeleteAllImage(IEnumerable<Image> imagesFile);
    }
}
