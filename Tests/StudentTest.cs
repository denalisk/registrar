using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using Xunit;
using System.Data;

namespace Registrar
{
    public class StudentTest : IDisposable
    {
        public StudentTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=registrar_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Student_EmptyOnLoad_0()
        {
            Assert.Equal(0, Student.GetAll().Count);
        }
        [Fact]
        public void Student_EqualityTest_1()
        {
            Student newStudent = new Student("Johnny English", "2001-09-21", 1);
            Student secondStudent = new Student("Johnny English", "2001-09-21", 1);

            Assert.Equal(newStudent, secondStudent);
        }
        [Fact]
        public void Save_SavesStudentToDatabase_2()
        {
            Student newStudent = new Student("Johnny English", "2001-09-21", 1);
            newStudent.Save();

            List<Student> expectedList = new List<Student> {newStudent};
            List<Student> actualList = Student.GetAll();

            Assert.Equal(expectedList, actualList);
        }
        [Fact]
        public void Student_Find_ReturnStudentObject()
        {
            Student newStudent = new Student("Johnny English", "2001-09-21", 1);
            newStudent.Save();

            Student foundStudent = Student.Find(newStudent.GetId());

            Assert.Equal(newStudent, foundStudent);
        }
        [Fact]
        public void Student_Delete_RemoveFromDatabase()
        {
            Student newStudent = new Student("Johnny English", "2001-09-21", 1);
            newStudent.Save();

            Student.Delete(newStudent.GetId());

            Assert.Equal(0, Student.GetAll().Count);
        }

        [Fact]
        public void Student_AddCourseToStudent()
        {
            Student newStudent = new Student("Johnny English", "2001-09-21", 1);
            newStudent.Save();
            Student otherStudent = new Student("Smitty Sociology", "2001-09-21", 1);
            otherStudent.Save();
            List<Student> expected = new List<Student>{newStudent};

            Course newCourse = new Course("English 101", "ENG101", 1);
            newCourse.Save();

            newStudent.AddCourse(newCourse.GetId());

            List<Student> actual =Course.GetByCourse(newCourse.GetId());

            Assert.Equal(expected[0].GetName(), actual[0].GetName());
        }

        [Fact]
        public void Student_FindCourseByStudentId()
        {
            Student newStudent = new Student("Johnny English", "2001-09-21", 1);
            newStudent.Save();

            Course newCourse = new Course("English 101", "ENG101", 1);
            newCourse.Save();
            Course otherCourse = new Course("English 102", "ENG102", 1);
            otherCourse.Save();
            List<Course> expected = new List<Course>{newCourse, otherCourse};
            newStudent.AddCourse(newCourse.GetId());
            newStudent.AddCourse(otherCourse.GetId());


            List<Course> actual = newStudent.GetCourses();


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
