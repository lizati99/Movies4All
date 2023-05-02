using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Movies4All.Core.Interfaces;
using Movies4All.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies4All.Data.Repositories
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;

        public FileService(IWebHostEnvironment environment)
        {
            this._environment = environment;
        }
        /* --------- Multi Uploads --------- */
        public Tuple<int, string, List<Image>> SaveImage(int movieId, List<IFormFile> imageFiles)
        {
            try
            {
                string newFileName = "";
                var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };
                string msg = string.Format("Only {0} extensions are allowed", string.Join(",", allowedExtensions));
                var uploadedImages= new List<Image>();
                foreach (var imageFile in imageFiles)
                {
                    var uploadsFolder = Path.Combine(_environment.ContentRootPath, "Uploads");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var ext = Path.GetExtension(imageFile.FileName);
                    newFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                    if (!allowedExtensions.Contains(ext))
                        return Tuple.Create(0,msg,uploadedImages);//or new Tuple<int, string>(0,msg)

                    var fileWithPath = Path.Combine(uploadsFolder, newFileName);
                    var stream = new FileStream(fileWithPath, FileMode.Create);
                    imageFile.CopyTo(stream);
                    stream.Close();
                    var image= new Image
                    {
                        Name=newFileName,
                        MovieId=movieId
                    };
                    uploadedImages.Add(image);
                }

                return Tuple.Create(1,"Upload is success.",uploadedImages);
            }
            catch (Exception ex)
            {
                return Tuple.Create(0,"Error concurred.",new List<Image>());
            }
        }

        public bool DeleteImage(string imageFileName)
        {
            try
            {
                var uploadsFile = Path.Combine(_environment.WebRootPath, "Uploads\\",imageFileName);
                if (File.Exists(uploadsFile))
                {
                    File.Delete(uploadsFile);
                    return true;
                }
                return false;
            }catch(Exception ex) 
            {
                return false;
            }
        }

        /* --------- Single Uploads --------- */
        /*public Tuple<int, string> SaveImage(IFormFile imageFile)
        {
            try
            {
                string newFileName = null;
                if (imageFile != null)
                {
                    var uploadsFolder = Path.Combine(_environment.ContentRootPath, "Uploads");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var ext = Path.GetExtension(imageFile.Name);
                    newFileName = Guid.NewGuid().ToString() + "_" + imageFile.Name;
                    var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };
                    if (!allowedExtensions.Contains(ext))
                    {
                        string msg = string.Format("Only {0} extensions are allowed", string.Join(",", allowedExtensions));
                        return Tuple.Create(0, msg);//or new Tuple<int, string>(0,msg)
                    }
                    var fileWithPath = Path.Combine(uploadsFolder, newFileName);
                    var stream = new FileStream(fileWithPath, FileMode.Create);
                    imageFile.CopyTo(stream);
                    stream.Close();
                    return Tuple.Create(0, newFileName);
                }
                return Tuple.Create(0, newFileName);
            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(0, ex.Message);
            }
        }*/
    }
}
