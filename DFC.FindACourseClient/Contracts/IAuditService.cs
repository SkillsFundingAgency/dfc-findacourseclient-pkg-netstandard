using System;
using System.Threading.Tasks;

namespace DFC.FindACourseClient
{
    public interface IAuditService
    {
        //Fire and forget audit
        void CreateAudit(object request, object response, Guid? correlationId = null);
    }
}