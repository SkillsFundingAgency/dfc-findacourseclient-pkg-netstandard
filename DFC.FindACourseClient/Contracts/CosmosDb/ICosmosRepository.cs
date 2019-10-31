using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace DFC.FindACourseClient.Contracts.CosmosDb
{
    public interface ICosmosRepository<T>
        where T : IDataModel
    {
        Task<HttpStatusCode> UpsertAsync(T model);
    }
}
