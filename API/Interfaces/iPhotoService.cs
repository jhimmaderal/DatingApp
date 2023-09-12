using CloudinaryDotNet.Actions;

namespace API.Interfaces
{
    public interface iPhotoService
    {
        Task<ImageUploadResult>AddPhotoAsync(IFormFile file);
        Task<DeletionResult>DeletePhotoAsync(string publicId);
    }
}