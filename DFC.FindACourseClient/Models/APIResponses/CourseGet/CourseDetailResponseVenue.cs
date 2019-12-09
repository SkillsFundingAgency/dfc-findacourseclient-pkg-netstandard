namespace DFC.FindACourseClient.Models.APIResponses.CourseGet
{
    public class CourseDetailResponseVenue
    {
        public string VenueName { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string Town { get; set; }

        public string County { get; set; }

        public string Postcode { get; set; }

        public string Telephone { get; set; }

        public string Email { get; set; }

        public string Website { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }
    }
}