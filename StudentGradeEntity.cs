using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wirstADO.net
{
    public class StudentGradeEntity
    {
        public string Name { set; get; }
        public string Group { set; get; }
        public int AvgGrade { set; get; }
        public string MinGradeLesson { set; get; }
        public int MinGrade { set; get; }
        public string MaxGradeLesson { set; get; }
        public int MaxGrade { set; get; }
        public StudentGradeEntity(string name, string group, int avg, string minGradel, int minGrade, string maxGradel, int maxGrade) 
        {
            Name = name;
            Group = group;
            AvgGrade = avg;
            MinGradeLesson = minGradel;
            MinGrade = minGrade;
            MaxGradeLesson = maxGradel;
            MaxGrade = maxGrade;
        }
    }
}
