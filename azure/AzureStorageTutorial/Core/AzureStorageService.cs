using System;
using System.Collections.Generic;
using System.IO;
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

            _blobContainer = _blobClient.GetContainerReference("photoblobs");

            while (await _blobContainer.ExistsAsync())
            {
                Thread.Sleep(300);
            }

            _created = await _blobContainer.CreateIfNotExistsAsync();
        }

        public async Task Delete()
        {
            var blobContainer = _blobClient.GetContainerReference("photoblobs");

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
