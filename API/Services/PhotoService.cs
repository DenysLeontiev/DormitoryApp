using API.Helpers;
using API.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace API.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;

        public PhotoService(IOptions<CloudinarySettings> options)
        {
            Account acc = new Account(options.Value.CloudName, options.Value.APIKey, options.Value.APISecret);

            _cloudinary = new Cloudinary(acc);
        }

        public async Task<ImageUploadResult> UploadPhotoAsync(IFormFile file)
        {
            var imageUploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),
                    Folder = "DormitoryAnnouncement",
                };

                imageUploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            return imageUploadResult;
        }

        public Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            throw new NotImplementedException();
        }
    }
}