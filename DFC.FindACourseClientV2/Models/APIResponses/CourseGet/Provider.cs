using System;
using System.Collections.Generic;
using System.Text;

namespace DFC.FindACourseClientV2.Models.APIResponses
{
    public class Provider
    {
        public string Id { get; set; }

        public string UnitedKingdomProviderReferenceNumber { get; set; }

        public string ProviderName { get; set; }

        public object CourseDirectoryName { get; set; }

        public string ProviderStatus { get; set; }

        public IList<ProviderContact> ProviderContact { get; set; }

        public DateTime? ProviderVerificationDate { get; set; }

        public bool ProviderVerificationDateSpecified { get; set; }

        public bool ExpiryDateSpecified { get; set; }

        public string ProviderAssociations { get; set; }

        //Ignore for now seems to be cyclic
        //public IList<ProviderAlias> ProviderAliases { get; set; }
        public IList<VerificationDetail> VerificationDetails { get; set; }

        public int Status { get; set; }

        public DateTime? DateDownloaded { get; set; }

        public DateTime? DateOnboarded { get; set; }

        public DateTime? DateUpdated { get; set; }

        public string UpdatedBy { get; set; }

        public object ProviderId { get; set; }

        public object UPIN { get; set; }

        public object TradingName { get; set; }

        public bool NationalApprenticeshipProvider { get; set; }

        public string MarketingInformation { get; set; }

        public string Alias { get; set; }

        public int ProviderType { get; set; }

        public string BulkUploadStatus { get; set; }
    }
}
