using DFC.FindACourseClientV2.Contracts.CosmosDb;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace DFC.FindACourseClientV2.Models.CosmosDb
{
    public class ApiAuditRecordCourse : IDataModel
    {
        [Required]
        [JsonProperty(PropertyName = "id")]
        public Guid DocumentId { get; set; }

        [JsonProperty(PropertyName = "_etag")]
        public string Etag { get; set; }

        public Guid CorrelationId { get; set; }

        public object Request { get; set; }

        public object Response { get; set; }

        public string PartitionKey => $"{AuditDateTime.Year}{AuditDateTime.Month}";

        private static DateTime AuditDateTime => DateTime.UtcNow;
    }
}