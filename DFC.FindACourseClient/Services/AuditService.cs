﻿using Microsoft.Extensions.Logging;
using System;

namespace DFC.FindACourseClient
{
    internal class AuditService : IAuditService
    {
        private readonly ICosmosRepository<ApiAuditRecordCourse> auditRepository;
        private readonly ILogger<IAuditService> logger;

        public AuditService(ICosmosRepository<ApiAuditRecordCourse> auditRepository, ILogger<IAuditService> logger)
        {
            this.auditRepository = auditRepository;
            this.logger = logger;
        }

        public void CreateAudit(object request, object response, Guid? correlationId = null)
        {
            var auditRecord = new ApiAuditRecordCourse
            {
                DocumentId = Guid.NewGuid(),
                CorrelationId = correlationId ?? Guid.NewGuid(),
                Request = request,
                Response = response,
            };

            TaskHelper.ExecuteNoWait(() => auditRepository.UpsertAsync(auditRecord).ConfigureAwait(false), ex => logger.LogError(ex, $"Failed to create audit message"));
        }
    }
}