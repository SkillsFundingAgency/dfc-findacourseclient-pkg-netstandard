using DFC.FindACourseClient.Contracts.CosmosDb;
using DFC.FindACourseClient.Models.Configuration;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;

namespace DFC.FindACourseClient.Repositories
{
    [ExcludeFromCodeCoverage]
    public class CosmosRepository<T> : ICosmosRepository<T>
    where T : IDataModel
    {
        private readonly CourseSearchAuditCosmosDbSettings cosmosDbConnection;
        private readonly IDocumentClient documentClient;

        public CosmosRepository(CourseSearchAuditCosmosDbSettings cosmosDbConnection, IDocumentClient documentClient)
        {
            this.cosmosDbConnection = cosmosDbConnection;
            this.documentClient = documentClient;
        }

        private Uri DocumentCollectionUri => UriFactory.CreateDocumentCollectionUri(cosmosDbConnection.DatabaseId, cosmosDbConnection.CollectionId);

        public async Task<HttpStatusCode> UpsertAsync(T model)
        {
            var accessCondition = new AccessCondition { Condition = model.Etag, Type = AccessConditionType.IfMatch };
            var partitionKey = new PartitionKey(model.PartitionKey);
            var result = await documentClient.UpsertDocumentAsync(DocumentCollectionUri, model, new RequestOptions { AccessCondition = accessCondition, PartitionKey = partitionKey }).ConfigureAwait(false);

            return result.StatusCode;
        }

        public async Task InitialiseDatabaseAsync(bool? isDevelopment)
        {
            if (isDevelopment.GetValueOrDefault())
            {
                await CreateDatabaseIfNotExistsAsync();
                await CreateCollectionIfNotExistsAsync();
            }
        }

        private async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                await documentClient.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(cosmosDbConnection.DatabaseId)).ConfigureAwait(false);
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    await documentClient.CreateDatabaseAsync(new Database { Id = cosmosDbConnection.DatabaseId }).ConfigureAwait(false);
                }
                else
                {
                    throw;
                }
            }
        }

        private async Task CreateCollectionIfNotExistsAsync()
        {
            try
            {
                await documentClient.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(cosmosDbConnection.DatabaseId, cosmosDbConnection.CollectionId)).ConfigureAwait(false);
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    var pkDef = new PartitionKeyDefinition
                    {
                        Paths = new Collection<string>() { cosmosDbConnection.PartitionKey },
                    };

                    await documentClient.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(cosmosDbConnection.DatabaseId),
                        new DocumentCollection { Id = cosmosDbConnection.CollectionId, PartitionKey = pkDef },
                        new RequestOptions { OfferThroughput = 1000 }).ConfigureAwait(false);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}