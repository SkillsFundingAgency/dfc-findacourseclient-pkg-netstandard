﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DFC.FindACourseClientV2.Models.APIRequests
{
    public class CourseSearchRequest
    {
        public string SubjectKeyword { get; set; }

        public string DfE1619Funded { get; set; }

        public double Distance { get; set; }

        public List<int> QualificationLevels { get; set; }

        public List<int> StudyModes { get; set; }

        public List<int> AttendanceModes { get; set; }

        public List<int> AttendancePatterns { get; set; }

        public string Town { get; set; }

        public string Postcode { get; set; }

        public int SortBy { get; set; }

        public string StartDateFrom { get; set; }

        public string StartDateTo { get; set; }

        public int TopResults { get; set; }

        public int PageNo { get; set; }
    }

}
