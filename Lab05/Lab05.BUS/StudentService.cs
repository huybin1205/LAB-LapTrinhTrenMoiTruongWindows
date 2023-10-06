using Lab05.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab05.BUS
{
    public class StudentService
    {
        public List<Student> GetAll()
        {
            StudentModel context = new StudentModel();
            return context.Students.ToList();
        }

        public List<Student> GetAllHasNoMajor()
        {
            StudentModel context = new StudentModel();
            return context.Students.Where(x => x.MajorID == null).ToList();
        }

        public List<Student> GetAllHasNoMajor(int facultyID)
        {
            StudentModel context = new StudentModel();
            return context.Students.Where(x => x.MajorID == null && x.FacultyID == facultyID).ToList();
        }

        public Student FindByID(string studentID)
        {
            StudentModel context = new StudentModel();
            return context.Students.FirstOrDefault(x => x.StudentID == studentID);
        }

        public void InsertUpdate(Student s)
        {
            StudentModel context = new StudentModel();
            var student = context.Students.SingleOrDefault(x => x.StudentID == s.StudentID);
            if (student == null)
            {
                context.Students.Add(s);
            }
            else
            {
                student.FullName = s.FullName;
                student.AverageScore = s.AverageScore;
                student.FacultyID = s.FacultyID;
                student.MajorID = s.MajorID;
                student.Avatar = string.IsNullOrEmpty(s.Avatar) ? student.Avatar : s.Avatar;
            }
            context.SaveChanges();
        }

        public void Delete(string studentID)
        {
            StudentModel context = new StudentModel();
            var student = context.Students.FirstOrDefault(x=>x.StudentID == studentID);
            
            if (student == null)
                throw new Exception("Không tìm thấy sinh viên");

            context.Students.Remove(student);
            context.SaveChanges();
        }
    }
}
