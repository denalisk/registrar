// using System;
// using System.Data.SqlClient;
// using System.Collections.Generic;
// using Xunit;
// using System.Data;
//
// namespace Registrar
// {
//     public class DataGenTest : IDisposable
//     {
//         public DataGenTest()
//         {
//             DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=registrar;Integrated Security=SSPI;";
//         }
//
//         [Fact]
//         public void WriteSomeData()
//         {
//             // Department englishDepartment = new Department("English");
//             // englishDepartment.Save();
//             // Department spanishDepartment = new Department("Spanish");
//             // spanishDepartment.Save();
//             // Department germanDepartment = new Department("German");
//             // germanDepartment.Save();
//             // Department frenchDepartment = new Department("French");
//             // frenchDepartment.Save();
//             //
//             // Course englishCourse = new Course("English 101", "ENG101", englishDepartment.GetId());
//             // englishCourse.Save();
//             // Course english2Course = new Course("English 102", "Eng102", englishDepartment.GetId());
//             // english2Course.Save();
//             // Course frenchCourse = new Course("French 101", "FRAN101", frenchDepartment.GetId());
//             // frenchCourse.Save();
//             // Course french2Course = new Course("French 102", "FRAN102", frenchDepartment.GetId());
//             // french2Course.Save();
//             // Course germanCourse = new Course("German 101", "GER101", germanDepartment.GetId());
//             // germanCourse.Save();
//             // Course german2Course = new Course("German 102", "GER102", germanDepartment.GetId());
//             // german2Course.Save();
//             // Course spanishCourse = new Course("Spanish 101", "SPAN101", spanishDepartment.GetId());
//             // spanishCourse.Save();
//             // Course spanish2Course = new Course("Spanish 102", "SPAN102", spanishDepartment.GetId());
//             // spanish2Course.Save();
//             //
//             //
//             // Student englishStudent = new Student("Johnny English", "2001-09-21", englishDepartment.GetId());
//             // englishStudent.Save();
//             // Student spanishStudent = new Student("Smitty Spanish", "2001-09-21", spanishDepartment.GetId());
//             // spanishStudent.Save();
//             // Student frenchStudent = new Student("Franny French", "2001-09-21", frenchDepartment.GetId());
//             // frenchStudent.Save();
//             // Student germanStudent = new Student("Gertrude German", "2001-09-21", germanDepartment.GetId());
//             // germanStudent.Save();
//             //
//             // englishStudent.AddCourse(englishCourse.GetId());
//             // englishStudent.AddCourse(english2Course.GetId());
//             // englishStudent.AddCourse(germanCourse.GetId());
//             //
//             // spanishStudent.AddCourse(spanishCourse.GetId());
//             // spanishStudent.AddCourse(spanish2Course.GetId());
//             // frenchStudent.AddCourse(frenchCourse.GetId());
//             // frenchStudent.AddCourse(french2Course.GetId());
//             // germanStudent.AddCourse(germanCourse.GetId());
//             // germanStudent.AddCourse(german2Course.GetId());
//             // Student englishStudent2 = new Student("Jenny English", "2001-09-21", 1);
//             // englishStudent2.Save();
//
//
//         }
//
//         public void Dispose()
//         {
//             // Course.DeleteAll();
//             // Student.DeleteAll();
//             // Department.DeleteAll();
//         }
//     }
// }
