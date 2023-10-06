using Lab05.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Lab05.BUS
{
    public class FacultyService
    {
        public List<Faculty> GetAll()
        {
            StudentModel context = new StudentModel();
            return context.Faculties.ToList();
        }

        public Faculty FindByFacultyName(string facultyName)
        {
            StudentModel context = new StudentModel();
            return context.Faculties.FirstOrDefault(x => x.FacultyName == facultyName);
        }
    }
}
