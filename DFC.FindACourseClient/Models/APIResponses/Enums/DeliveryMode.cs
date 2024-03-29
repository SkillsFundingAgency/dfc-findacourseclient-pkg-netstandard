﻿using System.ComponentModel;

namespace DFC.FindACourseClient
{
    public enum DeliveryMode
    {
        [Description("Undefined")]
        Undefined = 0,

        [Description("Classroom based")]
        ClassroomBased = 1,

        [Description("Online")]
        Online = 2,

        [Description("Work based")]
        WorkBased = 3,

        [Description("Blended learning")]
        BlendedLearning = 4,
    }
}