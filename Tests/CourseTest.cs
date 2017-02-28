using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using Xunit;
using System.Data;

namespace Registrar
{
    public class CourseTest : IDisposable
    {
        public CourseTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=registrar_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Course_EmptyOnLoad_0()
        {
            Assert.Equal(0, Course.GetAll().Count);
        }
        [Fact]
        public void Course_EqualityTest_1()
        {
            Course newCourse = new Course("English 101", "ENG101", 1);
            Course secondCourse = new Course("English 101", "ENG101", 1);

            Assert.Equal(newCourse, secondCourse);
        }
        [Fact]
        public void Save_SavesCourseToDatabase_2()
        {
            Course newCourse = new Course("English 101", "ENG101", 1);
            newCourse.Save();

            List<Course> expectedList = new List<Course> {newCourse};
            List<Course> actualList = Course.GetAll();

            Assert.Equal(expectedList, actualList);
        }
        [Fact]
        public void Course_Find_2()
        {
            Course newCourse = new Course("English 101", "ENG101", 1);
            newCourse.Save();

            Course foundCourse = Course.Find(newCourse.GetId());

            Assert.Equal(newCourse, foundCourse);
        }
        [Fact]
        public void Course_Delete_RemoveFromDatabase()
        {
            Course newCourse = new Course("English 101", "ENG101", 1);
            newCourse.Save();

            Course.Delete(newCourse.GetId());

            Assert.Equal(0, Course.GetAll().Count);
        }

        public void Dispose()
        {
            Course.DeleteAll();
            Student.DeleteAll();
            Department.DeleteAll();
        }
    }
}
