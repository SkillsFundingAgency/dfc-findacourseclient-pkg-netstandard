using System.Collections.Generic;

namespace DFC.FindACourseClientV2.Models.APIResponses.CourseGet
{
    public class ContactAddress
    {
        public Saon Saon { get; set; }

        public Paon Paon { get; set; }

        public string StreetDescription { get; set; }

        public string UniqueStreetReferenceNumber { get; set; }

        public string Locality { get; set; }

        public IList<string> Items { get; set; }

        public IList<int> ItemsElementName { get; set; }

        public string PostTown { get; set; }

        public string PostCode { get; set; }

        public string UniquePropertyReferenceNumber { get; set; }
    }
}