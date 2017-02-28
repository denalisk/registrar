using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace Registrar
{
    public class Student
    {
        private string _name;
        private int _id;
        private string _enrollmentDate;
        private int _deptId;

        public Student(string Name, string EnrollmentDate, int DeptId, int Id = 0)
        {
            _name = Name;
            _enrollmentDate = EnrollmentDate;
            _deptId = DeptId;
            _id = Id;
        }

        public int GetId()
        {
            return _id;
        }

        public string GetEnrollmentDate()
        {
            return _enrollmentDate;
        }

        public int GetDeptId()
        {
            return _deptId;
        }

        public string GetName()
        {
            return _name;
        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO students (name, enrollment_date, dept_id) OUTPUT INSERTED.id VALUES (@StudentName, @Date, @DeptId);", conn);
            cmd.Parameters.Add(new SqlParameter("@StudentName", this.GetName()));
            cmd.Parameters.Add(new SqlParameter("@Date", this.GetEnrollmentDate()));
            cmd.Parameters.Add(new SqlParameter("@DeptId", this.GetDeptId()));

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._id = rdr.GetInt32(0);
            }

            DB.CloseSqlConnection(conn, rdr);
        }

        public static List<Student> GetAll()
        {
            List<Student> allStudents = new List<Student> {};
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM students;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                allStudents.Add(new Student(rdr.GetString(1), rdr.GetString(2), rdr.GetInt32(3), rdr.GetInt32(0)));
            }

            DB.CloseSqlConnection(conn, rdr);
            return allStudents;
        }

        public void UpdateCourseGrade(int id, string grade)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE students_courses SET grade = @NewGrade WHERE student_id = @StudentId AND course_id = @CourseId;", conn);
            cmd.Parameters.Add(new SqlParameter("@NewGrade", grade));
            cmd.Parameters.Add(new SqlParameter("@StudentId", this.GetId()));
            cmd.Parameters.Add(new SqlParameter("@CourseId", id));

            cmd.ExecuteNonQuery();

            DB.CloseSqlConnection(conn);
        }

        public string GetCourseGrade(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT grade FROM students_courses WHERE student_id = @StudentId AND course_id = @CourseId;", conn);
            cmd.Parameters.Add(new SqlParameter("@StudentId", this.GetId()));
            cmd.Parameters.Add(new SqlParameter("@CourseId", id));

            SqlDataReader rdr = cmd.ExecuteReader();

            string foundGrade = null;

            while(rdr.Read())
            {
                try
                {
                    foundGrade = rdr.GetString(0);
                }
                catch
                {
                    foundGrade = "There is no grade listed";
                }
            }
            DB.CloseSqlConnection(conn, rdr);

            return foundGrade;
        }

        public List<Course> GetCompletedCourses()
        {
            List<Course> completedCourses = new List<Course> {};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT courses.* FROM students JOIN students_courses ON (students.id = students_courses.student_id) JOIN courses ON (courses.id = students_courses.course_id) WHERE students.id = @StudentId AND students_courses.grade IS NOT NULL;", conn);
            cmd.Parameters.Add(new SqlParameter("@StudentId", this.GetId()));

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                completedCourses.Add(new Course(rdr.GetString(1), rdr.GetString(2), rdr.GetInt32(3), rdr.GetInt32(0)));
            }

            DB.CloseSqlConnection(conn, rdr);
            return completedCourses;
        }

        public static Student Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM students WHERE id = @StudentId;", conn);
            cmd.Parameters.Add(new SqlParameter("@StudentId", id));

            SqlDataReader rdr = cmd.ExecuteReader();

            int foundId = 0;
            string foundName = null;
            string foundEnrollmentDate = null;
            int foundDeptId = 0;

            while(rdr.Read())
            {
                foundId = rdr.GetInt32(0);
                foundName = rdr.GetString(1);
                foundEnrollmentDate = rdr.GetString(2);
                foundDeptId = rdr.GetInt32(3);
            }

            DB.CloseSqlConnection(conn, rdr);

            return new Student(foundName, foundEnrollmentDate, foundDeptId, foundId);
        }

        public void AddCourse(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO students_courses (student_id, course_id) VALUES (@StudentId, @CourseId);", conn);

            cmd.Parameters.Add(new SqlParameter("@StudentId", this.GetId()));
            cmd.Parameters.Add(new SqlParameter("@CourseId", id));

            cmd.ExecuteNonQuery();

            DB.CloseSqlConnection(conn);
        }

        public List<Course> GetCourses()
        {
            List<Course> studentCourses = new List<Course>();
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT courses.* FROM courses JOIN students_courses ON (courses.id = students_courses.course_id) JOIN students ON (students.id = students_courses.student_id) WHERE students.id = @StudentId;", conn);

            cmd.Parameters.Add(new SqlParameter("@StudentId", this.GetId()));
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                studentCourses.Add(new Course(rdr.GetString(1), rdr.GetString(2), rdr.GetInt32(3), rdr.GetInt32(0)));
            }

            DB.CloseSqlConnection(conn, rdr);

            return studentCourses;
        }

        public static void Delete(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM students WHERE id=@StudentId;", conn);
            cmd.Parameters.Add(new SqlParameter("@StudentId", id));

            cmd.ExecuteNonQuery();
            DB.CloseSqlConnection(conn);
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM students;", conn);
            cmd.ExecuteNonQuery();

            DB.CloseSqlConnection(conn);
        }

        public override bool Equals(System.Object otherStudent)
        {
            if(!(otherStudent is Student))
            {
                return false;
            }
            else
            {
                Student newStudent = (Student) otherStudent;
                bool idEquality = this.GetId() == newStudent.GetId();
                bool nameEquality = this.GetName() == newStudent.GetName();
                bool deptIdEquality = this.GetDeptId() == newStudent.GetDeptId();
                bool dateEquality = this.GetEnrollmentDate() == newStudent.GetEnrollmentDate();
                return (idEquality && nameEquality && dateEquality && deptIdEquality);
            }
        }
    }
}
