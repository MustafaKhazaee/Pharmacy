
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Services {
    public interface IPictureService {
        public string SavePicture (IFormFile file);
        public string[] SaveAndResizePicture (IFormFile file, int width, int height);
    }
}
