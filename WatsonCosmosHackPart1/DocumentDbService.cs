using System;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace WatsonCosmosHackPart1
{
    public static class DocumentDbService
    {
        static readonly DocumentClient documentClient = new DocumentClient(new Uri("https://lebronofazure.documents.azure.com:443/"), "oHSMja5vTrYvHqzsFka6ONApAC9U39rNsbgL68VMgOjOWLreDp32X2JwAo9d8DZY2msNxsiH9naskHh4PxqUdA==");

		static readonly string DatabaseId = "Xamarin";
        static readonly string CollectionId = "Dog";

        public static async Task<List<DogModel>> GetAllDogsAsync()
        {
            
            var docResults = await Task.Run(() => documentClient.CreateDocumentQuery<DogModel>(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId)).ToList());

            return docResults;
        }

        public static async Task<DogModel> GetDogByIdAsync(string id)
        {
            var result = await documentClient.ReadDocumentAsync<DogModel>(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id));

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
                return null;

            return result;
        }

        public static async Task PostDogAsync(DogModel dog){
            await documentClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), dog);

            return;
        }

        public static async Task DeleteDogAsync(DogModel dog){
            await documentClient.DeleteDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, dog.Id));
        }
    }
}
