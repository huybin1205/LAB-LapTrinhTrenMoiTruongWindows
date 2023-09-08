using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01_03
{
    class Program
    {
        static List<Student> students = new List<Student>();
        static List<Teacher> teachers = new List<Teacher>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Chương trình quản lý sinh viên và giáo viên");
                Console.WriteLine("1 - Thêm sinh viên");
                Console.WriteLine("2 - Thêm giáo viên");
                Console.WriteLine("3 - Xuất danh sách sinh viên");
                Console.WriteLine("4 - Xuất danh sách giáo viên");
                Console.WriteLine("5 - Số lượng từng danh sách (tổng số sinh viên, tổng số giáo viên)");
                Console.WriteLine("6 - Xuất danh sách các Sinh Viên thuộc khoa 'CNTT'");
                Console.WriteLine("7 - Xuất ra danh sách giáo viên có địa chỉ chứa thông tin 'Quận 9'");
                Console.WriteLine("8 - Xuất ra danh sách sinh viên có điểm trung bình cao nhất và thuộc khoa 'CNTT'");
                Console.WriteLine("9 - Hãy cho biết số lượng của từng xếp loại trong danh sách");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Lựa chọn không hợp lệ. Hãy chọn lại.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddStudent();
                        break;
                    case 2:
                        AddTeacher();
                        break;
                    case 3:
                        DisplayStudents();
                        break;
                    case 4:
                        DisplayTeachers();
                        break;
                    case 5:
                        DisplayCounts();
                        break;
                    case 6:
                        DisplayCNTTStudents();
                        break;
                    case 7:
                        DisplayTeachersInDistrict9();
                        break;
                    case 8:
                        DisplayHighestGPAStudentsInCNTT();
                        break;
                    case 9:
                        DisplayGPAStatistics();
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ. Hãy chọn lại.");
                        break;
                }
            }
        }

        static void AddStudent()
        {
            Console.WriteLine("Nhập thông tin sinh viên:");
            Student student = new Student();

            Console.Write("Mã số: ");
            student.StudentID = Console.ReadLine();

            Console.Write("Họ tên: ");
            student.FullName = Console.ReadLine();

            Console.Write("Khoa: ");
            student.Faculty = Console.ReadLine();

            Console.Write("Điểm trung bình: ");
            if (float.TryParse(Console.ReadLine(), out float gpa))
            {
                student.AverageScore = gpa;
            }
            else
            {
                Console.WriteLine("Điểm trung bình không hợp lệ. Thêm sinh viên không thành công.");
                return;
            }

            students.Add(student);
            Console.WriteLine("Thêm sinh viên thành công.");
        }

        static void AddTeacher()
        {
            Console.WriteLine("Nhập thông tin giáo viên:");
            Teacher teacher = new Teacher();

            Console.Write("Mã số: ");
            teacher.StudentID = Console.ReadLine();

            Console.Write("Họ tên: ");
            teacher.FullName = Console.ReadLine();

            Console.Write("Địa chỉ: ");
            teacher.Address = Console.ReadLine();

            teachers.Add(teacher);
            Console.WriteLine("Thêm giáo viên thành công.");
        }

        static void DisplayStudents()
        {
            Console.WriteLine("Danh sách sinh viên:");
            foreach (var student in students)
            {
                Console.WriteLine($"Mã số: {student.StudentID}");
                Console.WriteLine($"Họ tên: {student.FullName}");
                Console.WriteLine($"Khoa: {student.Faculty}");
                Console.WriteLine($"Điểm trung bình: {student.AverageScore}");
                Console.WriteLine();
            }
        }

        static void DisplayTeachers()
        {
            Console.WriteLine("Danh sách giáo viên:");
            foreach (var teacher in teachers)
            {
                Console.WriteLine($"Mã số: {teacher.StudentID}");
                Console.WriteLine($"Họ tên: {teacher.FullName}");
                Console.WriteLine($"Địa chỉ: {teacher.Address}");
                Console.WriteLine();
            }
        }

        static void DisplayCounts()
        {
            Console.WriteLine($"Tổng số sinh viên: {students.Count}");
            Console.WriteLine($"Tổng số giáo viên: {teachers.Count}");
        }

        static void DisplayCNTTStudents()
        {
            Console.WriteLine("Danh sách sinh viên thuộc khoa 'CNTT':");
            foreach (var student in students.Where(s => s.Faculty == "CNTT"))
            {
                Console.WriteLine($"Mã số: {student.StudentID}");
                Console.WriteLine($"Họ tên: {student.FullName}");
                Console.WriteLine($"Khoa: {student.Faculty}");
                Console.WriteLine($"Điểm trung bình: {student.AverageScore}");
                Console.WriteLine();
            }
        }

        static void DisplayTeachersInDistrict9()
        {
            Console.WriteLine("Danh sách giáo viên có địa chỉ chứa thông tin 'Quận 9':");
            foreach (var teacher in teachers.Where(t => t.Address.Contains("Quận 9")))
            {
                Console.WriteLine($"Mã số: {teacher.StudentID}");
                Console.WriteLine($"Họ tên: {teacher.FullName}");
                Console.WriteLine($"Địa chỉ: {teacher.Address}");
                Console.WriteLine();
            }
        }

        static void DisplayHighestGPAStudentsInCNTT()
        {
            var cnttStudents = students.Where(s => s.Faculty == "CNTT").ToList();
            if (cnttStudents.Count == 0)
            {
                Console.WriteLine("Không có sinh viên thuộc khoa 'CNTT'.");
                return;
            }

            var highestGPA = cnttStudents.Max(s => s.AverageScore);
            var highestGPAStudents = cnttStudents.Where(s => s.AverageScore == highestGPA).ToList();

            Console.WriteLine($"Danh sách sinh viên thuộc khoa 'CNTT' có điểm trung bình cao nhất ({highestGPA}):");
            foreach (var student in highestGPAStudents)
            {
                Console.WriteLine($"Mã số: {student.StudentID}");
                Console.WriteLine($"Họ tên: {student.FullName}");
                Console.WriteLine($"Khoa: {student.Faculty}");
                Console.WriteLine($"Điểm trung bình: {student.AverageScore}");
                Console.WriteLine();
            }
        }

        static void DisplayGPAStatistics()
        {
            var excellentCount = students.Count(s => s.AverageScore >= 9.0);
            var goodCount = students.Count(s => s.AverageScore >= 8.0 && s.AverageScore < 9.0);
            var fairCount = students.Count(s => s.AverageScore >= 7.0 && s.AverageScore < 8.0);

            Console.WriteLine($"Số lượng sinh viên xuất sắc (9.0 đến 10.0): {excellentCount}");
            Console.WriteLine($"Số lượng sinh viên giỏi (8.0 đến cận 9.0): {goodCount}");
            Console.WriteLine($"Số lượng sinh viên khá (7.0 đến cận 8.0): {fairCount}");
        }
    }
}
