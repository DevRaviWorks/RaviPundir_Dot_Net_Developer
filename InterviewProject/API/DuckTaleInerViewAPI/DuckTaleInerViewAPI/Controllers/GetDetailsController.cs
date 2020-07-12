using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DuckTaleInerViewAPI.Models;
using DuckTaleInerViewAPI.Services;
using DuckTaleInterviewAPI.DataAccessLayer;
using DuckTaleInterviewAPI.DataAccessLayer.Model;

namespace DuckTaleInerViewAPI.Controllers
{
    public class GetDetailsController : ApiController
    {
        StudentService studentService = new StudentService();
        public List<StudentModel> GetList()
        {
            return studentService.GetList();
        }

        public dynamic GetAllSubjects()
        {
            return studentService.GetAllSubjects();
        }
        [HttpPost]
        public StudentModel InsertStudent(StudentModel model)
        {
            return studentService.Insert(model);
        }

        [HttpPatch]
        public Student UpdateStudent(StudentModel model)
        {
            studentService.Update(model);
            return studentService.GetStudentByID(model.StudentID);
        }

        [HttpPost]
        public Subject InsertSubject(Subject subject)
        {
            return studentService.InsertSubject(subject);
        }

        [HttpDelete]
        public bool DeleteStudentSubject(int StudentID,int SubjectID)
        {
            return studentService.DeleteStudentSubject(StudentID, SubjectID);
        }
    }
}
