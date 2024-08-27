

namespace Task1_Core.SaveImage
{
    public class SaveImage
    {
        public static void SaveImage1(IFormFile image)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            var filePath = Path.Combine(uploadsFolder, image.FileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyToAsync(fileStream);
            }
        }
    }
}
