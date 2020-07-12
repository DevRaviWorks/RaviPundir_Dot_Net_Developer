using DuckTaleInterviewAPI.DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckTaleInterviewAPI.DataAccessLayer
{
    public class DBLayer
    {
        DuckTaleInterviewDBEntities _db = new DuckTaleInterviewDBEntities();
        private IQueryable<Student> GetStudent()
        {
            return _db.Students.Where(x => x.Deleted == false);
        }
        public List<StudentModel> GetList()
        {
            IQueryable<Student> student = GetStudent();
            List<StudentModel> st = new List<StudentModel>();
            foreach (Student std in student)
            {
                StudentModel sm = new StudentModel()
                {
                    FirstName = std.FirstName,
                    LastName = std.LastName,
                    StudentID = std.StudentID,
                    Marks = GetMarks(std)
                };
                st.Add(sm);
            }

            return st;
        }
        private List<MarksModel> GetMarks(Student Student)
        {
            List<MarksModel> mrk = new List<MarksModel>();
            foreach (var marks in Student.StudentMarks)
            {
                MarksModel marksModel = new MarksModel()
                {
                    Marks = marks.Marks,
                    SubjectID = marks.SubjectID,
                    SubjectName = marks.Subject.Name
                };
                mrk.Add(marksModel);
            }
            return mrk;
        }

    }
}
