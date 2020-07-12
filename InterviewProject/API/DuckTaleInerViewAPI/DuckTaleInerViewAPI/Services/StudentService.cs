using DuckTaleInerViewAPI.Models;
using DuckTaleInterviewAPI.DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DuckTaleInerViewAPI.Services
{
    public class StudentService
    {

        private IQueryable<Student> GetStudent()
        {
            DuckTaleInterviewDBEntities _db = new DuckTaleInterviewDBEntities();
            return _db.Students.Where(x => x.Deleted == false);
        }
        public Student GetStudentByID(int StudentID)
        {
            DuckTaleInterviewDBEntities _db = new DuckTaleInterviewDBEntities();
            return _db.Students.Where(x => x.Deleted == false).Where(x=>x.StudentID==StudentID).FirstOrDefault();
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
        private List<MarksModel> GetMarks(Student StudentID)
        {
            List<MarksModel> mrk = new List<MarksModel>();
            foreach (var marks in StudentID.StudentMarks.Where(x=>x.Deleted==false))
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

        public StudentModel Insert(StudentModel Model)
        {
            DuckTaleInterviewDBEntities  _db = new DuckTaleInterviewDBEntities();
            Student student;
            using (_db)
            {
                student = new Student()
                {
                    FirstName = Model.FirstName,
                    LastName = Model.LastName,
                    Deleted = false
                };
                _db.Students.Add(student);
                _db.SaveChanges();
            }
            StudentModel sm = new StudentModel()
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                StudentID = student.StudentID,
                Marks = PrepareMarksList(Model.Marks, student.StudentID)
            };
           
            return sm;

        }
        private List<MarksModel> PrepareMarksList(List<MarksModel> marks, int StudentID)
        {
            DuckTaleInterviewDBEntities _db = new DuckTaleInterviewDBEntities();
            List<StudentMark> stdmarks;
            using (_db)
            {
                foreach (MarksModel model in marks)
                {
                    StudentMark studentMark = new StudentMark()
                    {
                        Marks = model.Marks,
                        StudentID = StudentID,
                        SubjectID = model.SubjectID,
                        Deleted=false
                    };
                    _db.StudentMarks.Add(studentMark);
                    _db.SaveChanges();
                }
                 stdmarks = _db.StudentMarks.Where(x => x.StudentID == StudentID).Where(x=>x.Deleted==false).ToList();
            }
            
            List<MarksModel> mModel = new List<MarksModel>();
            foreach (var m in stdmarks) {
                MarksModel marksModel= new MarksModel()
                {
                    Marks = m.Marks,
                    SubjectID = m.SubjectID,
                    SubjectName = GetSubjectName(m.SubjectID)
                };
                mModel.Add(marksModel);
            }
            return mModel;
        }
        private string GetSubjectName(int subjectID)
        {
            DuckTaleInterviewDBEntities _db = new DuckTaleInterviewDBEntities();
            using (_db)
            {
                return _db.Subjects.Where(x => x.SubjectID == subjectID).Select(x => x.Name).FirstOrDefault().ToString();
            }
        }

        public void Update(StudentModel Model)
        {
            DuckTaleInterviewDBEntities _db = new DuckTaleInterviewDBEntities();
            Student student = _db.Students.Where(x => x.Deleted == false).Where(x => x.StudentID == Model.StudentID).FirstOrDefault();
            if (student != null)
            {
                student.FirstName = Model.FirstName;
                student.LastName = Model.LastName;
                _db.SaveChanges();
            } 
            foreach(MarksModel marks in Model.Marks)
            {
                StudentMark studentMark = _db.StudentMarks.Where(x => x.StudentID == Model.StudentID).Where(x => x.SubjectID == marks.SubjectID).FirstOrDefault();
                if (studentMark != null)
                {
                    studentMark.Marks = marks.Marks;
                    _db.SaveChanges();
                }
                else
                {
                   
                    StudentMark sm = new StudentMark()
                    {
                        Marks = marks.Marks,
                        StudentID = Model.StudentID,
                        SubjectID = marks.SubjectID
                    };
                    _db.StudentMarks.Add(sm);
                    _db.SaveChanges();
                }
            }
            

        }

        public bool DeleteStudent(int StudentID)
        {
            try
            {
                DuckTaleInterviewDBEntities _db = new DuckTaleInterviewDBEntities();
                Student student = _db.Students.Where(x => x.StudentID == StudentID).FirstOrDefault();
                if (student != null)
                {
                    student.Deleted = true;
                    _db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteSubject(int SubjectID)
        {
            try
            {
                DuckTaleInterviewDBEntities _db = new DuckTaleInterviewDBEntities();
                Subject subject = _db.Subjects.Where(x => x.SubjectID == SubjectID).FirstOrDefault();
                if (subject != null)
                {
                    subject.Deleted = true;
                    _db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public Subject InsertSubject(Subject subject)
        {
            DuckTaleInterviewDBEntities _db = new DuckTaleInterviewDBEntities();
            Subject sub = new Subject()
            {
                Name = subject.Name,
                Deleted = false
            };
            _db.Subjects.Add(sub);
            _db.SaveChanges();
            return sub;
        }

        public bool DeleteStudentSubject(int StudentID, int SubjectID)
        {
            try
            {
                DuckTaleInterviewDBEntities _db = new DuckTaleInterviewDBEntities();
                StudentMark marks = _db.StudentMarks.Where(x => x.StudentID == StudentID).Where(x => x.SubjectID == SubjectID).FirstOrDefault();
                if (marks != null)
                {
                    marks.Deleted = true;
                    _db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
            
           
        }

        public dynamic GetAllSubjects()
        {
            DuckTaleInterviewDBEntities _db = new DuckTaleInterviewDBEntities();
            //return 
            return _db.Subjects.Where(x => x.Deleted == false).Select(x=> new { x.Name,x.SubjectID});
             
        }
    }
}