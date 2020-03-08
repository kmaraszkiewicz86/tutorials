using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AzureStorageTutorial.Core
{
    public class AzureStorageService
    {
        private IConfiguration _configuration;

        private CloudStorageAccount _storageAccount;
        private CloudBlobClient _blobClient;
        private CloudBlobContainer _blobContainer;
        private bool _created;
        private string _cloudBlobContainerName => "photoblobs";

        public bool IsBlobContainerCreated => _created;

        public AzureStorageService()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsetings.json").Build();
        }

        public void Open()
        {
            var connectionString = _configuration["StorageAccountConnectionString"];

            if (!CloudStorageAccount.TryParse(connectionString, out _storageAccount))
            {
                throw new Exception("Unable to patse connection string");
            }

            _blobClient = _storageAccount.CreateCloudBlobClient();
        }

        public async Task CreateBlobContainer()
        {
            if (_storageAccount == null)
                throw new Exception($"{nameof(_storageAccount)} is null");

            _blobContainer = _blobClient.GetContainerReference(_cloudBlobContainerName);

            _created = await _blobContainer.CreateIfNotExistsAsync();
        }

        public async Task<IEnumerable<ICloudBlob>> GetBlobs()
        {
            BlobContinuationToken continuationToken = null;
            BlobResultSegment resultSegment = null;
            IEnumerable<ICloudBlob> cloudBlobs = new List<ICloudBlob>();
            IEnumerable<ICloudBlob> blockCloudBlobs = new List<ICloudBlob>();

            do
            {
                resultSegment = await _blobContainer.ListBlobsSegmentedAsync(continuationToken);

                cloudBlobs = resultSegment.Results.OfType<ICloudBlob>();
                blockCloudBlobs = resultSegment.Results.OfType<CloudBlockBlob>();

                continuationToken = resultSegment.ContinuationToken;
            } while (continuationToken != null);

            return cloudBlobs;
        }

        public async Task Save(Stream fileStream, string name)
        {
            var blockBlob = _blobContainer.GetBlockBlobReference(name);
            await blockBlob.UploadFromStreamAsync(fileStream);
        }

        public async Task<Stream> Load(string name)
        {
            return await _blobContainer.GetBlockBlobReference(name).OpenReadAsync();
        }

        public async Task Delete()
        {
            var blobContainer = _blobClient.GetContainerReference(_cloudBlobContainerName);

            if (blobContainer != null)
            {
                await blobContainer.DeleteIfExistsAsync();

                while (await blobContainer.ExistsAsync())
                {
                    Thread.Sleep(300);
                }
            }
        }
    }
}
