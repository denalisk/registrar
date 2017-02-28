using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using Xunit;
using System.Data;

namespace Registrar
{
    public class DepartmentTest : IDisposable
    {
        public DepartmentTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=registrar_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Department_EmptyOnLoad_0()
        {
            Assert.Equal(0, Department.GetAll().Count);
        }
        [Fact]
        public void Department_EqualityTest_1()
        {
            Department newDepartment = new Department("English");
            Department secondDepartment = new Department("English");

            Assert.Equal(newDepartment, secondDepartment);
        }
        [Fact]
        public void Save_SavesDeptToDatabase_2()
        {
            Department newDepartment = new Department("English");
            newDepartment.Save();

            List<Department> expectedList = new List<Department> {newDepartment};
            List<Department> actualList = Department.GetAll();

            Assert.Equal(expectedList, actualList);
        }
        [Fact]
        public void Department_Find_ReturnDepartmentObject()
        {
            Department newDepartment = new Department("English");
            newDepartment.Save();

            Department foundDepartment = Department.Find(newDepartment.GetId());

            Assert.Equal(newDepartment, foundDepartment);
        }
        [Fact]
        public void Department_Delete_RemoveFromDatabase()
        {
            Department newDepartment = new Department("English");
            newDepartment.Save();

            Department.Delete(newDepartment.GetId());

            Assert.Equal(0, Department.GetAll().Count);
        }
        [Fact]
        public void Department_GetStudents_ReturnStudentsInDept()
        {
            Department newDepartment = new Department("English");
            newDepartment.Save();
            Student newStudent = new Student("Johnny English", "2001-09-21", newDepartment.GetId());
            newStudent.Save();
            Student otherStudent = new Student("Smitty Sociology", "2001-09-21", newDepartment.GetId());
            otherStudent.Save();

            List<Student> expected = new List<Student> {newStudent, otherStudent};
            List<Student> actual = Department.GetStudents(newDepartment.GetId());

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Department_GetCoursesByDept()
        {
            Department newDepartment = new Department("English");
            newDepartment.Save();
            Course newCourse = new Course("English", "ENG21", newDepartment.GetId());
            newCourse.Save();
            Course otherCourse = new Course("Sociology", "SOC21", newDepartment.GetId());
            otherCourse.Save();

            List<Course> expected = new List<Course> {newCourse, otherCourse};
            List<Course> actual = Department.GetCourses(newDepartment.GetId());

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Department_CheckStudentsNotCompleted()
        {
            Department newDepartment = new Department("English");
            newDepartment.Save();
            Department otherDepartment = new Department("Spanish");
            otherDepartment.Save();

            Course newCourse = new Course("English 101", "ENG101", newDepartment.GetId());
            newCourse.Save();
            Course otherCourse = new Course("English 102", "Eng102", otherDepartment.GetId());
            otherCourse.Save();


            Student newStudent = new Student("Johnny English", "2001-09-21", newDepartment.GetId());
            newStudent.Save();
            newStudent.AddCourse(newCourse.GetId());
            newStudent.AddCourse(otherCourse.GetId());


            Student otherStudent = new Student("Smitty Sociology", "2001-09-21", otherDepartment.GetId());
            otherStudent.Save();
            otherStudent.AddCourse(otherCourse.GetId());

            List<Student> expected = new List<Student>{otherStudent};
            List<Student> actual = newDepartment.CheckMissingRequirements();

            foreach(Student student in actual)
            {
                Console.WriteLine(student.GetName());
            }



            Assert.Equal(expected, actual);

        }

        public void Dispose()
        {
            Course.DeleteAll();
            Student.DeleteAll();
            Department.DeleteAll();
        }
    }
}
