namespace CoreMVCWebApp.Models
{
    public class Student
    {

        public int StudentId { get; set; }

        public string StudentName { get; set; }
        public DateTime? DateofBirth { get; set; }
        public DateTime? EnrollmentDate { get; set; }


        public int GradeId { get; set; }
        public virtual Grade grade { get; set; }

    }
}
