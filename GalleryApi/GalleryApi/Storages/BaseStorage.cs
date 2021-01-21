using GalleryApi.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;

namespace GalleryApi.Storages
{
    public class BaseStorage : IBaseStorage
    {
        private readonly IConfiguration configuration;

        public BaseStorage(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<CloudBlobContainer> GetCloudBlobContainerAsync()
        {
            var GalleryAccountConnection = configuration.GetConnectionString("GalleryAccountConnection");
            var cloudStorageAccount = CloudStorageAccount.Parse(GalleryAccountConnection);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            var cloudBlobContainer = cloudBlobClient.GetContainerReference("images");
            await cloudBlobContainer.CreateIfNotExistsAsync(BlobContainerPublicAccessType.Blob, null, null);
            return cloudBlobContainer;

        }

        public async Task<CloudBlockBlob> GetCloudBlockBlob(string fileName)
        {
            var cloudBlobContainer = await GetCloudBlobContainerAsync();
            var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
            return cloudBlockBlob;

        }

       
    }
}
