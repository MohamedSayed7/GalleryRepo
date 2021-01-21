using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GalleryApi.Contracts
{
    public interface IGalleryStorage
    {
        Task<bool> UploadImageAsync(ViewModel.FileInfo file);
        Task<IEnumerable<CloudBlockBlob>> GetAllImagesAsync();
        Task<bool> DeleteImageAsync(string id);
        Task<bool> DeleteAllImagesAsync();
    }
}
