using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace Registrar
{
    public class Department
    {
        private string _name;
        private int _id;

        public Department(string Name, int Id = 0)
        {
            _name = Name;
            _id = Id;
        }

        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO departments (name) OUTPUT INSERTED.id VALUES (@DeptName);", conn);
            cmd.Parameters.Add(new SqlParameter("@DeptName", this.GetName()));

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._id = rdr.GetInt32(0);
            }

            DB.CloseSqlConnection(conn, rdr);
        }

        public static List<Department> GetAll()
        {
            List<Department> allDepartments = new List<Department> {};
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM departments;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                allDepartments.Add(new Department(rdr.GetString(1), rdr.GetInt32(0)));
            }

            DB.CloseSqlConnection(conn, rdr);
            return allDepartments;
        }

        public static Department Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM departments WHERE id = @DepartmentId;", conn);
            cmd.Parameters.Add(new SqlParameter("@DepartmentId", id));

            SqlDataReader rdr = cmd.ExecuteReader();

            int foundId = 0;
            string foundName = null;


            while(rdr.Read())
            {
                foundId = rdr.GetInt32(0);
                foundName = rdr.GetString(1);

            }

            DB.CloseSqlConnection(conn, rdr);

            return new Department(foundName, foundId);
        }

        public static List<Student> GetStudents(int id)
        {
            List<Student> studentsInDept = new List<Student> {};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM students WHERE dept_id = @DeptId;", conn);
            cmd.Parameters.Add(new SqlParameter("@DeptId", id));

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                studentsInDept.Add(new Student(rdr.GetString(1), rdr.GetString(2), rdr.GetInt32(3), rdr.GetInt32(0)));
            }

            DB.CloseSqlConnection(conn, rdr);
            return studentsInDept;
        }
        public List<Student> GetDepartmentStudents()
        {
            List<Student> studentsInDept = new List<Student> {};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM students WHERE dept_id = @DeptId;", conn);
            cmd.Parameters.Add(new SqlParameter("@DeptId", this.GetId()));

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                studentsInDept.Add(new Student(rdr.GetString(1), rdr.GetString(2), rdr.GetInt32(3), rdr.GetInt32(0)));
            }

            DB.CloseSqlConnection(conn, rdr);
            return studentsInDept;
        }

        public static List<Course> GetCourses(int id)
        {
            List<Course> coursesInDept = new List<Course> {};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM courses WHERE dept_id = @DeptId;", conn);
            cmd.Parameters.Add(new SqlParameter("@DeptId", id));

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                coursesInDept.Add(new Course(rdr.GetString(1), rdr.GetString(2), rdr.GetInt32(3), rdr.GetInt32(0)));
            }

            DB.CloseSqlConnection(conn, rdr);
            return coursesInDept;
        }

        public List<Course> GetDepartmentCourses()
        {
            List<Course> coursesInDept = new List<Course> {};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM courses WHERE dept_id = @DeptId;", conn);
            cmd.Parameters.Add(new SqlParameter("@DeptId", this.GetId()));

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                coursesInDept.Add(new Course(rdr.GetString(1), rdr.GetString(2), rdr.GetInt32(3), rdr.GetInt32(0)));
            }

            DB.CloseSqlConnection(conn, rdr);
            return coursesInDept;
        }

        public List<Student> CheckMissingRequirements()
        {
            List<Student> foundStudents = new List<Student>();
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT DISTINCT students.* FROM students LEFT JOIN students_courses ON (students.id = students_courses.student_id) LEFT JOIN courses ON (courses.id = students_courses.course_id) WHERE students.dept_id != @DeptId;", conn);
            cmd.Parameters.Add(new SqlParameter("@DeptId", this.GetId()));

            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                foundStudents.Add(new Student(rdr.GetString(1), rdr.GetString(2), rdr.GetInt32(3), rdr.GetInt32(0)));
            }

            DB.CloseSqlConnection(conn, rdr);
            return foundStudents;
        }

        public static void Delete(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM departments WHERE id=@DepartmentId;", conn);
            cmd.Parameters.Add(new SqlParameter("@DepartmentId", id));

            cmd.ExecuteNonQuery();
            DB.CloseSqlConnection(conn);
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM departments;", conn);
            cmd.ExecuteNonQuery();

            DB.CloseSqlConnection(conn);
        }

        public override bool Equals(System.Object otherDepartment)
        {
            if(!(otherDepartment is Department))
            {
                return false;
            }
            else
            {
                Department newDepartment = (Department) otherDepartment;
                bool idEquality = this.GetId() == newDepartment.GetId();
                bool nameEquality = this.GetName() == newDepartment.GetName();
                return (idEquality && nameEquality);
            }
        }
    }
}
