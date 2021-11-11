using Azure.Storage.Blobs;
using CMPG_323_Project2.Models;
//using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CMPG_323_Project2.Logic
{
    public class FileManagerLogic: IFileManagerLogic
    {
        private readonly BlobServiceClient _blobServiceClient;
        public FileManagerLogic(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }
        public async Task Upload(FileModel model,int Id)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient("uploadimage");
            var blobClient = blobContainer.GetBlobClient(Id.ToString());
          
            await blobClient.UploadAsync(model.MyFile.OpenReadStream());
        }
        public string read(string filename)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient("uploadimage");
            var blobClient = blobContainer.GetBlobClient(filename);

            return blobClient.Uri.ToString();
        }

        public async Task<byte[]> GetData(string filename)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient("uploadimage");
            var blobClient = blobContainer.GetBlobClient(filename);

            var imageDownload = await blobClient.DownloadAsync();

            using(MemoryStream ms = new MemoryStream())
            {
                await imageDownload.Value.Content.CopyToAsync(ms);
                return ms.ToArray();
            }
        }
        public async Task Delete(string filename)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient("uploadimage");
            var blobClient = blobContainer.GetBlobClient(filename);

            await blobClient.DeleteIfExistsAsync();
        }
    }
}
