using Movies4All.Core.Dto;
using Movies4All.Core.Models;

namespace Movies4All.Core.Interfaces
{
    public interface IImageService:IBaseRepository<Image>
    {
        IEnumerable<Image> GetAllImagesByMovie(int movieId);
        List<ImageDto> ConvertImageToByte(IEnumerable<Image> imagePath);
        ImageDto ConvertImageToByte(Image imagePath);
    }
}
