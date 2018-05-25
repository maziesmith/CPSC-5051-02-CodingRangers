using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TreeAttendance.Models;

namespace TreeAttendance.Backend
{
    /// <summary>
    /// The interface for the Student DataSource.
    /// </summary>
    public interface IStudentInterface
    {
        StudentModel Create(StudentModel data);
        StudentModel Read(string id);
        StudentModel Update(StudentModel data);
        bool Delete(string id);
        List<StudentModel> Index();
        void Reset();
    }
}