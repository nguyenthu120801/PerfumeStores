namespace PerfumeStores.Services
{
    public class ImageHandle
    {
        public static readonly List<string> ImageExtensions = new List<string> { ".jpg", ".jpeg", ".jpe", ".bmp", ".gif", ".png" };

        public string? AddImage(IFormFile file, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            if (file != null)
            {
                foreach (var f in ImageExtensions)
                {
                    if (file.FileName.Contains(f))
                    {
                        var fileUp = Path.Combine(environment.WebRootPath, "img", file.FileName);
                        using (var fileStream = new FileStream(fileUp, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                            return $"/img/{file.FileName}";
                        }
                    }
                }
            }
            return "/img/default-image.jpg";
        }
    }
}
