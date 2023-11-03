using CloudinaryDotNet.Actions;

namespace KeyShop.Interfaces {
    public interface IPhotoService {
        Task<ImageUploadResult> AddPhotoAsyns(IFormFile file);
        Task<DeletionResult> DeletePhotoAsinc(string publicId);
    }
}
