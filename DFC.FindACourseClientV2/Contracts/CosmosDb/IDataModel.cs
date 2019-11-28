﻿using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace DFC.FindACourseClientV2.Contracts.CosmosDb
{
    public interface IDataModel
    {
        [Required]
        [JsonProperty(PropertyName = "id")]
        Guid DocumentId { get; set; }

        [JsonProperty(PropertyName = "_etag")]
        string Etag { get; set; }

        [Required]
        string PartitionKey { get; }
    }
}
