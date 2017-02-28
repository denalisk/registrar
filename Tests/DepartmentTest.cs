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

        public void Dispose()
        {
            Course.DeleteAll();
            Student.DeleteAll();
            Department.DeleteAll();
        }
    }
}
