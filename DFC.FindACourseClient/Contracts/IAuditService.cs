using System;
using System.Threading.Tasks;

namespace DFC.FindACourseClient.Contracts
{
    public interface IAuditService
    {
        Task CreateAudit(object request, object response, Guid? correlationId = null);
    }
}