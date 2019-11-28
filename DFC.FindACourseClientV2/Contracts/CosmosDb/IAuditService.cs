using System.Threading.Tasks;

namespace DFC.FindACourseClientV2.Contracts.CosmosDb
{
    public interface IAuditService
    {
        Task AuditAsync(string request, string response);
    }
}
