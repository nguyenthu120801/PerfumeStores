namespace PerfumeStores.Core.Services
{
    public interface IImageHandle
    {
        Task<string?> AddImage(IFormFile file, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment);
    }
}
