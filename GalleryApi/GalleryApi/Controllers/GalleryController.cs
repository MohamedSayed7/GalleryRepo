using System.Collections.Generic;
using System.Threading.Tasks;
using GalleryApi.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage.Blob;

namespace GalleryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        private readonly IGalleryStorage galleryStorage;

        public GalleryController(IGalleryStorage GalleryStorage)
        {
            galleryStorage = GalleryStorage;
        }
        /// <summary>
        /// Api Responsable for Fet All Images
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<CloudBlockBlob>> Get()
        {
            var cloudBlockBlob = await galleryStorage.GetAllImagesAsync();
            return cloudBlockBlob ;
        }

        /// <summary>
        /// Api Responsable for Upload Image 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> UploadImage( ViewModel.FileInfo file)
        {
            bool IsCreated = await galleryStorage.UploadImageAsync(file);
            if (IsCreated)
            {
                return Ok(IsCreated);
            }
            else return BadRequest(IsCreated);
           
            
        }

        /// <summary>
        /// Api Responsable for Delete Image 
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
           bool IsDeleted = await galleryStorage.DeleteImageAsync(id);
            if (IsDeleted)
            {
                return Ok();
            }
            else return BadRequest();
        }
        /// <summary>
        /// Api Responsable for Delete All Images 
        /// </summary>
        /// <returns></returns>
        [HttpDelete ]
        public async Task<ActionResult> DeleteAllImages()
        {
            bool IsDeleted = await galleryStorage.DeleteAllImagesAsync();
            if (IsDeleted)
            {
                return Ok();
            }
            else return BadRequest();
        }

    }
}
