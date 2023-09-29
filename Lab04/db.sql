USE [QuanLySinhVien]
GO
INSERT [dbo].[Faculty] ([FacultyID], [FacultyName], [TotalProfessor]) VALUES (1, N'Công Nghệ Thông Tin', NULL)
INSERT [dbo].[Faculty] ([FacultyID], [FacultyName], [TotalProfessor]) VALUES (2, N'Ngôn Ngữ Anh', NULL)
INSERT [dbo].[Faculty] ([FacultyID], [FacultyName], [TotalProfessor]) VALUES (3, N'Quản Trị Kinh Doanh', NULL)
GO
INSERT [dbo].[Student] ([StudentID], [FullName], [AverageScore], [FacultyID]) VALUES (N'1611061916', N'Nguyễn Trần Hương Lan', 4.5, 1)
INSERT [dbo].[Student] ([StudentID], [FullName], [AverageScore], [FacultyID]) VALUES (N'1711060596', N'Đàm Minh Đức', 2.5, 1)
INSERT [dbo].[Student] ([StudentID], [FullName], [AverageScore], [FacultyID]) VALUES (N'1711061004', N'Nguyễn Quốc An', 10, 2)
GO
