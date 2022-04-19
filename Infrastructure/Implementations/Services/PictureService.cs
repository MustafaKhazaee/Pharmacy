
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.Drawing.Imaging;

namespace Infrastructure.Implementations.Services {
    public class PictureService : IPictureService {
        public string[] SaveAndResizePicture(IFormFile file, int width, int height, string path) {
            string [] paths = new string[2];
            if (file == null)
                return null;
            string rootPath = Path.GetFullPath("wwwroot");
            string filepathOrginial = Path.Combine(rootPath, $"images\\{path}\\original\\", file.FileName);
            string filepathSmall = Path.Combine(rootPath, $"images\\{path}\\small\\", file.FileName);

            using (Stream fileStream = new FileStream(filepathOrginial, FileMode.Create, FileAccess.Write)) {
                file.CopyTo(fileStream);
            }


            Image smallImage = new Bitmap(width, height);
            using (var g = Graphics.FromImage(smallImage)) {
                Image image = Image.FromStream(file.OpenReadStream(), true, true);
                g.DrawImage(image, 0, 0, width, height);
                smallImage.Save(filepathSmall);
            }
            smallImage.Dispose();

            paths[0] = filepathOrginial;
            paths[1] = filepathSmall;
            return paths;
        }

        public string SavePicture(IFormFile file, string path) {
            if (file == null)
                return null;
            string rootPath = Path.GetFullPath("wwwroot");
            string filepathOrginial = Path.Combine(rootPath, $"images\\{path}\\", file.FileName);
            using (Stream fileStream = new FileStream(filepathOrginial, FileMode.Create, FileAccess.Write)) {
                file.CopyTo(fileStream);
            }
            return filepathOrginial;
        }
    }
}
