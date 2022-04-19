
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Services {
    public interface IPictureService {
        public string SavePicture (IFormFile file, string path);
        public string[] SaveAndResizePicture (IFormFile file, int width, int height, string path);
    }
}
