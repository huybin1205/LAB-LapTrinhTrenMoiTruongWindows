using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01_02
{
    class Program
    {
        /// <summary>
        /// Viết chương trình menu cho phép người dùng quản lý sinh viên. Biết rằng 
        /// mỗi sinh viên có(mã số, họ tên, khoa và điểm trung bình).
        /// Cac chức năng menu:
        ///     1 – Thêm sinh viên
        ///     2 – Xuất danh sach sinh viên
        ///     3 - Xuất ra thông tin của cac SV đều thuộc khoa “CNTT”
        ///     4 - Xuất ra thông tin sinh viên có điểm TB lớn hơn bằng 5.
        ///     5 - Xuất ra danh sach sinh viên được sắp xếp theo điểm trung bình tăng dần
        ///     6 - Xuất ra danh sach sinh viên có điểm TB lớn hơn bằng 5 và thuộc khoa “CNTT”
        ///     7- Xuất ra danh sach sinh viên có điểm trung bình cao nhất và thuộc khoa “CNTT”
        ///     8- Hãy cho biết số lượng của từng xếp loại trong danh sach? Biết rằng theo thang
        ///        điểm 10.
        ///        Từ 9,0 đến 10,0: Xuất sắc; Từ 8,0 đến cận 9,0: Giỏi; Từ 7,0 đến cận 8,0: Kha;
        ///     0 - Thoat
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            List<Student> studentList = new List<Student>();
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("=== MENU ===");
                Console.WriteLine("1. Them sinh vien");
                Console.WriteLine("2. Xuat danh sach sinh vien");
                Console.WriteLine("3. Xuat ra thong tin cua cac SV deu thuoc khoa “CNTT”");
                Console.WriteLine("4. Xuat ra thong tin sinh vien co diem TB lon hon bang 5.");
                Console.WriteLine("5. Xuat ra danh sach sinh vien duoc sap xep theo diem trung binh tang dan");
                Console.WriteLine("6. Xuat ra danh sach sinh vien co diem TB lon hon bang 5 va thuoc khoa “CNTT”");
                Console.WriteLine("7. Xuat ra danh sach sinh vien co diem trung binh cao nhat va thuoc khoa “CNTT”");
                Console.WriteLine("8. Hay cho biet so luong cua tung xep loai trong danh sach? Biet rang theo thang diem 10.");
                Console.WriteLine("0. Thoat");
                Console.Write("Chon chuc nang (0-2): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddStudent(studentList);
                        break;
                    case "2":
                        DisplayStudentList(studentList);
                        break;
                    case "3":
                        DisplayStudentsByFaculty(studentList, "CNTT");
                        break;
                    case "4":
                        DisplayStudentsWithHighAverageScore(studentList, 5);
                        break;
                    case "5":
                        SortStudentsByAverageScore(studentList);
                        break;
                    case "6":
                        DisplayStudentsByFacultyAndScore(studentList, "CNTT", 5);
                        break;
                    case "7":
                        DisplayStudentsWithHighestAverageScoreByFaculty(studentList, "CNTT");
                        break;
                    case "8":
                        DisplayGPAStatistics(studentList);
                        break;
                    case "0":
                        exit = true;
                        Console.WriteLine("Ket thuc chuong trinh.");
                        break;
                    default:
                        Console.WriteLine("Tuy chon khong hop le. Vui long chon lai.");
                        break;
                }

                Console.WriteLine();
            }
        }

        static void AddStudent(List<Student> studentList)
        {
            Console.WriteLine("=== Nhap thong tin sinh vien ===");
            Student student = new Student();
            student.Input();
            studentList.Add(student);
            Console.WriteLine("Them sinh vien thanh cong!");
        }

        static void DisplayStudentList(List<Student> studentList)
        {
            Console.WriteLine("=== Danh sach chi tiet thong tin sinh vien ===");
            foreach (Student student in studentList)
            {
                student.Show();
            }
        }

        //case 3: DS Sinh viên khoa CNTT
        static void DisplayStudentsByFaculty(List<Student> studentList, string faculty)
        {
            Console.WriteLine("=== Danh sach sinh vien thuộc khoa {0}", faculty);
            var students = studentList.Where(s => s.Faculty.Equals(faculty,
            StringComparison.OrdinalIgnoreCase));
            DisplayStudentList(studentList);
        }

        //case 4: Xuất ra thông tin sinh viên có điểm TB lớn hơn bằng 5.
        static void DisplayStudentsWithHighAverageScore(List<Student> studentList, float minDTB)
        {
            Console.WriteLine("=== Danh sach sinh vien có điem TB >= {0}", minDTB);
            var students = studentList.Where(s => s.AverageScore >= minDTB);
            DisplayStudentList(studentList);
        }

        //case 5: Xuất ra danh sach sinh viên được sắp xếp theo điểm trung bình tăng dần
        static void SortStudentsByAverageScore(List<Student> studentList)
        {
            Console.WriteLine("=== Danh sach sinh vien được sap xep theo điem trung binh tang dan === ");
            var sortedStudents = studentList.OrderBy(s => s.AverageScore).ToList();
            DisplayStudentList(sortedStudents);
        }

        //case 6: DS sinh vien co DTB >=5 va thuoc khoa CNTT
        static void DisplayStudentsByFacultyAndScore(List<Student> studentList, string faculty, float minDTB)
        {
            Console.WriteLine("=== Danh sach sinh vien có điem TB >= {0} va thuoc khoa {1}", minDTB, faculty);
            var students = studentList.Where(s => s.AverageScore >= minDTB && 
                                            s.Faculty.Equals(faculty, StringComparison.OrdinalIgnoreCase))
                                        .ToList();
            DisplayStudentList(students);
        }

        //case 7: Xuat ra danh sach sinh vien co diem trung binh cao nhat va thuoc khoa <faculty>
        static void DisplayStudentsWithHighestAverageScoreByFaculty(List<Student> studentList, string faculty)
        {
            Console.WriteLine("=== Danh sach sinh vien có điem TB cao nhat và thuoc khoa {0}", faculty);
            var students = studentList.Where(s => s.Faculty == faculty).ToList();
            if (studentList.Count == 0)
            {
                Console.WriteLine("Khong co sinh vien thuoc khoa '{0}'.", faculty);
                return;
            }

            var highestGPA = studentList.Max(s => s.AverageScore);
            var highestGPAStudents = studentList.Where(s => s.AverageScore == highestGPA).ToList();

            Console.WriteLine($"Danh sach sinh vien thuoc khoa '{faculty}' có điem trung binh cao nhat ({highestGPA}):");
            foreach (var student in highestGPAStudents)
            {
                student.Show();
                Console.WriteLine();
            }
        }

        // case 8: Hay cho biet so luong cua tung xep loai trong danh sach? Biet rang theo thang diem 10.
        static void DisplayGPAStatistics(List<Student> studentList)
        {
            var excellentCount = studentList.Count(s => s.AverageScore >= 9.0);
            var goodCount = studentList.Count(s => s.AverageScore >= 8.0 && s.AverageScore < 9.0);
            var fairCount = studentList.Count(s => s.AverageScore >= 7.0 && s.AverageScore < 8.0);

            Console.WriteLine($"So lượng sinh vien xuat sac (9.0 đen 10.0): {excellentCount}");
            Console.WriteLine($"So lượng sinh vien gioi (8.0 đen can 9.0): {goodCount}");
            Console.WriteLine($"So lượng sinh vien kha (7.0 đen can 8.0): {fairCount}");
        }
    }
}
