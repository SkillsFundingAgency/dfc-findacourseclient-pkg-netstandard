using System.Collections.Generic;
using System.Linq;
using Comp = DFC.CompositeInterfaceModels.FindACourseClient;

namespace DFC.FindACourseClient
{
    internal static class EducationLevelExtensions
    {
        internal static List<EducationLevel> MapToEducationLevels(this EducationLevel educationLevel)
        {
            var result = new List<EducationLevel>();
            switch (educationLevel)
            {
                case EducationLevel.EntryLevel:
                    result.Add(EducationLevel.EntryLevel);
                    break;

                case EducationLevel.One:
                    result.Add(EducationLevel.One);
                    break;

                case EducationLevel.Two:
                    result.Add(EducationLevel.Two);
                    break;

                case EducationLevel.Three:
                    result.Add(EducationLevel.Three);
                    break;

                case EducationLevel.Four:
                    result.Add(EducationLevel.Four);
                    break;

                case EducationLevel.Five:
                    result.Add(EducationLevel.Five);
                    break;

                case EducationLevel.Six:
                    result.Add(EducationLevel.Six);
                    break;

                case EducationLevel.Seven:
                    result.Add(EducationLevel.Seven);
                    break;

                case EducationLevel.All:
                default:
                    result.Add(EducationLevel.EntryLevel);
                    result.Add(EducationLevel.One);
                    result.Add(EducationLevel.Two);
                    result.Add(EducationLevel.Three);
                    result.Add(EducationLevel.Four);
                    result.Add(EducationLevel.Five);
                    result.Add(EducationLevel.Six);
                    result.Add(EducationLevel.Seven);
                    break;
            }

            return result.Distinct().ToList();
        }
    }
}