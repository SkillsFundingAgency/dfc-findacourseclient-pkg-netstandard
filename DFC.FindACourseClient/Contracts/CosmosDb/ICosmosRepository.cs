using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("DFC.FindACourseClient.UnitTests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace DFC.FindACourseClient
{
    internal interface ICosmosRepository<T>
        where T : IDataModel
    {
        Task<HttpStatusCode> UpsertAsync(T model);

        Task InitialiseDatabaseAsync(bool? isDevelopment);
    }
}