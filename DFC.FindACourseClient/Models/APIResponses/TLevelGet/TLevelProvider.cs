using System;
using System.Collections.Generic;
using System.Text;

namespace DFC.FindACourseClient
{
    public class TLevelProvider
    {
        public string ProviderName { get; set; }

        public string UKPRN { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string Town { get; set; }

        public string Postcode { get; set; }

        public string County { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }

        public string Fax { get; set; }

        public string Website { get; set; }

        public decimal? LearnerSatisfaction { get; set; }

        public decimal? EmployerSatisfaction { get; set; }
    }
}
