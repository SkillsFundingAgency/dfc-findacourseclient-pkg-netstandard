using System;
using System.Threading.Tasks;

namespace DFC.FindACourseClient
{
    public interface IAuditService
    {
        Task CreateAudit(object request, object response, Guid? correlationId = null);
    }
}