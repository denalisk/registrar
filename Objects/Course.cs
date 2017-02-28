using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace Registrar
{
    public class Course
    {
        private string _name;
        private string _number;
        private int _deptId;
        private int _id;

        public Course(string Name, string Number, int DeptId, int Id = 0)
        {
            _name = Name;
            _number = Number;
            _deptId = DeptId;
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

        public string GetNumber()
        {
            return _number;
        }

        public int GetDeptId()
        {
            return _deptId;
        }

        public static Course Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM courses WHERE id = @CourseId;", conn);
            cmd.Parameters.Add(new SqlParameter("@CourseId", id));

            SqlDataReader rdr = cmd.ExecuteReader();

            int foundId = 0;
            string foundName = null;
            string foundCourseNumber = null;
            int foundDeptId = 0;

            while(rdr.Read())
            {
                foundId = rdr.GetInt32(0);
                foundName = rdr.GetString(1);
                foundCourseNumber = rdr.GetString(2);
                foundDeptId = rdr.GetInt32(3);
            }

            DB.CloseSqlConnection(conn, rdr);

            return new Course(foundName, foundCourseNumber, foundDeptId, foundId);
        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO courses (name, number, dept_id) OUTPUT INSERTED.id VALUES (@CourseName, @CourseNumber, @CourseDeptId);", conn);
            cmd.Parameters.Add(new SqlParameter("@CourseName", this.GetName()));
            cmd.Parameters.Add(new SqlParameter("@CourseNumber", this.GetNumber()));
            cmd.Parameters.Add(new SqlParameter("@CourseDeptId", this.GetDeptId()));

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._id = rdr.GetInt32(0);
            }

            DB.CloseSqlConnection(conn, rdr);
        }

        public static List<Course> GetAll()
        {
            List<Course> allCourses = new List<Course> {};
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM courses;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                allCourses.Add(new Course(rdr.GetString(1), rdr.GetString(2), rdr.GetInt32(3), rdr.GetInt32(0)));
            }

            DB.CloseSqlConnection(conn, rdr);
            return allCourses;
        }

        public void AddStudent(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO students_courses (student_id, course_id) VALUES (@StudentId, @CourseId);", conn);
            cmd.Parameters.Add(new SqlParameter("@StudentId", id));
            cmd.Parameters.Add(new SqlParameter("@CourseId", this.GetId()));

            cmd.ExecuteNonQuery();
            DB.CloseSqlConnection(conn);
        }


        public static List<Student> GetByCourse(int id)
        {
            List<Student> studentsInCourse = new List<Student> {};
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT students.* FROM students JOIN students_courses ON (students.id = students_courses.student_id) JOIN courses ON (courses.id = students_courses.course_id) WHERE courses.id = @CourseId;", conn);

            cmd.Parameters.Add(new SqlParameter("@CourseId", id));

            SqlDataReader rdr = cmd.ExecuteReader();
            while(rdr.Read())
            {
                studentsInCourse.Add(new Student(rdr.GetString(1), rdr.GetString(2), rdr.GetInt32(3), rdr.GetInt32(0)));
            }

            DB.CloseSqlConnection(conn, rdr);
            return studentsInCourse;
        }


        public static void Delete(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM courses WHERE id=@CourseId;", conn);
            cmd.Parameters.Add(new SqlParameter("@CourseId", id));

            cmd.ExecuteNonQuery();
            DB.CloseSqlConnection(conn);
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM courses;", conn);
            cmd.ExecuteNonQuery();

            DB.CloseSqlConnection(conn);
        }

        public override bool Equals(System.Object otherCourse)
        {
            if(!(otherCourse is Course))
            {
                return false;
            }
            else
            {
                Course newCourse = (Course) otherCourse;
                bool idEquality = this.GetId() == newCourse.GetId();
                bool nameEquality = this.GetName() == newCourse.GetName();
                bool numEquality = this.GetNumber() == newCourse.GetNumber();
                bool deptEquality = this.GetDeptId() == newCourse.GetDeptId();
                return (idEquality && nameEquality && numEquality && deptEquality);
            }
        }
    }
}
