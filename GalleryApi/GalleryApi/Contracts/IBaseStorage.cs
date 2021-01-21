using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;

namespace GalleryApi.Contracts
{
   public interface IBaseStorage
    {
        Task<CloudBlobContainer> GetCloudBlobContainerAsync();
        Task<CloudBlockBlob> GetCloudBlockBlob(string fileName);
    }
}
