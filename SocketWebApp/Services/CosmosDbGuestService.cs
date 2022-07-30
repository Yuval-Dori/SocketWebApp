using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocketWebApp.Models;
using Microsoft.Azure.Cosmos;


namespace SocketWebApp
{
    public class CosmosDbGuestService : ICosmosDbGuestService
    {
        private Container _container;

        public CosmosDbGuestService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddGuestAsync(Models.Guest guest)
        {
            await this._container.CreateItemAsync<Models.Guest>(guest, new PartitionKey(guest.Id));
        }

    }
}

