using Microsoft.Azure.Cosmos;

namespace ToDoItems.Dal
{
    public static class CosmosDbServiceProvider
    {
        private const string DatabaseName = "Items";
		private const string TodoContainerName = "Todo";
        private const string PersonContainerName = "Person";
        private const string Account = "https://pppktodoitemspeople.documents.azure.com:443/";
		private const string Key = "d3qiOM4n1WOYduy2cifFRxtDIgdyr8DGn9keqqXxvyyUXSsgVQDzlN2OhyKgb8JDnGwp4LWUfmZPACDbeUlF6A==";


        private static ICosmosDbService? service;
        private static ICosmosDbPeopleService? peopleService;
        public static ICosmosDbService? Service { get => service; }
        public static ICosmosDbPeopleService? PeopleService { get => peopleService; }

        public async static Task Init() // EAGER!!!
        {
            CosmosClient cosmosClient = new(Account, Key);
            service = new CosmosDbService(cosmosClient, DatabaseName, TodoContainerName);
            peopleService = new CosmosDbPeopleService(cosmosClient, DatabaseName, PersonContainerName);
            DatabaseResponse databaseResponse = await cosmosClient.CreateDatabaseIfNotExistsAsync(DatabaseName);
            await databaseResponse.Database.CreateContainerIfNotExistsAsync(TodoContainerName, "/id");
            await databaseResponse.Database.CreateContainerIfNotExistsAsync(PersonContainerName, "/id");
        }
    }
}
