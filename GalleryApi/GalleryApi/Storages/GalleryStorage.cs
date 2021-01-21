using GalleryApi.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalleryApi.Storages
{
    public class GalleryStorage : BaseStorage, IGalleryStorage
    {

        public GalleryStorage(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<bool> UploadImageAsync(ViewModel.FileInfo file)
        {
            bool IsCreated = true;
            try
            {
                var cloudBlockBlob = await GetCloudBlockBlob(file.FileName);
                var isExist = await cloudBlockBlob.ExistsAsync();
                if (!isExist)
                {
                    var t = file.FileByteArray;
                    byte[] imageBytes = Convert.FromBase64String(t);
                    await cloudBlockBlob.UploadFromByteArrayAsync(imageBytes, 0, imageBytes.Length);

                }
                else IsCreated = false;
            }
            catch
            {
                throw;

            }
            return IsCreated;

        }

        public async Task<bool> DeleteImageAsync(string id)
        {
            bool isDeleted = true;
            try
            {
                var cloudBlockBlob = await GetCloudBlockBlob(id);

                isDeleted = await cloudBlockBlob.DeleteIfExistsAsync();
               

            }
            catch
            {

                throw;
            }
            return isDeleted;
        }

        public async Task<IEnumerable<CloudBlockBlob>> GetAllImagesAsync()
        {
            var cloudBlockBlobs = new List<CloudBlockBlob>();
            var cloudBlobContainer = await GetCloudBlobContainerAsync();
            var blobResultSegment = await cloudBlobContainer.ListBlobsSegmentedAsync(null);
            cloudBlockBlobs.AddRange(blobResultSegment.Results.OfType<CloudBlockBlob>());
            return cloudBlockBlobs;


        }

        public async Task<bool> DeleteAllImagesAsync()
        {
            bool isDeleted = true;

            try
            {
                var cloudBlobContainer = await GetCloudBlobContainerAsync();
                var blobResultSegment = await cloudBlobContainer.ListBlobsSegmentedAsync(null);
                var result = blobResultSegment.Results.OfType<CloudBlockBlob>();
                foreach (var blob in result)
                {
                    isDeleted = await blob.DeleteIfExistsAsync();
                }
            }
            catch 
            {
                throw;
            }

            return isDeleted;


        }
    }
}
