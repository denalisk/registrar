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

        public void Dispose()
        {
            Course.DeleteAll();
            Student.DeleteAll();
            Department.DeleteAll();
        }
    }
}
