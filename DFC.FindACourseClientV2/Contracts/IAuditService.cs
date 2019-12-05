using System;
using System.Threading.Tasks;

namespace DFC.FindACourseClientV2.Contracts
{
    public interface IAuditService
    {
        Task CreateAudit(object request, object response, Guid? correlationId = null);
    }
}