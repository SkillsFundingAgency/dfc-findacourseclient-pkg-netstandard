using System.Collections.Generic;

namespace DFC.FindACourseClientV2.Models.APIResponses.CourseGet
{
    public class ContactPersonalDetails
    {
        public IList<string> PersonNameTitle { get; set; }

        public IList<string> PersonGivenName { get; set; }

        public string PersonFamilyName { get; set; }

        public object PersonNameSuffix { get; set; }

        public string PersonRequestedName { get; set; }
    }
}