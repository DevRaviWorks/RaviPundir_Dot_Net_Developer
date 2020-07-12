using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DuckTaleInterviewAPI.DataAccessLayer.Model
{
    public class StudentModel
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<MarksModel> Marks { get; set; }
    }
    public class MarksModel
    {
        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        public int Marks { get; set; }

    }
}